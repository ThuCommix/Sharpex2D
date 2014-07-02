// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Text;

namespace Sharpex2D.Framework.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveStream : Stream
    {
        #region Stream Implementation

        /// <summary>
        ///     A value indicating whether the stream can read.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        ///     A value indicating whether the stream can seek.
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        ///     A value indicating whether the stream can write.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        ///     Gets the Length.
        /// </summary>
        public override long Length
        {
            get { return _length; }
        }

        /// <summary>
        ///     Gets the Position.
        /// </summary>
        public override long Position
        {
            get { return _stream.Position - _position; }
            set { Seek(value, SeekOrigin.Begin); }
        }

        /// <summary>
        ///     Closes the Stream.
        /// </summary>
        public override void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">The Disposing State.</param>
        protected override void Dispose(bool disposing)
        {
            Dispose();

            if (disposing)
            {
                _stream.Dispose();
            }
        }

        /// <summary>
        ///     Flushs the Stream.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        ///     Sets the Length.
        /// </summary>
        /// <param name="length">The Length.</param>
        public override void SetLength(long length)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        ///     Seeks the Stream.
        /// </summary>
        /// <param name="position">The Position.</param>
        /// <param name="origin">The SeekOrigin.</param>
        /// <returns>Long.</returns>
        public override long Seek(long position, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _stream.Position = position + _position;
                    break;
                case SeekOrigin.Current:
                    _stream.Seek(position, SeekOrigin.Current);
                    break;
                case SeekOrigin.End:
                    _stream.Position = _position + _length - position;
                    break;
            }

            return Position;
        }

        /// <summary>
        ///     Reads from the Stream.
        /// </summary>
        /// <param name="buffer">The Buffer.</param>
        /// <param name="offset">The Offset.</param>
        /// <param name="count">The Count.</param>
        /// <returns>Int.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var toread = (int) System.Math.Min(count, _length - Position);
            return _stream.Read(buffer, offset, toread);
        }

        /// <summary>
        ///     Writes into the Stream.
        /// </summary>
        /// <param name="buffer">The Buffer.</param>
        /// <param name="offset">The Offset.</param>
        /// <param name="count">The Count.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        #endregion

        private readonly Stream _stream;
        private long _length;
        private long _position;

        /// <summary>
        ///     Initializes a new WaveStream class.
        /// </summary>
        /// <param name="path">The Path.</param>
        public WaveStream(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read))
        {
        }

        /// <summary>
        ///     Initializes a new WaveStream class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        public WaveStream(Stream stream)
        {
            if (!stream.CanRead)
            {
                throw new InvalidOperationException("The given stream restricts reading.");
            }

            _stream = stream;

            ReadHeader();
        }

        /// <summary>
        ///     Gets the WaveFormat.
        /// </summary>
        public WaveFormat WaveFormat { private set; get; }

        /// <summary>
        ///     Reads a chunk.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns>String.</returns>
        private string ReadChunk(BinaryReader reader)
        {
            var ch = new byte[4];
            reader.Read(ch, 0, ch.Length);
            return Encoding.ASCII.GetString(ch);
        }

        /// <summary>
        ///     Reads the Header.
        /// </summary>
        private void ReadHeader()
        {
            var reader = new BinaryReader(_stream);
            if (ReadChunk(reader) != "RIFF")
                throw new InvalidOperationException("Invalid file format.");

            reader.ReadInt32();

            if (ReadChunk(reader) != "WAVE")
                throw new InvalidOperationException("Invalid file format.");

            if (ReadChunk(reader) != "fmt ")
                throw new InvalidOperationException("Invalid file format.");

            int len = reader.ReadInt32();
            if (len < 16)
                throw new InvalidOperationException("Invalid file format.");

            WaveFormat = new WaveFormat(22050, 16, 2)
            {
                wFormatTag = reader.ReadInt16(),
                nChannels = reader.ReadInt16(),
                nSamplesPerSec = reader.ReadInt32(),
                nAvgBytesPerSec = reader.ReadInt32(),
                nBlockAlign = reader.ReadInt16(),
                wBitsPerSample = reader.ReadInt16()
            };

            len -= 16;
            while (len > 0)
            {
                reader.ReadByte();
                len--;
            }

            while (_stream.Position < _stream.Length && ReadChunk(reader) != "data")
            {
            }

            if (_stream.Position >= _stream.Length)
                throw new InvalidOperationException("Invalid file format.");

            _length = reader.ReadInt32();
            _position = _stream.Position;

            Position = 0;
        }
    }
}