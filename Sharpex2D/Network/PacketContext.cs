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
using System.IO;

namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class PacketContext : IDisposable
    {
        private readonly MemoryStream _packetStream;
        private readonly byte[] _readBuffer;

        /// <summary>
        /// Initializes a new PacketContext class.
        /// </summary>
        /// <param name="identifer">The Identifer from the Packet.</param>
        internal PacketContext(short identifer)
        {
            _packetStream = new MemoryStream();
            Identifer = identifer;
            CanWrite = true;
            _readBuffer = new byte[8];
            _packetStream.Write(BitConverter.GetBytes(identifer), 0, 2);
        }

        /// <summary>
        /// Initializes a new PacketContext class.
        /// </summary>
        /// <param name="data">The Data.</param>
        internal PacketContext(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length == 0)
                throw new ArgumentException("The data must atleast contains 1 byte.");

            _packetStream = new MemoryStream(data);
            _readBuffer = new byte[8];
            CanWrite = false;
            _packetStream.Read(_readBuffer, 0, 2);
            Identifer = BitConverter.ToInt16(_readBuffer, 0);
        }

        /// <summary>
        /// Gets the Length of the PacketContext in bytes.
        /// </summary>
        public long Length
        {
            get { return _packetStream.Length; }
        }

        /// <summary>
        /// Gets the Identifer.
        /// </summary>
        public short Identifer { get; private set; }

        /// <summary>
        /// A value indicating whether the PacketContext can write.
        /// </summary>
        public bool CanWrite { get; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref byte value)
        {
            if (CanWrite)
            {
                _packetStream.Write(new[] {value}, 0, 1);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 1);
                value = _readBuffer[0];
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref bool value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 1);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 1);
                value = BitConverter.ToBoolean(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref short value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 2);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 2);
                value = BitConverter.ToInt16(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref int value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 4);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 4);
                value = BitConverter.ToInt32(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref long value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 8);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 8);
                value = BitConverter.ToInt64(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref ushort value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 2);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 2);
                value = BitConverter.ToUInt16(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref uint value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 4);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 4);
                value = BitConverter.ToUInt32(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref ulong value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 8);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 8);
                value = BitConverter.ToUInt64(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref float value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 4);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 4);
                value = BitConverter.ToSingle(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref double value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 8);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 8);
                value = BitConverter.ToDouble(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref char value)
        {
            if (CanWrite)
            {
                _packetStream.Write(BitConverter.GetBytes(value), 0, 2);
            }
            else
            {
                _packetStream.Read(_readBuffer, 0, 2);
                value = BitConverter.ToChar(_readBuffer, 0);
            }
        }

        /// <summary>
        /// Serialize a value into the PacketContext.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Serialize(ref byte[] value)
        {
            if (CanWrite)
            {
                _packetStream.Write(value, 0, value.Length);
            }
            else
            {
                _packetStream.Read(value, 0, value.Length);
            }
        }

        /// <summary>
        /// Gets the bytes of the PacketContext.
        /// </summary>
        /// <returns>ByteArray.</returns>
        public byte[] GetBytes()
        {
            return _packetStream.ToArray();
        }

        /// <summary>
        /// Deconstructs the PacketContext class.
        /// </summary>
        ~PacketContext()
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
                _packetStream.Dispose();
            }
        }
    }
}