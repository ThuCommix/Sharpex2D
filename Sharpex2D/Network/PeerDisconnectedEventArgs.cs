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

namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class PeerDisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new PeerDisconnectedEventArgs class.
        /// </summary>
        /// <param name="peer">The RemotePeer.</param>
        internal PeerDisconnectedEventArgs(RemotePeer peer)
            : this(peer, DisconnectReason.Unknown)
        {
        }

        /// <summary>
        /// Initializes a new PeerDisconnectedEventArgs class.
        /// </summary>
        /// <param name="peer">The RemotePeer.</param>
        /// <param name="reason">The Reason.</param>
        internal PeerDisconnectedEventArgs(RemotePeer peer, DisconnectReason reason)
        {
            RemotePeer = peer;
            Reason = reason;
        }

        /// <summary>
        /// Gets the joined remote peer.
        /// </summary>
        public RemotePeer RemotePeer { private set; get; }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        public DisconnectReason Reason { private set; get; }
    }
}