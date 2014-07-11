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
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Sharpex2D.Audio.WaveOut
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal class WaveOut : IDisposable
    {
        private readonly NativeMethods.WaveDelegate _bufferProcessor = WaveOutBuffer.WaveOutProc;
        private readonly byte _zero;
        private WaveOutBuffer _buffers;
        private WaveOutBuffer _currentBuffer;
        private BufferFillEventHandler _fillProcessor;
        private bool _finished;
        private Task _thread;
        private IntPtr _waveOutHandle;
        private WaveStream _waveStream;

        /// <summary>
        ///     Initializes a new WaveOut class.
        /// </summary>
        /// <param name="device">The AudioDevice.</param>
        /// <param name="waveStream">The WaveStream.</param>
        /// <param name="bufferDescription">The BufferDescription.</param>
        public WaveOut(WaveOutDevice device, WaveStream waveStream, WaveOutBufferDescription bufferDescription)
        {
            _waveStream = waveStream;
            LockObj = new object();
            _zero = _waveStream.Format.wBitsPerSample == 8 ? (byte) 128 : (byte) 0;
            _fillProcessor = FillBuffer;

            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutOpen(out _waveOutHandle, device.DeviceId, waveStream.Format,
                    _bufferProcessor, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION));
            }

            AllocateBuffers(bufferDescription.Size, bufferDescription.Count);

            _thread = new Task(CopyData);
            _thread.Start();
        }

        /// <summary>
        ///     Gets the LockObj.
        /// </summary>
        public object LockObj { private set; get; }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (_thread != null)
                try
                {
                    _finished = true;
                    if (_waveOutHandle != IntPtr.Zero)
                    {
                        lock (LockObj)
                        {
                            WaveOutResult.Try(NativeMethods.waveOutReset(_waveOutHandle));
                        }
                    }
                    while (!_thread.IsCompleted)
                    {
                    }
                    _fillProcessor = null;
                    FreeBuffers();
                    if (_waveOutHandle != IntPtr.Zero)
                    {
                        lock (LockObj)
                        {
                            WaveOutResult.Try(NativeMethods.waveOutClose(_waveOutHandle));
                        }
                    }
                    lock (LockObj)
                    {
                        _waveStream.Close();
                        _waveStream.Dispose();
                        _waveStream = null;
                    }
                }
                finally
                {
                    _thread = null;
                    _waveOutHandle = IntPtr.Zero;
                }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Enumerates the devices.
        /// </summary>
        /// <returns>Array of AudioDevice.</returns>
        public static WaveOutDevice[] EnumerateDevices()
        {
            int deviceNum = NativeMethods.waveOutGetNumDevs();
            var devices = new WaveOutDevice[deviceNum];
            for (int i = 0; i <= deviceNum - 1; i++)
            {
                var caps = new WaveOutCaps();
                int result = NativeMethods.waveOutGetDevCaps((uint) i, out caps, (uint) Marshal.SizeOf(caps));
                WaveOutResult.Try(result);

                devices[i] = new WaveOutDevice((uint) i, caps);
            }

            return devices;
        }

        /// <summary>
        ///     Deconstructs the WaveOut.
        /// </summary>
        ~WaveOut()
        {
            Dispose();
        }

        /// <summary>
        ///     Copies the data.
        /// </summary>
        private void CopyData()
        {
            while (!_finished)
            {
                Advance();
                if (_fillProcessor != null && ! _finished)
                    _fillProcessor(_currentBuffer.Data, _currentBuffer.Size);
                else
                {
                    byte v = _zero;
                    var b = new byte[_currentBuffer.Size];
                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i] = v;
                    }
                    Marshal.Copy(b, 0, _currentBuffer.Data, b.Length);
                }
                _currentBuffer.Play();
            }
            WaitForAllBuffers();
        }

        /// <summary>
        ///     Fills the buffer.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="size">The Size.</param>
        private void FillBuffer(IntPtr data, int size)
        {
            var b = new byte[size];
            lock (LockObj)
            {
                if (_waveStream != null)
                {
                    int pos = 0;
                    while (pos < size && !_finished)
                    {
                        int toget = size - pos;
                        int got = _waveStream.Read(b, pos, toget);
                        if (got < toget)
                        {
                            _finished = true;
                        }
                        pos += got;
                    }
                }
                else
                {
                    for (int i = 0; i < b.Length; i++)
                        b[i] = 0;
                }
            }
            Marshal.Copy(b, 0, data, size);
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
                _buffers = new WaveOutBuffer(_waveOutHandle, bufferSize, this);
                WaveOutBuffer prev = _buffers;
                try
                {
                    for (int i = 1; i < bufferCount; i++)
                    {
                        var buffer = new WaveOutBuffer(_waveOutHandle, bufferSize, this);
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
        ///     Waits for all buffers.
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
        ///     Sets the Volume.
        /// </summary>
        public void SetVolume(float value)
        {
            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutSetVolume(_waveOutHandle, FloatToWaveOutVolume(value, value)));
            }
        }

        /// <summary>
        ///     Sets the balance.
        /// </summary>
        /// <param name="left">The Left.</param>
        /// <param name="right">The Right.</param>
        public void SetBalance(float left, float right)
        {
            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutSetVolume(_waveOutHandle, FloatToWaveOutVolume(left, right)));
            }
        }

        /// <summary>
        ///     Converts a float into waveOut volume.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>UInt.</returns>
        private static uint FloatToWaveOutVolume(float left, float right)
        {
            return (uint) (left*0xFFFF) + ((uint) (right*0xFFFF) << 16);
        }

        /// <summary>
        ///     Pause.
        /// </summary>
        public void Pause()
        {
            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutPause(_waveOutHandle));
            }
        }

        /// <summary>
        ///     Resume.
        /// </summary>
        public void Resume()
        {
            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutRestart(_waveOutHandle));
            }
        }

        /// <summary>
        ///     Stop.
        /// </summary>
        public void Stop()
        {
            lock (LockObj)
            {
                WaveOutResult.Try(NativeMethods.waveOutReset(_waveOutHandle));
            }
        }

        /// <summary>
        ///     Gets the Position in ms.
        /// </summary>
        /// <returns>Int64.</returns>
        public long GetPosition()
        {
            lock (LockObj)
            {
                return _waveStream.Position/_waveStream.Format.nAvgBytesPerSec;
            }
        }

        /// <summary>
        ///     Gets the Length in ms.
        /// </summary>
        /// <returns>Int64.</returns>
        public long GetLength()
        {
            lock (LockObj)
            {
                return _waveStream.Length/_waveStream.Format.nAvgBytesPerSec;
            }
        }

        /// <summary>
        ///     Seeks.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            lock (LockObj)
            {
                long requestesPostion = position/1000*_waveStream.Format.nAvgBytesPerSec;
                if (requestesPostion < _waveStream.Length)
                {
                    _waveStream.Seek(requestesPostion, SeekOrigin.Begin);
                }
            }
        }

        /// <summary>
        ///     A value indicating whether the waveOut is playing.
        /// </summary>
        /// <returns>True if playing.</returns>
        public bool IsPlaying()
        {
            return !_finished;
        }

        private delegate void BufferFillEventHandler(IntPtr data, int size);
    }

#endif
}