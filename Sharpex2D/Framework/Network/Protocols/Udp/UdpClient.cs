// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Sharpex2D.Framework.Network.Logic;
using Sharpex2D.Framework.Network.Packages;
using Sharpex2D.Framework.Network.Packages.System;

namespace Sharpex2D.Framework.Network.Protocols.Udp
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class UdpClient : IClient, IDisposable
    {
        #region IClient Implemenation

        /// <summary>
        ///     Sends a package to the given receivers.
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
        ///     Sends a package to the given receivers.
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
        ///     Receives a package.
        /// </summary>
        public void BeginReceive()
        {
            var beginHandle = new Thread(InternalBeginReceive) {IsBackground = true};
            beginHandle.Start();
        }

        /// <summary>
        ///     Connects to the local server.
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
        ///     Disconnect from the local server.
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
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IPackageListener subscriber)
        {
            _packageListeners.Add(subscriber);
        }

        /// <summary>
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IClientListener subscriber)
        {
            _clientListeners.Add(subscriber);
        }

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IPackageListener unsubscriber)
        {
            _packageListeners.Remove(unsubscriber);
        }

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IClientListener unsubscriber)
        {
            _clientListeners.Remove(unsubscriber);
        }

        #endregion

        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    _udpClient.Close();
                }
            }
        }

        #endregion

        private const int IdleMax = 30;
        private readonly List<IClientListener> _clientListeners;
        private readonly List<IPackageListener> _packageListeners;
        private readonly System.Net.Sockets.UdpClient _udpClient;
        private bool _connected;
        private int _currentIdle;
        private int _idleTimeout;
        private IPAddress _ip;

        /// <summary>
        ///     Initializes a new UdpClient class.
        /// </summary>
        public UdpClient()
        {
            _clientListeners = new List<IClientListener>();
            _packageListeners = new List<IPackageListener>();
            _udpClient = new System.Net.Sockets.UdpClient(2565);
        }

        /// <summary>
        ///     Receives data.
        /// </summary>
        private void InternalBeginReceive()
        {
            while (_connected)
            {
                if (_udpClient.Available > 0)
                {
                    var serverIpEndPoint = new IPEndPoint(IPAddress.Any, 2565);
                    byte[] receivedData = _udpClient.Receive(ref serverIpEndPoint);
                    using (var mStream = new MemoryStream(receivedData))
                    {
                        IBasePackage package = PackageSerializer.Deserialize(mStream);

                        var binaryPackage = package as BinaryPackage;
                        if (binaryPackage != null)
                        {
                            //binary package
                            foreach (IPackageListener subscriber in GetPackageSubscriber(binaryPackage.OriginType))
                            {
                                subscriber.OnPackageReceived(binaryPackage);
                            }
                            continue;
                        }

                        //system packages
                        var systemPackage = package as NotificationPackage;
                        if (systemPackage != null)
                        {
                            switch (systemPackage.Mode)
                            {
                                case NotificationMode.ClientJoined:
                                    for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientJoined(systemPackage.Connection[0]);
                                    }
                                    break;
                                case NotificationMode.ClientExited:
                                    for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientExited(systemPackage.Connection[0]);
                                    }
                                    break;
                                case NotificationMode.ClientList:
                                    for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientListing(systemPackage.Connection);
                                    }
                                    break;
                                case NotificationMode.TimeOut:
                                    for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                    {
                                        _clientListeners[i].OnClientTimedOut();
                                    }
                                    break;
                                case NotificationMode.ServerShutdown:
                                    for (int i = 0; i <= _clientListeners.Count - 1; i++)
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
        ///     Gets a list of all matching package listeners.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>List of package listeners</returns>
        private IEnumerable<IPackageListener> GetPackageSubscriber(Type type)
        {
            var listenerContext = new List<IPackageListener>();
            for (int i = 0; i <= _packageListeners.Count - 1; i++)
            {
                //if listener type is null go to next
                if (_packageListeners[i].ListenerType == null)
                {
                    continue;
                }

                if (_packageListeners[i].ListenerType == type)
                {
                    listenerContext.Add(_packageListeners[i]);
                }
            }
            return listenerContext;
        }

        /// <summary>
        ///     Idles the thread.
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