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
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Audio.WaveOut
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOutBuffer : IDisposable
    {


        #region IDisposable Implementation

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">The Disposing State.</param>
        public virtual void Dispose(bool disposing)
        {
            if (_header.lpData != IntPtr.Zero)
            {
                NativeMethods.waveOutUnprepareHeader(_waveOut, _header, Marshal.SizeOf(_header));
                _headerHandle.Free();
                _header.lpData = IntPtr.Zero;
            }

            _playEvent.Close();

            if (_headerDataHandle.IsAllocated)
            {
                _headerDataHandle.Free();
            }

            if (disposing)
            {
            }
        }

        #endregion

        /// <summary>
        ///     BufferFillEventHandler.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="size">The Size.</param>
        public delegate void BufferFillEventHandler(IntPtr data, int size);

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

        private readonly AutoResetEvent _playEvent = new AutoResetEvent(false);
        private readonly IntPtr _waveOut;
        private readonly WaveHdr _header = new WaveHdr();
        private byte[] _headerData;
        private GCHandle _headerHandle;
        private GCHandle _headerDataHandle;
        private bool _isPlaying;

        /// <summary>
        ///     Initializes a new WaveOutBuffer class.
        /// </summary>
        /// <param name="waveOutHandle">The Handle.</param>
        /// <param name="size">The Size.</param>
        public WaveOutBuffer(IntPtr waveOutHandle, int size)
        {
            _waveOut = waveOutHandle;

            _headerHandle = GCHandle.Alloc(_header, GCHandleType.Pinned);
            _header.dwUser = (IntPtr) GCHandle.Alloc(this);
            _headerData = new byte[size];
            _headerDataHandle = GCHandle.Alloc(_headerData, GCHandleType.Pinned);
            _header.lpData = _headerDataHandle.AddrOfPinnedObject();
            _header.dwBufferLength = size;

            WaveOutResult.Try(NativeMethods.waveOutPrepareHeader(_waveOut, _header, Marshal.SizeOf(_header)));
        }

        /// <summary>
        ///     Processes the WaveOut messages.
        /// </summary>
        /// <param name="hdrvr">The Handle</param>
        /// <param name="uMsg">The Message.</param>
        /// <param name="dwUser">The User.</param>
        /// <param name="wavhdr">The WaveHeader.</param>
        /// <param name="dwParam2">Reserved for driver.</param>
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
                    LogManager.GetClassLogger().Warn("Error while handling the WaveOut message processing.");
                }
            }
        }

        /// <summary>
        ///     OnCompleted.
        /// </summary>
        private void OnCompleted()
        {
            _playEvent.Set();
            _isPlaying = false;
        }

        /// <summary>
        ///     Plays.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool Play()
        {
            lock (this)
            {
                _playEvent.Reset();
                _isPlaying = NativeMethods.waveOutWrite(_waveOut, _header, Marshal.SizeOf(_header)) ==
                             NativeMethods.MMSYSERR_NOERROR;
                return _isPlaying;
            }
        }

        /// <summary>
        ///     Waits for the PlayEvent to reset.
        /// </summary>
        public void WaitFor()
        {
            if (_isPlaying)
            {
                _isPlaying = _playEvent.WaitOne();
            }
            else
            {
                Thread.Sleep(0);
            }
        }
    }

#endif

}