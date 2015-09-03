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
    public class PeerStatistics
    {
        private int _packetsReceived;
        private int _packetsSent;
        private int _totalBytesReceived;
        private int _totalBytesSent;

        /// <summary>
        /// Initializes a new PeerStatistics class.
        /// </summary>
        internal PeerStatistics()
        {
            Lifetime = new TimeSpan();
        }

        /// <summary>
        /// Gets the average amount of bytes sent per second.
        /// </summary>
        public int AvgBytesSent
        {
            get
            {
                if (Lifetime.Seconds != 0)
                {
                    return TotalBytesSent/Lifetime.Seconds;
                }
                return TotalBytesSent;
            }
        }

        /// <summary>
        /// Gets the average amount of bytes received per second.
        /// </summary>
        public int AvgBytesReceived
        {
            get
            {
                if (Lifetime.Seconds != 0)
                {
                    return TotalBytesReceived/Lifetime.Seconds;
                }
                return TotalBytesReceived;
            }
        }

        /// <summary>
        /// Gets the average bandwidth.
        /// </summary>
        public int AvgBandwidth => AvgBytesReceived + AvgBytesSent;

        /// <summary>
        /// Gets the amount of packets sent.
        /// </summary>
        public int PacketsSent
        {
            internal set { _packetsSent = value > Int32.MaxValue ? Int32.MaxValue : value; }
            get { return _packetsSent; }
        }

        /// <summary>
        /// Gets the amount of packets received.
        /// </summary>
        public int PacketsReceived
        {
            internal set { _packetsReceived = value > Int32.MaxValue ? Int32.MaxValue : value; }
            get { return _packetsReceived; }
        }

        /// <summary>
        /// Gets the total amount of bytes sent.
        /// </summary>
        public int TotalBytesSent
        {
            internal set { _totalBytesSent = value > Int32.MaxValue ? Int32.MaxValue : value; }
            get { return _totalBytesSent; }
        }

        /// <summary>
        /// Gets the total amount of bytes received.
        /// </summary>
        public int TotalBytesReceived
        {
            internal set { _totalBytesReceived = value > Int32.MaxValue ? Int32.MaxValue : value; }
            get { return _totalBytesReceived; }
        }

        /// <summary>
        /// Gets the amount of bytes sent per second.
        /// </summary>
        public int BytesSentPerSecond { internal set; get; }

        /// <summary>
        /// Gets the amount of bytes received per second.
        /// </summary>
        public int BytesReceivedPerSecond { internal set; get; }

        /// <summary>
        /// Gets the average packet size.
        /// </summary>
        public int AvgPacketSize => ((TotalBytesReceived + TotalBytesSent)/2)/(PacketsReceived + PacketsSent);

        /// <summary>
        /// Gets the lifetime.
        /// </summary>
        public TimeSpan Lifetime { internal set; get; }
    }
}
