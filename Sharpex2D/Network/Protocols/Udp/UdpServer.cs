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
using Sharpex2D.Debug.Logging;
using Sharpex2D.Network.Logic;
using Sharpex2D.Network.Packages;
using Sharpex2D.Network.Packages.System;

namespace Sharpex2D.Network.Protocols.Udp
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class UdpServer : IServer, IDisposable
    {
        #region IServer Implementation

        /// <summary>
        ///     Sends a package to the given receivers.
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
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                _listener.Client.SendTo(result, new IPEndPoint(_connections[i].IPAddress, 2565));
            }
        }

        /// <summary>
        ///     Sends a package to the given receivers.
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
            _listener.Client.SendTo(result, new IPEndPoint(receiver, 2565));
        }

        /// <summary>
        ///     A value indicating whether the server is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IPackageListener subscriber)
        {
            _packageListeners.Add(subscriber);
        }

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IPackageListener unsubscriber)
        {
            _packageListeners.Remove(unsubscriber);
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
                    _listener.Close();
                }
            }
        }

        #endregion

        private const int IdleMax = 30;
        private readonly UdpConnectionManager _connectionManager;
        private readonly List<IConnection> _connections;
        private readonly System.Net.Sockets.UdpClient _listener;
        private readonly Logger _logger;
        private readonly List<IPackageListener> _packageListeners;
        private int _currentIdle;
        private int _idleTimeout;

        /// <summary>
        ///     Initializes a new UdpServer class.
        /// </summary>
        public UdpServer()
        {
            _packageListeners = new List<IPackageListener>();
            _connections = new List<IConnection>();
            _logger = LogManager.GetClassLogger();
            _connectionManager = new UdpConnectionManager();
            _connectionManager.PingTimedOut += _connectionManager_PingTimedOut;
            _connectionManager.Start();
            TimeOutLatency = 500f;
            _listener = new System.Net.Sockets.UdpClient(2563);
            IsActive = true;
            var beginHandle = new Thread(BeginAcceptConnections) {IsBackground = true};
            var pingHandle = new Thread(PingRequestLoop) {IsBackground = true};
            beginHandle.Start();
            pingHandle.Start();
        }

        /// <summary>
        ///     Sets or gets the TimeOutLatency, if a client latency is higher than this value, the client is going to be
        ///     disconnected.
        /// </summary>
        public float TimeOutLatency { set; get; }

        /// <summary>
        ///     Called if a PingRequest timed out.
        /// </summary>
        private void _connectionManager_PingTimedOut(object sender, IPAddress ipAddress)
        {
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                if (Equals(_connections[i].IPAddress, ipAddress))
                {
                    //remove the connection
                    _connections.RemoveAt(i);
                    //notify clients
                    SendNotificationPackage(NotificationMode.TimeOut,
                        new IConnection[] {SerializableConnection.FromIConnection(_connections[i])});
                    break;
                }
            }
        }

        /// <summary>
        ///     Accepts clients if available.
        /// </summary>
        private void BeginAcceptConnections()
        {
            var incommingConnection = new IPEndPoint(IPAddress.Any, 2565);

            while (IsActive)
            {
                try
                {
                    if (_listener.Available > 0)
                    {
                        byte[] receivedData = _listener.Receive(ref incommingConnection);
                        using (var mStream = new MemoryStream(receivedData))
                        {
                            IBasePackage package = PackageSerializer.Deserialize(mStream);
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
                                    // publish the new server list, after a new connection.
                                    SendNotificationPackage(NotificationMode.ClientList, _connections.ToArray());
                                }
                                else //(UdpNotify.Bye)
                                {
                                    //notify
                                    SendNotificationPackage(NotificationMode.ClientExited, _connections.ToArray());
                                    //remove connection
                                    UdpConnection udpConnection = GetConnection(incommingConnection.Address);
                                    if (udpConnection != null)
                                    {
                                        _connections.Remove(udpConnection);
                                        // publish the new server list, after a lost connection.
                                        SendNotificationPackage(NotificationMode.ClientList, _connections.ToArray());
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
                    _logger.Error(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Handles the given package.
        /// </summary>
        /// <param name="package">The Package.</param>
        private void HandlePackage(IBasePackage package)
        {
            var binaryPackage = package as BinaryPackage;
            if (binaryPackage != null)
            {
                //notify package listeners
                foreach (IPackageListener subscriber in GetPackageSubscriber(binaryPackage.OriginType))
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
        ///     Sets the latency of a connection.
        /// </summary>
        /// <param name="pingPackage">The PingPackage.</param>
        private void SetLatency(PingPackage pingPackage)
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan dif = timeNow - pingPackage.TimeStamp;
            UdpConnection connection = GetConnection(pingPackage.Receiver);
            connection.Latency = (float) dif.TotalMilliseconds;

            //alert the connectionmanager
            _connectionManager.RemoveByIP(pingPackage.Receiver);

            //Kick the client if the latency is to high
            if (!(connection.Latency > TimeOutLatency)) return;
            SendNotificationPackage(NotificationMode.TimeOut,
                new IConnection[] {SerializableConnection.FromIConnection(connection)});
            _connections.Remove(connection);
        }

        /// <summary>
        ///     Gets the UpdConnection by the specified IPAddress.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        /// <returns>UpdConnection</returns>
        private UdpConnection GetConnection(IPAddress ipAddress)
        {
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                if (Equals(_connections[i].IPAddress, ipAddress))
                {
                    return (UdpConnection) _connections[i];
                }
            }
            return null;
        }

        /// <summary>
        ///     Sending a ping request every 30 seconds to all clients.
        /// </summary>
        private void PingRequestLoop()
        {
            while (IsActive)
            {
                IConnection[] connectionList = SerializableConnection.FromIConnection(_connections.ToArray());
                //Send a ping request to all clients
                for (int i = 0; i <= _connections.Count - 1; i++)
                {
                    var pingPackage = new PingPackage {Receiver = _connections[i].IPAddress};
                    Send(pingPackage, _connections[i].IPAddress);
                    //add the ping request to the connection manager.
                    _connectionManager.AddPingRequest(new UdpPingRequest(_connections[i].IPAddress,
                        pingPackage.TimeStamp));

                    //Also update the client list.
                    SendNotificationPackage(NotificationMode.ClientList, connectionList);
                }
                //Idle for 15 seconds
                Thread.Sleep(15000);
            }
        }

        /// <summary>
        ///     Sends a NotificationPackage to all clients.
        /// </summary>
        /// <param name="mode">The Mode.</param>
        /// <param name="connections">The ConnectionParams.</param>
        private void SendNotificationPackage(NotificationMode mode, IConnection[] connections)
        {
            var notificationPackage = new NotificationPackage(connections, mode);
            Send(notificationPackage);
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
        ///     Closes the server.
        /// </summary>
        public void Close()
        {
            SendNotificationPackage(NotificationMode.ServerShutdown, null);
            _listener.Close();
            _connectionManager.Stop();
            IsActive = false;
        }
    }
}