using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using SharpexGL.Framework.Network.Logic;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Packages.System;

namespace SharpexGL.Framework.Network.Protocols.Udp
{
    public class UdpClient : IClient
    {
        #region IClient Implemenation
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void Send(IBasePackage package)
        {
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(package, mStream);
                _udpClient.Client.SendTo(mStream.ToArray(), new IPEndPoint(_ip, 2563));
            }
        }
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IPAddress receiver)
        {
            package.Receiver = receiver;
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(package, mStream);
                _udpClient.Client.SendTo(mStream.ToArray(), new IPEndPoint(_ip, 2563));
            }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public void BeginReceive()
        {
            var beginHandle = new Thread(InternalBeginReceive) { IsBackground = true };
            beginHandle.Start();
        }
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        public void Connect(IPAddress ip)
        {
            _ip = ip;
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(new UdpPackage(UdpNotify.Hi), mStream);
                _udpClient.Client.SendTo(mStream.ToArray(), new IPEndPoint(ip, 2563));
                _connected = true;
            }
        }
        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        public void Disconnect()
        {
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(new UdpPackage(UdpNotify.Bye), mStream);
                _udpClient.Client.SendTo(mStream.ToArray(), new IPEndPoint(_ip, 2563));
                _connected = false;
            }
            _udpClient.Close();
        }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IPackageListener subscriber)
        {
            _packageListeners.Add(subscriber);
        }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IClientListener subscriber)
        {
            _clientListeners.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IPackageListener unsubscriber)
        {
            _packageListeners.Remove(unsubscriber);
        }
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IClientListener unsubscriber)
        {
            _clientListeners.Remove(unsubscriber);
        }

        #endregion


        private readonly System.Net.Sockets.UdpClient _udpClient;
        private IPAddress _ip;
        private bool _connected;
        private int _idleTimeout;
        private const int IdleMax = 30;
        private int _currentIdle;
        private readonly List<IClientListener> _clientListeners;
        private readonly List<IPackageListener> _packageListeners;

        /// <summary>
        /// Initializes a new UdpClient class.
        /// </summary>
        public UdpClient()
        {
            _clientListeners = new List<IClientListener>();
            _packageListeners = new List<IPackageListener>();
            _udpClient = new System.Net.Sockets.UdpClient(2563);
        }

        /// <summary>
        /// Receives data.
        /// </summary>
        private void InternalBeginReceive()
        {
            while (_connected)
            {
                if (_udpClient.Available > 0)
                {
                    var serverIpEndPoint = new IPEndPoint(IPAddress.Any, 2563);
                    var receivedData = _udpClient.Receive(ref serverIpEndPoint);
                    using (var mStream = new MemoryStream(receivedData))
                    {
                        var package = PackageSerializer.Deserialize(mStream);

                        var binaryPackage = package as BinaryPackage;
                        if (binaryPackage != null)
                        {
                            //binary package

                            return;
                        }

                        //system packages
                        var systemPackage = package as NotificationPackage;
                        if (systemPackage != null)
                        {
                            switch (systemPackage.Mode)
                            {
                                case NotificationMode.ClientJoined:
                                    for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientJoined(systemPackage.Connection[0]);
                                    }
                                    break;
                                case NotificationMode.ClientExited:
                                    for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientExited(systemPackage.Connection[0]);
                                    }
                                    break;
                                case NotificationMode.ClientList:
                                    for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientListing(systemPackage.Connection);
                                    }
                                    break;
                                case NotificationMode.TimeOut:
                                    for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientTimedOut();
                                    }
                                    break;
                                case NotificationMode.ServerShutdown:
                                    for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnServerShutdown();
                                    }
                                    break;
                            }
                        }

                        var pingPackage = package as PingPackage;
                        if (pingPackage != null)
                        {
                            //send ping package back
                            Send(pingPackage);
                        }
                    }
                }
                else
                {
                    //no data available

                    Idle();
                }
            }
        }

        /// <summary>
        /// Idles the thread.
        /// </summary>
        private void Idle()
        {
            //Idle to save cpu power.   
            _currentIdle++;

            if (_idleTimeout > 0)
            {
                Thread.Sleep(_idleTimeout);
            }

            if (_currentIdle < IdleMax) return;

            _currentIdle = 0;
            if (_idleTimeout < 15)
            {
                _idleTimeout++;
            }
        }
    }
}
