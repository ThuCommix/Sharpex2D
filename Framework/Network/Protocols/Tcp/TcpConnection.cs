using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols.Tcp
{
    public class TcpConnection : IConnection
    {
        #region IConnection Implementation
        /// <summary>
        /// Gets the IP.
        /// </summary>
        public IPAddress IP { get; private set; }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public void Disconnect()
        {
            _tcpClient.Close();
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public IPackage<object> Receive()
        {
            using (var mStream = new MemoryStream())
            {
                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = _tcpClient.Client.Receive(buffer)) != 0)
                {
                    mStream.Write(buffer, 0, bytesRead);
                }
                return (IPackage<Object>)PackageSerializer<object>.Deserialize(mStream);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this is connected.
        /// </summary>
        public bool Connected {
            get { return _tcpClient != null && _tcpClient.Connected; }
        }

        #endregion

        private TcpClient _tcpClient;

        /// <summary>
        /// Initializes a new TcpConnection class.
        /// </summary>
        /// <param name="tcpClient">The TcpClient.</param>
        public TcpConnection(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            IP = ((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address;
        }
    }
}
