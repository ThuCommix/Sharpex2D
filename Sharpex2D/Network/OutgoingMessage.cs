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
    public class OutgoingMessage
    {
        /// <summary>
        /// Initializes a new OutgoingMessage class.
        /// </summary>
        /// <param name="data">The Data.</param>
        public OutgoingMessage(byte[] data) : this(data, PeerProtocol.Data)
        {
        }

        /// <summary>
        /// Initializes a new OutgoingMessage class.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="protocol0">The PeerProtocol.</param>
        /// <param name="protocol1">The PeerProtocol.</param>
        internal OutgoingMessage(byte[] data, PeerProtocol protocol0,
            PeerProtocol protocol1 = PeerProtocol.Unknown)
        {
            Data = new byte[data.Length + 2];
            Data[0] = (byte) protocol0;
            Data[1] = (byte) Protocol1;
            Array.Copy(data, 0, Data, 2, data.Length);
            Protocol0 = protocol0;
            Protocol1 = protocol1;
        }

        /// <summary>
        /// Gets the size of the message.
        /// </summary>
        public int Size => Data.Length;

        /// <summary>
        /// Gets the peer protocol.
        /// </summary>
        internal PeerProtocol Protocol0 { private set; get; }

        /// <summary>
        /// Gets the peer protocol.
        /// </summary>
        internal PeerProtocol Protocol1 { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        internal byte[] Data { get; }
    }
}
