using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols.Local
{
    public class LocalClient : IClient
    {
        #region IClient Implementation
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void Send(IBasePackage package)
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            PackageSerializer.Serialize(package, _nStream);
            _nStream.Flush();
        }
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IPAddress receiver)
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            package.Receiver = receiver;
            PackageSerializer.Serialize(package, _nStream);
            _nStream.Flush();
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        /// <returns></returns>
        public IBasePackage Receive()
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            return _tcpClient.Available > 0 ? PackageSerializer.Deserialize(_nStream) : null;
        }
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        public void Connect(IPAddress ip)
        {
            Task.Factory.StartNew(() =>
            {
                _tcpClient.Connect(new IPEndPoint(ip, 2563));
                _nStream = _tcpClient.GetStream();
            });
        }
        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        public void Disconnect()
        {
            if (_tcpClient.Connected)
            {
                _tcpClient.Close();
            }
        }
        #endregion

        private readonly TcpClient _tcpClient;
        private NetworkStream _nStream;

        public LocalClient()
        {
            _tcpClient = new TcpClient();
        }
    }
}
