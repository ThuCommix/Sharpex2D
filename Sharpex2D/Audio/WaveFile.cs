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
using System.Text;

namespace Sharpex2D.Framework.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveFile : IDisposable
    {
        /// <summary>
        /// Gets the wave header.
        /// </summary>
        public WaveHeader WaveHeader { private set; get; }

        /// <summary>
        /// Gets the wave format.
        /// </summary>
        public WaveFormat WaveFormat { private set; get; }

        private readonly Stream _stream;
        private readonly long _offset;

        /// <summary>
        /// Initializes a new WaveFile class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        public WaveFile(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException();
            if (!stream.CanRead)
                throw new ArgumentException("The stream is not readable.");

            _stream = stream;

            var waveHeader = new WaveHeader();

            try
            {
                var br = new BinaryReader(stream);

                waveHeader.RiffId = br.ReadBytes(4);
                waveHeader.Size = br.ReadUInt32();
                waveHeader.WavId = br.ReadBytes(4);
                waveHeader.FmtId = br.ReadBytes(4);
                waveHeader.FmtSize = br.ReadUInt32();
                waveHeader.Format = br.ReadUInt16();
                waveHeader.Channels = br.ReadUInt16();
                waveHeader.SampleRate = br.ReadUInt32();
                waveHeader.BytesPerSec = br.ReadUInt32();
                waveHeader.BlockSize = br.ReadUInt16();
                waveHeader.Bit = br.ReadUInt16();
                waveHeader.DataId = br.ReadBytes(4);
                waveHeader.DataSize = br.ReadUInt32();
            }
            catch
            {
                throw new InvalidOperationException("Invalid file format.");
            }

            if (Encoding.ASCII.GetString(waveHeader.RiffId) != "RIFF" ||
                Encoding.ASCII.GetString(waveHeader.WavId) != "WAVE" ||
                Encoding.ASCII.GetString(waveHeader.FmtId) != "fmt ")
            {
                throw new InvalidOperationException("Invalid file format.");
            }

            _offset = stream.Position;

            WaveHeader = waveHeader;
            WaveFormat = new WaveFormat((int)waveHeader.SampleRate, waveHeader.Bit, waveHeader.Channels);
        }

        /// <summary>
        /// Deconstructs the WaveFile class.
        /// </summary>
        ~WaveFile()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the audio data.
        /// </summary>
        /// <returns>Byte Array.</returns>
        public byte[] GetAudioData()
        {
            _stream.Seek(_offset, SeekOrigin.Begin);
            var data = new byte[_stream.Length - _stream.Position];
            _stream.Read(data, 0, data.Length);
            return data;
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stream.Dispose();
            }
        }
    }
}
