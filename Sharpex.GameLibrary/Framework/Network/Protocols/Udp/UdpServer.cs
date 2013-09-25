using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Network.Events;
using SharpexGL.Framework.Network.Logic;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Packages.System;

namespace SharpexGL.Framework.Network.Protocols.Udp
{
    public class UdpServer : IServer
    {
        #region IServer Implementation
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void Send(IBasePackage package)
        {
            byte[] result;
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(package, mStream);
                result = mStream.ToArray();
            }
            for (var i = 0; i <= _connections.Count - 1; i++)
            {
                _listener.Client.SendTo(result, new IPEndPoint(_connections[i].IPAddress, 2563));
            }
        }
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IPAddress receiver)
        {
            byte[] result;
            using (var mStream = new MemoryStream())
            {
                PackageSerializer.Serialize(package, mStream);
                result = mStream.ToArray();
            }
            _listener.Client.SendTo(result, new IPEndPoint(receiver, 2563));
        }
        /// <summary>
        /// A value indicating whether the server is active.
        /// </summary>
        public bool IsActive { get; private set; }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IPackageListener subscriber)
        {
            _packageListeners.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IPackageListener unsubscriber)
        {
            _packageListeners.Remove(unsubscriber);
        }

        #endregion

        private readonly UdpClient _listener;
        private readonly List<IConnection> _connections;
        private readonly List<IPackageListener> _packageListeners;
        private int _idleTimeout;
        private const int IdleMax = 30;
        private int _currentIdle;

        /// <summary>
        /// Sets or gets the TimeOutLatency, if a client latency is higher than this value, the client is going to be disconnected.
        /// </summary>
        public float TimeOutLatency { set; get; }

        /// <summary>
        /// Initializes a new UdpServer class.
        /// </summary>
        public UdpServer()
        {
            _packageListeners = new List<IPackageListener>();
            _connections = new List<IConnection>();
            TimeOutLatency = 500f;
            _listener = new UdpClient(2563);
            IsActive = true;
            var beginHandle = new Thread(BeginAcceptConnections) {IsBackground = true};
            var pingHandle = new Thread(PingRequestLoop) {IsBackground = true};
            beginHandle.Start();
            pingHandle.Start();
        }

        /// <summary>
        /// Accepts clients if available.
        /// </summary>
        private void BeginAcceptConnections()
        {
            var incommingConnection = new IPEndPoint(IPAddress.Any, 2563);

            while (IsActive)
            {
                try
                {
                    if (_listener.Available > 0)
                    {
                        var receivedData = _listener.Receive(ref incommingConnection);
                        using (var mStream = new MemoryStream(receivedData))
                        {
                            var package = PackageSerializer.Deserialize(mStream);
                            var udpPackage = package as UdpPackage;
                            if (udpPackage != null)
                            {
                                if (udpPackage.NotifyMode == UdpNotify.Hi)
                                {
                                    //notify
                                    SendNotificationPackage(NotificationMode.ClientJoined, _connections.ToArray());
                                    //add connection
                                    var udpConnection = new UdpConnection(incommingConnection.Address);
                                    _connections.Add(udpConnection);
                                }
                                else //(UdpNotify.Bye)
                                {
                                    //notify
                                    SendNotificationPackage(NotificationMode.ClientExited, _connections.ToArray());
                                    //remove connection
                                    var udpConnection = GetConnection(incommingConnection.Address);
                                    if (udpConnection != null)
                                    {
                                        _connections.Remove(udpConnection);
                                    }
                                }
                            }
                            else
                            {
                                //any other package handle in new void
                                HandlePackage(package);
                            }

                        }
                    }
                    else
                    {
                        //no data available

                        Idle();
                    }
                }
                catch (Exception ex)
                {
                    SGL.Components.Get<EventManager>().Publish(new UnknownPackageExceptionEvent(ex.Message));
                }
            }
        }

        /// <summary>
        /// Handles the given package.
        /// </summary>
        /// <param name="package">The Package.</param>
        private void HandlePackage(IBasePackage package)
        {
            var binaryPackage = package as BinaryPackage;
            if (binaryPackage != null)
            {
                //notify package listeners
                foreach (var subscriber in GetPackageSubscriber(binaryPackage.OriginType))
                {
                    subscriber.OnPackageReceived(binaryPackage);
                }

                //send the package to its destination
                if (binaryPackage.Receiver == null)
                {
                    Send(binaryPackage);
                }
                else
                {
                    Send(binaryPackage, binaryPackage.Receiver);
                }

                return;
            }

            //system package pingpackage
            var pingPackage = package as PingPackage;
            if (pingPackage != null)
            {
                SetLatency(pingPackage);
            }
        }

        /// <summary>
        /// Sets the latency of a connection.
        /// </summary>
        /// <param name="pingPackage">The PingPackage.</param>
        private void SetLatency(PingPackage pingPackage)
        {
            var timeNow = DateTime.Now;
            var dif = timeNow - pingPackage.TimeStamp;
            var connection = GetConnection(pingPackage.Receiver);
            connection.Latency = (float)dif.TotalMilliseconds;

            //Kick the client if the latency is to high
            if (!(connection.Latency > TimeOutLatency)) return;
            SendNotificationPackage(NotificationMode.TimeOut, new IConnection[] { SerializableConnection.FromIConnection(connection) });
            _connections.Remove(connection);
        }

        /// <summary>
        /// Gets the UpdConnection by the specified IPAddress.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        /// <returns>UpdConnection</returns>
        private UdpConnection GetConnection(IPAddress ipAddress)
        {
            for (var i = 0; i <= _connections.Count - 1; i++)
            {
                if (Equals(_connections[i].IPAddress, ipAddress))
                {
                    return (UdpConnection)_connections[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Sending a ping request every 30 seconds to all clients.
        /// </summary>
        private void PingRequestLoop()
        {
            while (IsActive)
            {
                var connectionList = SerializableConnection.FromIConnection(_connections.ToArray());
                //Send a ping request to all clients
                for (var i = 0; i <= _connections.Count - 1; i++)
                {
                    var pingPackage = new PingPackage { Receiver = _connections[i].IPAddress };
                    Send(pingPackage, _connections[i].IPAddress);

                    //Also update the client list.
                    SendNotificationPackage(NotificationMode.ClientList, connectionList);
                }
                //Idle for 15 seconds
                Thread.Sleep(15000);
            }
        }

        /// <summary>
        /// Sends a NotificationPackage to all clients.
        /// </summary>
        /// <param name="mode">The Mode.</param>
        /// <param name="connections">The ConnectionParams.</param>
        private void SendNotificationPackage(NotificationMode mode, IConnection[] connections)
        {
            var notificationPackage = new NotificationPackage(connections, mode);
            Send(notificationPackage);
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

        /// <summary>
        /// Gets a list of all matching package listeners.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>List of package listeners</returns>
        private IEnumerable<IPackageListener> GetPackageSubscriber(Type type)
        {
            var listenerContext = new List<IPackageListener>();
            for (var i = 0; i <= _packageListeners.Count - 1; i++)
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
        /// Closes the server.
        /// </summary>
        public void Close()
        {
            _listener.Close();
            IsActive = false;
        }
    }
}
