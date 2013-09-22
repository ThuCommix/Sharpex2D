using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharpexGL.Framework.Network.Packages;

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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IConnection receiver)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        /// <returns></returns>
        public IBasePackage Receive()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// A value indicating whether the server is active.
        /// </summary>
        public bool IsActive { get; private set; }

        #endregion


        private List<LocalConnection> _connections;
        private TcpListener _localListener;

        public LocalServer()
        {
            _connections = new List<LocalConnection>();
            _localListener = new TcpListener(new IPEndPoint(IPAddress.Any, 2563));
            _localListener.Start();
            IsActive = true;
            //Listen on Connections.
            var connectionHandler = new Thread(BeginAcceptConnections) {IsBackground = true};
            connectionHandler.Start();
        }

        private void BeginAcceptConnections()
        {
            while (IsActive)
            {
                if (_localListener.Pending())
                {
                    var tcpClient = _localListener.AcceptTcpClient();
                    var localConnection = new LocalConnection(tcpClient);
                    _connections.Add(localConnection);
                    //TODO: Handle Connection in new Thread
                }
                else
                {
                    //Idle to save cpu power.
                    Thread.Sleep(1);
                }
            }
        }
    }
}
