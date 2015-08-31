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
using CSCore;
using Sharpex2D.Framework;

namespace Sharpex2D.Audio.XAudio2
{
    internal class EventWaveSource : IWaveSource
    {
        private readonly IWaveSource _waveSource;
        private bool _isEofTriggered;

        /// <summary>
        /// Initializes a new EventWaveSource class.
        /// </summary>
        /// <param name="waveSource">The IWaveSource.</param>
        public EventWaveSource(IWaveSource waveSource)
        {
            _waveSource = waveSource;
        }

        /// <summary>
        /// Gets the WaveFormat.
        /// </summary>
        public WaveFormat WaveFormat
        {
            get { return _waveSource.WaveFormat; }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public long Position
        {
            get { return _waveSource.Position; }
            set
            {
                _waveSource.Position = value;
                if (value < _waveSource.Length)
                {
                    _isEofTriggered = false;
                }
            }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public long Length
        {
            get { return _waveSource.Length; }
        }

        /// <summary>
        /// Reads from the WaveSource.
        /// </summary>
        /// <param name="buffer">The Buffer.</param>
        /// <param name="offset">The Offset.</param>
        /// <param name="count">The Count.</param>
        /// <returns>Bytes read.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                return _waveSource.Read(buffer, offset, count);
            }
            finally
            {
                if (Position == Length)
                {
                    if (EndOfStream != null && !_isEofTriggered)
                    {
                        _isEofTriggered = true;
                        EndOfStream(this, EventArgs.Empty);
                    }
                }
            }
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
        /// Triggered if the underlaying wave source is end of stream.
        /// </summary>
        public event EventHandler<EventArgs> EndOfStream;

        /// <summary>
        /// Deconstructs the WaveSource class.
        /// </summary>
        ~EventWaveSource()
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
                _waveSource.Dispose();
            }
        }
    }
}
