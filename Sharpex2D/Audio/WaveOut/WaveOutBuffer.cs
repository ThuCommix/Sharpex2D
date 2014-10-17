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
using System.Runtime.InteropServices;

namespace Sharpex2D.Audio.WaveOut
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOutBuffer : IDisposable
    {
        private readonly int _bufferSize;
        private readonly WaveOut _waveOut;
        private byte[] _buffer;
        private GCHandle _bufferHandle;
        private bool _disposed;
        private WaveHdr _header;
        private GCHandle _headerHandle;
        private GCHandle _userDataHandle;

        /// <summary>
        /// Initializes a new WaveOutBuffer class.
        /// </summary>
        /// <param name="waveOut">The WaveOut.</param>
        /// <param name="bufferSize">The BufferSize.</param>
        public WaveOutBuffer(WaveOut waveOut, int bufferSize)
        {
            if (waveOut == null)
                throw new ArgumentNullException("waveOut");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize");

            _waveOut = waveOut;
            _bufferSize = bufferSize;
        }

        /// <summary>
        /// A value indicating whether the Buffer is in queue.
        /// </summary>
        public bool IsInQueue
        {
            get { return (_header.dwFlags & WaveHeaderFlags.WHDR_INQUEUE) == WaveHeaderFlags.WHDR_INQUEUE; }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            _disposed = true;
        }

        /// <summary>
        /// Initializes the buffer.
        /// </summary>
        public void Initialize()
        {
            _buffer = new byte[_bufferSize];

            var header = new WaveHdr();
            _headerHandle = GCHandle.Alloc(header);
            _userDataHandle = GCHandle.Alloc(this);
            _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);

            header.dwUser = (IntPtr) _userDataHandle;
            header.dwLoops = 1;
            header.lpData = _bufferHandle.AddrOfPinnedObject();
            header.dwBufferLength = _bufferSize;

            _header = header;
            lock (_waveOut.LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutPrepareHeader(_waveOut.WaveOutHandle, header,
                    Marshal.SizeOf(header)));
            }
        }

        /// <summary>
        /// Writes data to the buffer.
        /// </summary>
        /// <returns>True on success.</returns>
        public bool WriteData()
        {
            int read;
            lock (_waveOut.WaveStream)
            {
                read = _waveOut.WaveStream.Read(_buffer, 0, _buffer.Length);
            }
            if (read > 0)
            {
                Array.Clear(_buffer, read, _buffer.Length - read);
                lock (_waveOut.LockObj)
                {
                    MMResult result = NativeMethods.waveOutWrite(_waveOut.WaveOutHandle, _header,
                        Marshal.SizeOf(_header));
                    if (result != MMResult.MMSYSERR_NOERROR)
                    {
                        WaveOutResult.Try(result);
                    }
                    return result == MMResult.MMSYSERR_NOERROR;
                }
            }
            return false;
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            lock (_waveOut.LockObj)
            {
                if (_header == null || _waveOut.WaveOutHandle == IntPtr.Zero)
                    return;


                WaveOutResult.Try(NativeMethods.waveOutUnprepareHeader(_waveOut.WaveOutHandle, _header,
                    Marshal.SizeOf(_header)));

                if (_bufferHandle.IsAllocated)
                    _bufferHandle.Free();
                if (_headerHandle.IsAllocated)
                    _headerHandle.Free();
                if (_userDataHandle.IsAllocated)
                    _userDataHandle.Free();
                _header = null;
            }
        }

        /// <summary>
        /// Deconstructs the WaveOutBuffer class.
        /// </summary>
        ~WaveOutBuffer()
        {
            Dispose(false);
        }
    }
#endif
}