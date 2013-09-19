using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpexGL.Framework.Network.Protocols.Tcp
{
    public class TcpServer : IServerProtocol
    {
        #region IServerProtocol Implementation
        /// <summary>
        /// Starts hosting on the given Port.
        /// </summary>
        /// <param name="port">The Port.</param>
        public void Host(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Hosted = true;
        }
        /// <summary>
        /// Accepts a Connection.
        /// </summary>
        public void AcceptConnection()
        {
            if (Pending)
            {
                _connections.Add(new TcpConnection(_listener.AcceptTcpClient()));
            }
        }
        /// <summary>
        /// Indicates whether the Server is hosting.
        /// </summary>
        public bool Hosted { get; set; }
        /// <summary>
        /// Sets the Server.
        /// </summary>
        public IServer Server { set; private get; }

        #endregion

        private TcpListener _listener;
        private List<TcpConnection> _connections;

        /// <summary>
        /// Indicates whether, connections are available.
        /// </summary>
        public bool Pending
        {
            get { return _listener != null && _listener.Pending(); }
        }

        /// <summary>
        /// Initializes a new TcpServer class.
        /// </summary>
        /// <param name="serverReference">The IServer Reference.</param>
        public TcpServer(IServer serverReference)
        {
            Server = serverReference;
            _connections = new List<TcpConnection>();
        }
    }
}
