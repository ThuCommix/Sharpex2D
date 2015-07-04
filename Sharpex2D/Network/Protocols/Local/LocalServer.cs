// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Sharpex2D.Network.Logic;
using Sharpex2D.Network.Packages;
using Sharpex2D.Network.Packages.System;

namespace Sharpex2D.Network.Protocols.Local
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class LocalServer : IServer
    {
        private const int IdleMax = 30;
        private readonly List<LocalConnection> _connections;
        private readonly TcpListener _localListener;
        private readonly List<IPackageListener> _packageListeners;
        private int _currentIdle;
        private int _idleTimeout;

        /// <summary>
        /// Initializes a new LocalServer class.
        /// </summary>
        public LocalServer()
        {
            _connections = new List<LocalConnection>();
            _packageListeners = new List<IPackageListener>();
            _localListener = new TcpListener(new IPEndPoint(IPAddress.Any, 2563));
            _localListener.Start();
            TimeOutLatency = 200.0f;
            IsActive = true;
            //Listen on Connections.
            var connectionHandler = new Thread(BeginAcceptConnections) {IsBackground = true};
            connectionHandler.Start();
            var pingLoopHandler = new Thread(PingRequestLoop) {IsBackground = true};
            pingLoopHandler.Start();
        }

        /// <summary>
        /// Sets or gets the TimeOutLatency, if a client latency is higher than this value, the client is going to be
        /// disconnected.
        /// </summary>
        public float TimeOutLatency { set; get; }

        /// <summary>
        /// Accepts clients if available.
        /// </summary>
        private void BeginAcceptConnections()
        {
            while (IsActive)
            {
                TcpClient tcpClient = _localListener.AcceptTcpClient();
                //Reset idle
                _idleTimeout = 0;
                _currentIdle = 0;
                var localConnection = new LocalConnection(tcpClient);
                _connections.Add(localConnection);
                //Handle connection.
                var pts = new ParameterizedThreadStart(HandleClient);
                var handleClient = new Thread(pts) {IsBackground = true};
                SendNotificationPackage(NotificationMode.ClientJoined,
                    new IConnection[] {SerializableConnection.FromIConnection(localConnection)});
                IConnection[] connectionList = SerializableConnection.FromIConnection(_connections.ToArray());
                SendNotificationPackage(NotificationMode.ClientList, connectionList);
                handleClient.Start(localConnection);
            }
        }

        /// <summary>
        /// Handles a connection.
        /// </summary>
        /// <param name="objConnection">The Connection.</param>
        private void HandleClient(object objConnection)
        {
            var localConnection = (LocalConnection) objConnection;
            NetworkStream networkStream = localConnection.Client.GetStream();
            while (localConnection.Connected)
            {
                if (localConnection.Client.Available > 0)
                {
                    //Reset idle
                    _idleTimeout = 0;
                    _currentIdle = 0;
                    IBasePackage package = PackageSerializer.Deserialize(networkStream);
                    var binaryPackage = package as BinaryPackage;
                    if (binaryPackage != null)
                    {
                        //notify package listeners
                        foreach (IPackageListener subscriber in GetPackageSubscriber(binaryPackage.OriginType))
                        {
                            subscriber.OnPackageReceived(binaryPackage);
                        }

                        //The package is not a system package, send it to it's destination
                        if (binaryPackage.Receiver == null)
                        {
                            //Send to all Clients
                            Send(binaryPackage);
                        }
                        else
                        {
                            //Special destination
                            Send(binaryPackage, binaryPackage.Receiver);
                        }
                        return;
                    }

                    //system package with type of pingpackage
                    var pingPackage = package as PingPackage;
                    if (pingPackage != null)
                    {
                        SetLatency(pingPackage);
                        return;
                    }
                }
                else
                {
                    Idle();
                }
            }

            //Client exited.
            SendNotificationPackage(NotificationMode.ClientExited,
                new IConnection[] {SerializableConnection.FromIConnection(localConnection)});
            _connections.Remove(localConnection);

            IConnection[] connectionList = SerializableConnection.FromIConnection(_connections.ToArray());
            SendNotificationPackage(NotificationMode.ClientList, connectionList);
        }

        /// <summary>
        /// Sets the latency of a connection.
        /// </summary>
        /// <param name="pingPackage">The PingPackage.</param>
        private void SetLatency(PingPackage pingPackage)
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan dif = timeNow - pingPackage.TimeStamp;
            LocalConnection connection = GetConnection(pingPackage.Receiver);
            connection.Latency = (float) dif.TotalMilliseconds;

            //Kick the client if the latency is to high
            if (!(connection.Latency > TimeOutLatency)) return;
            SendNotificationPackage(NotificationMode.TimeOut,
                new IConnection[] {SerializableConnection.FromIConnection(connection)});
            connection.Client.Close();
            _connections.Remove(connection);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="address">The IPAddress.</param>
        /// <returns>LocalConnection</returns>
        private LocalConnection GetConnection(IPAddress address)
        {
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                if (Equals(_connections[i].IPAddress, address))
                {
                    return _connections[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Sends a package to all clients.
        /// </summary>
        /// <param name="package">The Package.</param>
        private void SendToAllClients(IBasePackage package)
        {
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                NetworkStream networkStream = _connections[i].Client.GetStream();
                PackageSerializer.Serialize(package, networkStream);
                //May not be neccessary
                networkStream.Flush();
            }
        }

        /// <summary>
        /// Sends a package to a specified client.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="connection">The Connection</param>
        private void SendTo(IBasePackage package, IPAddress connection)
        {
            LocalConnection localConnection = GetConnection(connection);
            if (localConnection != null)
            {
                PackageSerializer.Serialize(package, localConnection.Client.GetStream());
            }
        }

        /// <summary>
        /// Sending a ping request every 30 seconds to all clients.
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
                    NetworkStream networkStream = _connections[i].Client.GetStream();
                    PackageSerializer.Serialize(pingPackage, networkStream);
                    //May not be neccessary
                    networkStream.Flush();

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
            for (int i = 0; i <= _connections.Count - 1; i++)
            {
                NetworkStream networkStream = _connections[i].Client.GetStream();
                PackageSerializer.Serialize(notificationPackage, networkStream);
                //May not be neccessary
                networkStream.Flush();
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

        /// <summary>
        /// Closes the server.
        /// </summary>
        public void Close()
        {
            SendNotificationPackage(NotificationMode.ServerShutdown, null);
            _localListener.Stop();
        }

        /// <summary>
        /// Gets a list of all matching package listeners.
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

        #region IServer Implementation

        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void Send(IBasePackage package)
        {
            SendToAllClients(package);
        }

        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IPAddress receiver)
        {
            SendTo(package, receiver);
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
    }
}