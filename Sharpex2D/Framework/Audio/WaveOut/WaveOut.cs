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

namespace Sharpex2D.Framework.Audio.WaveOut
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOut : IDisposable
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
            if (_thread != null)
            {
                try
                {
                    _finished = true;
                    if (_waveOut != IntPtr.Zero)
                        NativeMethods.waveOutReset(_waveOut);
                    _thread.Join();
                    _fillProc = null;
                    FreeBuffers();
                    if (_waveOut != IntPtr.Zero)
                        NativeMethods.waveOutClose(_waveOut);
                }
                finally
                {
                    _thread = null;
                    _waveOut = IntPtr.Zero;
                }

                if (disposing)
                {
                }
            }
        }

        #endregion

        private IntPtr _waveOut;
        private WaveOutBuffer _buffers;
        private WaveOutBuffer _currentBuffer;
        private Thread _thread;
        private WaveOutBuffer.BufferFillEventHandler _fillProc;
        private bool _finished;
        private readonly byte _zero;

        private readonly NativeMethods.WaveDelegate _bufferProc = WaveOutBuffer.WaveOutProc;

        /// <summary>
        ///     Gets the DeviceCount.
        /// </summary>
        public static int DeviceCount
        {
            get { return NativeMethods.waveOutGetNumDevs(); }
        }

        /// <summary>
        /// Enumerates the devices.
        /// </summary>
        /// <returns>Array of AudioDevice.</returns>
        public static AudioDevice[] EnumerateDevices()
        {
            var devices = new AudioDevice[NativeMethods.waveOutGetNumDevs()];
            for (var i = 0; i < devices.Length; i++)
            {
                var caps = new WaveOutCaps();
                NativeMethods.waveOutGetDevCaps((uint)i, out caps, (uint) Marshal.SizeOf(caps));
                devices[i] = new AudioDevice((uint)i, caps);
            }
            return devices;
        }

        /// <summary>
        ///     Initializes a new WaveOut class.
        /// </summary>
        /// <param name="device">The Device.</param>
        /// <param name="format">The Format.</param>
        /// <param name="bufferSize">The BufferSize.</param>
        /// <param name="bufferCount">The BufferCount.</param>
        /// <param name="fillProc">The FillProc.</param>
        public WaveOut(uint device, WaveFormat format, int bufferSize, int bufferCount,
            WaveOutBuffer.BufferFillEventHandler fillProc)
        {
            _zero = format.wBitsPerSample == 8 ? (byte) 128 : (byte) 0;
            _fillProc = fillProc;
            WaveOutResult.Try(NativeMethods.waveOutOpen(out _waveOut, device, format, _bufferProc, (IntPtr) 0,
                NativeMethods.CALLBACK_FUNCTION));
            AllocateBuffers(bufferSize, bufferCount);
            _thread = new Thread(ThreadProc);
            _thread.Start();
        }

        /// <summary>
        ///     Allocates the buffers.
        /// </summary>
        /// <param name="bufferSize">The BufferSize.</param>
        /// <param name="bufferCount">The BufferCount.</param>
        private void AllocateBuffers(int bufferSize, int bufferCount)
        {
            FreeBuffers();

            if (bufferCount > 0)
            {
                _buffers = new WaveOutBuffer(_waveOut, bufferSize);
                WaveOutBuffer prev = _buffers;
                try
                {
                    for (int i = 1; i < bufferCount; i++)
                    {
                        var buffer = new WaveOutBuffer(_waveOut, bufferSize);
                        prev.NextBuffer = buffer;
                        prev = buffer;
                    }
                }
                finally
                {
                    prev.NextBuffer = _buffers;
                }
            }
        }

        /// <summary>
        ///     Frees the buffers.
        /// </summary>
        private void FreeBuffers()
        {
            _currentBuffer = null;
            if (_buffers != null)
            {
                WaveOutBuffer first = _buffers;
                _buffers = null;

                WaveOutBuffer current = first;
                do
                {
                    WaveOutBuffer next = current.NextBuffer;
                    current.Dispose();
                    current = next;
                } while (current != first);
            }
        }

        /// <summary>
        ///     Advance.
        /// </summary>
        private void Advance()
        {
            _currentBuffer = _currentBuffer == null ? _buffers : _currentBuffer.NextBuffer;
            _currentBuffer.WaitFor();
        }

        /// <summary>
        ///     Wait for all buffers.
        /// </summary>
        private void WaitForAllBuffers()
        {
            WaveOutBuffer buffer = _buffers;
            while (buffer.NextBuffer != _buffers)
            {
                buffer.WaitFor();
                buffer = buffer.NextBuffer;
            }
        }

        /// <summary>
        ///     Processes the WaveOut Thread.
        /// </summary>
        private void ThreadProc()
        {
            while (!_finished)
            {
                Advance();
                if (_fillProc != null && ! _finished)
                    _fillProc(_currentBuffer.Data, _currentBuffer.Size);
                else
                {
                    // zero out buffer
                    byte v = _zero;
                    var b = new byte[_currentBuffer.Size];
                    for (int i = 0; i < b.Length; i++)
                        b[i] = v;
                    Marshal.Copy(b, 0, _currentBuffer.Data, b.Length);
                }
                _currentBuffer.Play();
            }
            WaitForAllBuffers();
        }

        /// <summary>
        ///     Sets the Volume.
        /// </summary>
        public void SetVolume(float value)
        {
            NativeMethods.waveOutSetVolume(_waveOut, FloatToWaveOutVolume(value, value));
        }

        /// <summary>
        ///     Sets the balance.
        /// </summary>
        /// <param name="left">The Left.</param>
        /// <param name="right">The Right.</param>
        public void SetBalance(float left, float right)
        {
            NativeMethods.waveOutSetVolume(_waveOut, FloatToWaveOutVolume(left, right));
        }

        private static uint FloatToWaveOutVolume(float left, float right)
        {
            return (uint) (left*0xFFFF) + ((uint) (right*0xFFFF) << 16);
        }

        /// <summary>
        ///     Pause.
        /// </summary>
        public void Pause()
        {
            NativeMethods.waveOutPause(_waveOut);
        }

        /// <summary>
        ///     Resume.
        /// </summary>
        public void Resume()
        {
            NativeMethods.waveOutRestart(_waveOut);
        }

        /// <summary>
        ///     Stop.
        /// </summary>
        public void Stop()
        {
            NativeMethods.waveOutReset(_waveOut);
        }
    }

#endif

}