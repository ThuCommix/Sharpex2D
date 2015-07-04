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

using System.Net;
using System.Net.Sockets;

namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class RemotePeer
    {
        /// <summary>
        /// Initializes a new RemotePeer class.
        /// </summary>
        internal RemotePeer()
        {
            HBuffer = new byte[4];
        }

        /// <summary>
        /// Gets the remote ip end point.
        /// </summary>
        public IPEndPoint RemoteEndPoint { internal set; get; }

        /// <summary>
        /// Gets the latency.
        /// </summary>
        public int Latency { internal set; get; }

        /// <summary>
        /// A value indicating whether the peer is connected.
        /// </summary>
        public bool IsConnected { internal set; get; }

        /// <summary>
        /// Gets or sets the underlaying socket.
        /// </summary>
        internal Socket Socket { get; set; }

        /// <summary>
        /// Gets or sets the header buffer.
        /// </summary>
        internal byte[] HBuffer { set; get; }

        /// <summary>
        /// Gets or sets the message buffer.
        /// </summary>
        internal byte[] MBuffer { set; get; }

        /// <summary>
        /// A value indicating whether the header was sent.
        /// </summary>
        internal bool HeaderSent { set; get; }

        /// <summary>
        /// Gets or sets the ticks without response.
        /// </summary>
        internal int TicksWithoutResponse { set; get; }

        /// <summary>
        /// Gets or sets the Tag.
        /// </summary>
        public object Tag { get; set; }
    }
}