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

namespace Sharpex2D.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class SerializableConnection : IConnection
    {
        /// <summary>
        /// Initializes a new SerializableConnection class.
        /// </summary>
        internal SerializableConnection(IPAddress ipaddress, float latency, bool connected)
        {
            IPAddress = ipaddress;
            Latency = latency;
            Connected = connected;
        }

        /// <summary>
        /// Sets or gets the Latency.
        /// </summary>
        public float Latency { get; set; }

        /// <summary>
        /// Sets or gets the IPAddress.
        /// </summary>
        public IPAddress IPAddress { get; private set; }

        /// <summary>
        /// A value indicating whether the connection is still available.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary>
        /// Creates a SerializableConnection from IConnection.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        /// <returns>SerializableConnection</returns>
        public static SerializableConnection FromIConnection(IConnection connection)
        {
            return new SerializableConnection(connection.IPAddress, connection.Latency, connection.Connected);
        }

        /// <summary>
        /// Converts an IConnection array into SerialiableConnection array.
        /// </summary>
        /// <param name="connections">The Connections.</param>
        /// <returns>SerializableConnections</returns>
        public static IConnection[] FromIConnection(IConnection[] connections)
        {
            return connections.Select(FromIConnection).ToArray();
        }
    }
}