
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Packages.System;

namespace SharpexGL.Framework.Network.Protocols.Local
{
    public class LocalServer : IServer
    {

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

        #endregion


        private readonly List<LocalConnection> _connections;
        private readonly TcpListener _localListener;
        private int _idleTimeout;
        private const int IdleMax = 30;
        private int _currentIdle;
        /// <summary>
        /// Sets or gets the TimeOutLatency, if a client latency is higher than this value, the client is going to be disconnected.
        /// </summary>
        public float TimeOutLatency { set; get; }
        /// <summary>
        /// Initializes a new LocalServer class.
        /// </summary>
        public LocalServer()
        {
            _connections = new List<LocalConnection>();
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
        /// Accepts clients if available.
        /// </summary>
        private void BeginAcceptConnections()
        {
            while (IsActive)
            {
                var tcpClient = _localListener.AcceptTcpClient();
                //Reset idle
                _idleTimeout = 0;
                _currentIdle = 0;
                var localConnection = new LocalConnection(tcpClient);
                _connections.Add(localConnection);
                //Handle connection.
                var pts = new ParameterizedThreadStart(HandleClient);
                var handleClient = new Thread(pts) {IsBackground = true};
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
            SendNotificationPackage(NotificationMode.ClientJoined, new IConnection[] { localConnection });
            var networkStream = localConnection.Client.GetStream();
            while (localConnection.Connected)
            {
                if (localConnection.Client.Available > 0)
                {
                    //Reset idle
                    _idleTimeout = 0;
                    _currentIdle = 0;
                    var package = PackageSerializer.Deserialize(networkStream);
                    var binaryPackage = package as BinaryPackage;
                    if (binaryPackage != null)
                    {
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
                        
                    }
                    else
                    {
                        //system package, the only package received by client is pingpackage
                        var pingPackage = (PingPackage) package;
                        SetLatency(pingPackage);
                    }
                }
                else
                {
                    Idle();
                }
            }

            //Client exited.
            SendNotificationPackage(NotificationMode.ClientExited, new IConnection[] {localConnection});
            _connections.Remove(localConnection);
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
            SendNotificationPackage(NotificationMode.TimeOut, new IConnection[] { connection });
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
            for (var i = 0; i <= _connections.Count - 1; i++)
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
            for (var i = 0; i <= _connections.Count - 1; i++)
            {
                var networkStream = _connections[i].Client.GetStream();
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
            var localConnection = GetConnection(connection);
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
                //Send a ping request to all clients
                for (var i = 0; i <= _connections.Count - 1; i++)
                {
                    var pingPackage = new PingPackage {Receiver = _connections[i].IPAddress};
                    var networkStream = _connections[i].Client.GetStream();
                    PackageSerializer.Serialize(pingPackage, networkStream);
                    //May not be neccessary
                    networkStream.Flush();
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
            for (var i = 0; i <= _connections.Count - 1; i++)
            {
                var networkStream = _connections[i].Client.GetStream();
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
    }
}
