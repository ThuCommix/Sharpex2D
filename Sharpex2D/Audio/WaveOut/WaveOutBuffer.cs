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
using System.Threading;

namespace Sharpex2D.Audio.WaveOut
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOutBuffer
    {
        private readonly WaveHdr _header;
        private readonly AutoResetEvent _playEvent = new AutoResetEvent(false);
        private readonly WaveOut _wavOut;
        private readonly IntPtr _waveOutHandle;
        private GCHandle _headerDataHandle;
        private GCHandle _headerHandle;
        private bool _playing;

        /// <summary>
        ///     Initializes a new WaveOutBuffer class.
        /// </summary>
        /// <param name="waveOutHandle">The WaveOutHandle.</param>
        /// <param name="size">The Size.</param>
        /// <param name="wavOut">The WaveOut.</param>
        public WaveOutBuffer(IntPtr waveOutHandle, int size, WaveOut wavOut)
        {
            _wavOut = wavOut;
            _waveOutHandle = waveOutHandle;

            _header = new WaveHdr();
            _headerHandle = GCHandle.Alloc(_header, GCHandleType.Pinned);
            _header.dwUser = (IntPtr) GCHandle.Alloc(this);
            var headerData = new byte[size];
            _headerDataHandle = GCHandle.Alloc(headerData, GCHandleType.Pinned);
            _header.lpData = _headerDataHandle.AddrOfPinnedObject();
            _header.dwBufferLength = size;

            lock (_wavOut.LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutPrepareHeader(_waveOutHandle, _header, Marshal.SizeOf(_header)));
            }
        }

        /// <summary>
        ///     Gets or sets the NextBuffer.
        /// </summary>
        public WaveOutBuffer NextBuffer { set; get; }

        /// <summary>
        ///     Gets the Size.
        /// </summary>
        public int Size
        {
            get { return _header.dwBufferLength; }
        }

        /// <summary>
        ///     Gets the Data.
        /// </summary>
        public IntPtr Data
        {
            get { return _header.lpData; }
        }

        /// <summary>
        ///     Deconstructs the object.
        /// </summary>
        ~WaveOutBuffer()
        {
            Dispose();
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (_header.lpData != IntPtr.Zero)
            {
                lock (_wavOut.LockObj)
                {
                    WaveOutResult.Try(NativeMethods.waveOutUnprepareHeader(_waveOutHandle, _header,
                        Marshal.SizeOf(_header)));
                }

                _headerHandle.Free();
                _header.lpData = IntPtr.Zero;
            }

            _playEvent.Close();

            if (_headerDataHandle.IsAllocated)
            {
                _headerDataHandle.Free();
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Plays the buffer.
        /// </summary>
        /// <returns>True if playing.</returns>
        public bool Play()
        {
            lock (this)
            {
                _playEvent.Reset();
                lock (_wavOut.LockObj)
                {
                    _playing = NativeMethods.waveOutWrite(_waveOutHandle, _header, Marshal.SizeOf(_header)) ==
                               (int) MMResult.MMSYSERR_NOERROR;
                }
                return _playing;
            }
        }

        /// <summary>
        ///     Waits for the buffer to be completed.
        /// </summary>
        public void WaitFor()
        {
            if (_playing)
            {
                _playing = _playEvent.WaitOne();
            }
            else
            {
                Thread.Sleep(0);
            }
        }

        /// <summary>
        ///     Called, if the buffer is completed.
        /// </summary>
        public void OnCompleted()
        {
            _playEvent.Set();
            _playing = false;
        }

        /// <summary>
        ///     Processes the WaveOut messages.
        /// </summary>
        /// <param name="hdrvr">The Handle.</param>
        /// <param name="uMsg">The Message.</param>
        /// <param name="dwUser">The UserData.</param>
        /// <param name="wavhdr">The WaveHeader.</param>
        /// <param name="dwParam2">Reserved.</param>
        internal static void WaveOutProc(IntPtr hdrvr, int uMsg, IntPtr dwUser, WaveHdr wavhdr, IntPtr dwParam2)
        {
            if (uMsg == NativeMethods.MM_WOM_DONE)
            {
                try
                {
                    var h = (GCHandle) wavhdr.dwUser;
                    var buf = (WaveOutBuffer) h.Target;
                    buf.OnCompleted();
                }
                catch
                {
                }
            }
        }
    }

#endif
}