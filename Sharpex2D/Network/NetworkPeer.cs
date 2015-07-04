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
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.InProgress)]
    public class NetworkPeer : IDisposable
    {
        [Developer("ThuCommix", "developer@sharpex2d.de")]
        [TestState(TestState.Tested)]
        public enum Protocol
        {
            /// <summary>
            /// Transmission control protocol.
            /// </summary>
            Tcp,

            /// <summary>
            /// User datagram protocol.
            /// </summary>
            Udp,
        }

        internal static Random Random;
        private readonly SynchronizedList<RemotePeer> _connections;

        private readonly Socket _hostSocket;
        private readonly int _id;
        private readonly Task _peerActivityTask;
        private readonly Protocol _protocol;
        private int _bytesReceived;
        private int _bytesSent;
        private bool _cancel;
        private Socket _clientSocket;
        private UdpClient _udpClient;

        /// <summary>
        /// Initializes a new NetworkPeer class.
        /// </summary>
        static NetworkPeer()
        {
            Random = new Random();
        }

        /// <summary>
        /// Initializes a new NetworkPeer class.
        /// </summary>
        /// <param name="protocol">The Protocol.</param>
        public NetworkPeer(Protocol protocol) : this(protocol, null)
        {
        }

        /// <summary>
        /// Initializes a new NetworkPeer class.
        /// </summary>
        /// <param name="protocol">The Protocol.</param>
        /// <param name="port">The Port.</param>
        public NetworkPeer(Protocol protocol, int port) : this(protocol, new IPEndPoint(IPAddress.Any, port))
        {
        }

        /// <summary>
        /// Initializes a new NetworkPeer class.
        /// </summary>
        /// <param name="protocol">The Protocol.</param>
        /// <param name="ipAddress">The IPAddress.</param>
        /// <param name="port">The Port.</param>
        public NetworkPeer(Protocol protocol, IPAddress ipAddress, int port)
            : this(protocol, new IPEndPoint(ipAddress, port))
        {
        }

        /// <summary>
        /// Initializes a new NetworkPeer class.
        /// </summary>
        /// <param name="protocol">The Protocol.</param>
        /// <param name="localEndPoint">The LocalEndPoint.</param>
        public NetworkPeer(Protocol protocol, IPEndPoint localEndPoint)
        {
            _protocol = protocol;
            LocalEndPoint = localEndPoint;
            _connections = new SynchronizedList<RemotePeer>();
            PeerStatistics = new PeerStatistics();
            _id = Random.Next(Int32.MaxValue);
            MaxReceiveBuffer = 10240;
            _peerActivityTask = new Task(PeerActivityTask);
            _peerActivityTask.Start();
            //if localEndPoint is null, we do not start hosting

            if (localEndPoint != null)
            {
                try
                {
                    if (_protocol == Protocol.Udp)
                    {
                        _udpClient = new UdpClient(localEndPoint);
                        var inValue = new byte[] {0};
                        var outValue = new byte[] {0};
                        _udpClient.Client.IOControl(-1744830452, inValue, outValue);
                        _udpClient.BeginReceive(OnUdpDataArrived, null);
                    }
                    else
                    {
                        _hostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _hostSocket.Bind(localEndPoint);
                        _hostSocket.Listen(200);
                        _hostSocket.BeginAccept(OnAcceptSockets, null);
                    }
                }
                catch (Exception e)
                {
                    throw new IPEndPointBindException("Unable to bind the specified IPEndPoint.", e);
                }
            }
        }

        /// <summary>
        /// Gets the local end point.
        /// </summary>
        public IPEndPoint LocalEndPoint { private set; get; }

        /// <summary>
        /// Gets the peer statistics.
        /// </summary>
        public PeerStatistics PeerStatistics { private set; get; }

        /// <summary>
        /// Gets the available connections.
        /// </summary>
        public RemotePeer[] Connections
        {
            get { return _connections.Where(x => x.IsConnected).ToArray(); }
        }

        /// <summary>
        /// Gets or sets the maximum receive buffer.
        /// </summary>
        public int MaxReceiveBuffer { set; get; }

        /// <summary>
        /// Gets the remote ip end point.
        /// </summary>
        public IPEndPoint RemoteEndPoint { private set; get; }

        /// <summary>
        /// Gets the latency.
        /// </summary>
        public int Latency { private set; get; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Raises when data arrives.
        /// </summary>
        public event EventHandler<IncomingMessageEventArgs> MessageArrived;

        /// <summary>
        /// Raises when a remote peer joined.
        /// </summary>
        public event EventHandler<PeerJoinedEventArgs> PeerJoined;

        /// <summary>
        /// Raises when a remote peer disconnected.
        /// </summary>
        public event EventHandler<PeerDisconnectedEventArgs> PeerDisconnected;

        /// <summary>
        /// Deconstructs the NetworkPeer class.
        /// </summary>
        ~NetworkPeer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancel = true;
                Disconnect();
            }
        }

        /// <summary>
        /// Closes the network peer.
        /// </summary>
        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Connects the peer to another.
        /// </summary>
        /// <param name="ipEndPoint">The IPEndPoint.</param>
        public void Connect(IPEndPoint ipEndPoint)
        {
            RemoteEndPoint = ipEndPoint;

            if (_protocol == Protocol.Tcp)
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    ReceiveTimeout = 5000
                };

                try
                {
                    _clientSocket.Connect(ipEndPoint);
                }
                catch
                {
                    throw new ConnectionRefusedException("The host refused the connection.");
                }

                var data = new byte[2];
                _clientSocket.Receive(data);

                if ((PeerProtocol) data[0] != PeerProtocol.Connected)
                {
                    throw new ConnectionRefusedException("The host refused the connection.");
                }


                var remotePeer = new RemotePeer
                {
                    Socket = _clientSocket,
                    RemoteEndPoint = RemoteEndPoint,
                    IsConnected = true
                };

                _connections.Add(remotePeer);

                if (!_cancel)
                {
                    _clientSocket.BeginReceive(remotePeer.HBuffer, 0, 4, SocketFlags.None, OnTcpDataArrvied, remotePeer);
                }
            }
            else
            {
                _udpClient = new UdpClient {Client = {ReceiveTimeout = 5000}};
                _udpClient.Send(new byte[] {(byte) PeerProtocol.Join, 0}, 2, ipEndPoint);

                IPEndPoint remoteEndPoint = null;
                byte[] data = _udpClient.Receive(ref remoteEndPoint);

                if ((PeerProtocol) data[0] != PeerProtocol.Connected)
                {
                    throw new ConnectionRefusedException("The host refused the connection.");
                }

                _connections.Add(new RemotePeer {RemoteEndPoint = ipEndPoint, IsConnected = true});

                if (!_cancel)
                {
                    _udpClient.BeginReceive(OnUdpDataArrived, null);
                }
            }
        }

        /// <summary>
        /// Disconnects the peer.
        /// </summary>
        public void Disconnect()
        {
            if (_protocol == Protocol.Tcp)
            {
                if (_clientSocket != null)
                {
                    if (_clientSocket.Connected)
                    {
                        _clientSocket.Close();
                    }
                }
            }
            else
            {
                _udpClient.BeginSend(new byte[] {(byte) PeerProtocol.Left, 0}, 2, RemoteEndPoint, OnUdpSent, null);
                _udpClient.Close();
            }
        }

        /// <summary>
        /// Raises when the socket accepts incomming sockets.
        /// </summary>
        /// <param name="ar">The AsyncResult.</param>
        private void OnAcceptSockets(IAsyncResult ar)
        {
            Socket socket = _hostSocket.EndAccept(ar);
            var remotePeer = new RemotePeer {RemoteEndPoint = (IPEndPoint) socket.RemoteEndPoint, Socket = socket};
            var joinedArgs = new PeerJoinedEventArgs(remotePeer);

            if (PeerJoined != null)
            {
                PeerJoined(this, joinedArgs);
            }

            if (joinedArgs.Cancel)
            {
                try
                {
                    socket.BeginSend(new byte[] {(byte) PeerProtocol.Disconnected, 0}, 0, 2, SocketFlags.None, OnTcpSent,
                        remotePeer);
                    socket.LingerState = new LingerOption(true, 4);
                    socket.Disconnect(false);
                }
                catch
                {
                    //If the client suddenly disappears dont crash the peer
                }
            }
            else
            {
                try
                {
                    socket.BeginSend(new byte[] {(byte) PeerProtocol.Connected, 0}, 0, 2, SocketFlags.None, OnTcpSent,
                        remotePeer);
                    _connections.Add(remotePeer);
                    remotePeer.IsConnected = true;

                    if (!_cancel)
                    {
                        socket.BeginReceive(remotePeer.HBuffer, 0, 4, SocketFlags.None, OnTcpDataArrvied, remotePeer);
                    }
                }
                catch
                {
                    //If the client suddenly disappears dont crash the peer
                }
            }

            if (!_cancel)
            {
                _hostSocket.BeginAccept(OnAcceptSockets, null);
            }
        }

        /// <summary>
        /// Raises when the udp client received data.
        /// </summary>
        /// <param name="ar">The AsyncResult.</param>
        private void OnUdpDataArrived(IAsyncResult ar)
        {
            IPEndPoint remoteEndPoint = null;
            byte[] data;
            try
            {
                data = _udpClient.EndReceive(ar, ref remoteEndPoint);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            PeerStatistics.PacketsReceived++;
            PeerStatistics.TotalBytesReceived += data.Length;
            _bytesReceived += data.Length;

            if (data.Length >= 2)
            {
                var protocol0 = (PeerProtocol) data[0];
                var protocol1 = (PeerProtocol) data[1];

                if (_connections.Any(x => x.RemoteEndPoint.Equals(remoteEndPoint)))
                {
                    RemotePeer remotePeer = _connections.First(x => x.RemoteEndPoint.Equals(remoteEndPoint));

                    if (data.Length > MaxReceiveBuffer)
                    {
                        remotePeer.IsConnected = false;
                        _connections.Remove(remotePeer);

                        //send disconnect
                        _udpClient.BeginSend(new byte[] {(byte) PeerProtocol.Disconnected, 0}, 2, OnUdpSent, null);

                        if (PeerDisconnected != null)
                        {
                            PeerDisconnected(this,
                                new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.ProtocolViolation));
                        }

                        return;
                    }

                    //the remote peer is already connected.
                    switch (protocol0)
                    {
                        case PeerProtocol.Data:

                            if (MessageArrived != null)
                            {
                                var msg = new byte[data.Length - 2];
                                Array.Copy(data, 2, msg, 0, msg.Length);
                                var incomingMessage = new IncomingMessage(msg, remotePeer);
                                MessageArrived(this, new IncomingMessageEventArgs(incomingMessage));
                            }

                            break;
                        case PeerProtocol.Ping:

                            if (data.Length == 14)
                            {
                                int id = BitConverter.ToInt32(data, 2);
                                if (id == _id)
                                {
                                    //we received our ping packet back
                                    long timestamp = BitConverter.ToInt64(data, 6);
                                    remotePeer.Latency = TimeSpan.FromTicks(DateTime.Now.Ticks - timestamp).Milliseconds;
                                    remotePeer.TicksWithoutResponse = 0;

                                    if (remotePeer.RemoteEndPoint.Equals(RemoteEndPoint))
                                    {
                                        Latency = remotePeer.Latency;
                                    }
                                }
                                else
                                {
                                    //send the ping packet to the sender
                                    _udpClient.BeginSend(data, 14, remotePeer.RemoteEndPoint, OnUdpDataArrived, null);
                                }
                            }

                            break;
                        case PeerProtocol.Left:

                            _connections.Remove(remotePeer);
                            remotePeer.IsConnected = false;

                            if (PeerDisconnected != null)
                            {
                                PeerDisconnected(this,
                                    new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.Disconnected));
                            }

                            break;
                    }
                }
                else
                {
                    switch (protocol0)
                    {
                        case PeerProtocol.Join:

                            var remotePeer = new RemotePeer {RemoteEndPoint = remoteEndPoint};
                            var joinedArgs = new PeerJoinedEventArgs(remotePeer);

                            if (PeerJoined != null)
                            {
                                PeerJoined(this, joinedArgs);
                            }

                            if (joinedArgs.Cancel)
                            {
                                //send message for disconnect

                                _udpClient.BeginSend(new byte[] {(byte) PeerProtocol.Disconnected, 0}, 2, OnUdpSent,
                                    null);
                            }
                            else
                            {
                                //send message for successfully connect

                                _udpClient.BeginSend(new byte[] {(byte) PeerProtocol.Connected, 0}, 2, remoteEndPoint,
                                    OnUdpSent,
                                    null);

                                remotePeer.IsConnected = true;
                                _connections.Add(remotePeer);
                            }

                            break;
                    }
                }
            }

            if (!_cancel)
            {
                _udpClient.BeginReceive(OnUdpDataArrived, null);
            }
        }

        /// <summary>
        /// Raises when the socket received data.
        /// </summary>
        /// <param name="ar">The AsyncResult.</param>
        private void OnTcpDataArrvied(IAsyncResult ar)
        {
            var remotePeer = (RemotePeer) ar.AsyncState;
            int bytesReceived;
            try
            {
                bytesReceived = remotePeer.Socket.EndReceive(ar);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (SocketException)
            {
                remotePeer.IsConnected = false;
                _connections.Remove(remotePeer);
                if (PeerDisconnected != null)
                {
                    PeerDisconnected(this, new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.Disconnected));
                }
                return;
            }

            if (bytesReceived >= 2)
            {
                PeerStatistics.PacketsReceived++;
                PeerStatistics.TotalBytesReceived += bytesReceived;
                _bytesReceived += bytesReceived;

                if (!remotePeer.HeaderSent)
                {
                    int dataLength = BitConverter.ToInt32(remotePeer.HBuffer, 0);
                    if (dataLength < 1 || dataLength > MaxReceiveBuffer)
                    {
                        remotePeer.IsConnected = false;
                        _connections.Remove(remotePeer);
                        remotePeer.Socket.LingerState = new LingerOption(true, 4);
                        remotePeer.Socket.Disconnect(false);
                        if (PeerDisconnected != null)
                        {
                            PeerDisconnected(this,
                                new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.ProtocolViolation));
                        }

                        return;
                    }
                    remotePeer.MBuffer = new byte[dataLength];
                    remotePeer.HeaderSent = true;

                    if (!_cancel)
                    {
                        try
                        {
                            remotePeer.Socket.BeginReceive(remotePeer.MBuffer, 0, dataLength, SocketFlags.None,
                                OnTcpDataArrvied,
                                remotePeer);
                        }
                        catch (SocketException)
                        {
                            remotePeer.IsConnected = false;
                            _connections.Remove(remotePeer);

                            if (PeerDisconnected != null)
                            {
                                PeerDisconnected(this,
                                    new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.Disconnected));
                            }
                        }
                    }
                }
                else
                {
                    var protocol0 = (PeerProtocol) remotePeer.MBuffer[0];
                    var protocol1 = (PeerProtocol) remotePeer.MBuffer[1];

                    switch (protocol0)
                    {
                        case PeerProtocol.Data:

                            if (MessageArrived != null)
                            {
                                var msg = new byte[remotePeer.MBuffer.Length - 2];
                                Array.Copy(remotePeer.MBuffer, 2, msg, 0, msg.Length);
                                var incomingMessage = new IncomingMessage(msg, remotePeer);
                                MessageArrived(this, new IncomingMessageEventArgs(incomingMessage));
                            }

                            break;
                        case PeerProtocol.Ping:

                            if (remotePeer.MBuffer.Length == 14)
                            {
                                int id = BitConverter.ToInt32(remotePeer.MBuffer, 2);
                                if (id == _id)
                                {
                                    //we received our ping packet back
                                    long timestamp = BitConverter.ToInt64(remotePeer.MBuffer, 6);
                                    remotePeer.Latency = TimeSpan.FromTicks(DateTime.Now.Ticks - timestamp).Milliseconds;
                                    remotePeer.TicksWithoutResponse = 0;

                                    if (remotePeer.RemoteEndPoint.Equals(RemoteEndPoint))
                                    {
                                        Latency = remotePeer.Latency;
                                    }
                                }
                                else
                                {
                                    //send the ping packet to the sender
                                    try
                                    {
                                        remotePeer.Socket.BeginSend(BitConverter.GetBytes(remotePeer.MBuffer.Length), 0,
                                            4, SocketFlags.None,
                                            OnTcpSent, remotePeer);
                                        remotePeer.Socket.BeginSend(remotePeer.MBuffer, 0, remotePeer.MBuffer.Length,
                                            SocketFlags.None, OnTcpSent, remotePeer);
                                    }
                                    catch (SocketException)
                                    {
                                        remotePeer.IsConnected = false;
                                        _connections.Remove(remotePeer);

                                        if (PeerDisconnected != null)
                                        {
                                            PeerDisconnected(this,
                                                new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.Disconnected));
                                        }
                                    }
                                }
                            }

                            break;
                    }

                    if (!_cancel)
                    {
                        try
                        {
                            remotePeer.HeaderSent = false;
                            remotePeer.Socket.BeginReceive(remotePeer.HBuffer, 0, 4, SocketFlags.None, OnTcpDataArrvied,
                                remotePeer);
                        }
                        catch (SocketException)
                        {
                            remotePeer.IsConnected = false;
                            _connections.Remove(remotePeer);

                            if (PeerDisconnected != null)
                            {
                                PeerDisconnected(this,
                                    new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.Disconnected));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sends data.
        /// </summary>
        /// <param name="outgoingMessage">The OutgoingMessage.</param>
        public void Send(OutgoingMessage outgoingMessage)
        {
            foreach (RemotePeer remotePeer in _connections)
            {
                Send(outgoingMessage, remotePeer);
            }
        }

        /// <summary>
        /// Sends data.
        /// </summary>
        /// <param name="outgoingMessage">The OutgoingMessage.</param>
        /// <param name="remotePeer">The RemotePeer.</param>
        public void Send(OutgoingMessage outgoingMessage, RemotePeer remotePeer)
        {
            if (_protocol == Protocol.Tcp)
            {
                if (remotePeer.IsConnected && remotePeer.Socket != null)
                {
                    try
                    {
                        remotePeer.Socket.BeginSend(BitConverter.GetBytes(outgoingMessage.Size), 0, 4, SocketFlags.None,
                            OnTcpSent, remotePeer);
                        remotePeer.Socket.BeginSend(outgoingMessage.Data, 0, outgoingMessage.Size, SocketFlags.None,
                            OnTcpSent, remotePeer);
                    }
                    catch
                    {
                        remotePeer.IsConnected = false;
                        throw new RemotePeerNotAvailableException();
                    }
                }
            }
            else
            {
                if (remotePeer.IsConnected)
                {
                    try
                    {
                        _udpClient.BeginSend(outgoingMessage.Data, outgoingMessage.Size, remotePeer.RemoteEndPoint,
                            OnUdpSent, null);
                    }
                    catch (ObjectDisposedException)
                    {
                        remotePeer.IsConnected = false;
                        throw new RemotePeerNotAvailableException();
                    }
                }
            }
        }

        /// <summary>
        /// Raises when the udp client finished sending.
        /// </summary>
        /// <param name="ar">The AsyncResult.</param>
        private void OnUdpSent(IAsyncResult ar)
        {
            try
            {
                int bytesSent = _udpClient.EndSend(ar);

                PeerStatistics.TotalBytesSent += bytesSent;
                PeerStatistics.PacketsSent++;
                _bytesSent += bytesSent;
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        /// Raises when the socket finished sending.
        /// </summary>
        /// <param name="ar">The AsyncResult.</param>
        private void OnTcpSent(IAsyncResult ar)
        {
            var remotePeer = (RemotePeer) ar.AsyncState;
            int bytesSent = remotePeer.Socket.EndSend(ar);

            PeerStatistics.TotalBytesSent += bytesSent;
            PeerStatistics.PacketsSent++;
            _bytesSent += bytesSent;
        }

        /// <summary>
        /// Performs peer activities such as pinging.
        /// </summary>
        private void PeerActivityTask()
        {
            while (!_cancel)
            {
                _peerActivityTask.Wait(2000);

                PeerStatistics.Lifetime += TimeSpan.FromSeconds(2);
                PeerStatistics.BytesReceivedPerSecond = _bytesReceived/2;
                PeerStatistics.BytesSentPerSecond = _bytesSent/2;
                _bytesReceived = 0;
                _bytesSent = 0;

                OutgoingMessage pingMessage = CreatePingMessage();

                for (int i = 0; i < _connections.Count; i++)
                {
                    RemotePeer remotePeer = _connections[i];

                    if (remotePeer.TicksWithoutResponse >= 8)
                    {
                        _connections.Remove(remotePeer);
                        remotePeer.IsConnected = false;

                        if (PeerDisconnected != null)
                        {
                            PeerDisconnected(this, new PeerDisconnectedEventArgs(remotePeer, DisconnectReason.TimeOut));
                        }
                    }
                    else
                    {
                        remotePeer.TicksWithoutResponse++;
                        try
                        {
                            Send(pingMessage, remotePeer);
                        }
                        catch (RemotePeerNotAvailableException)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new ping message.
        /// </summary>
        /// <returns>OutgoingMessage.</returns>
        private OutgoingMessage CreatePingMessage()
        {
            var msgBuffer = new byte[12];
            Array.Copy(BitConverter.GetBytes(_id), 0, msgBuffer, 0, 4);
            Array.Copy(BitConverter.GetBytes(DateTime.Now.Ticks), 0, msgBuffer, 4, 8);
            return new OutgoingMessage(msgBuffer, PeerProtocol.Ping);
        }
    }
}