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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sharpex2D.Audio.WaveOut
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOut
    {
        private readonly NativeMethods.WaveCallback _callback;
        private readonly object _lockObj = new object();
        private int _activeBuffers;
        private float _balance;
        private List<WaveOutBuffer> _buffers;
        private int _device;
        private IntPtr _hWaveOut;
        private int _latency = 150;

        private PlaybackState _playbackState = PlaybackState.Stopped;
        private WaveStream _stream;
        private float _volume;

        /// <summary>
        /// Initializes a new WaveOut class.
        /// </summary>
        public WaveOut()
        {
            _callback = Callback;
        }

        /// <summary>
        /// Just exists for lock.
        /// </summary>
        internal object LockObj
        {
            get { return _lockObj; }
        }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _volume; }
            set
            {
                SetVolume(_hWaveOut, value, value);
                _volume = value;
            }
        }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Balance
        {
            get { return _balance; }
            set
            {
                float left = System.Math.Min(1, value + 1);
                float right = System.Math.Abs(System.Math.Max(-1, value - 1));

                SetVolume(_hWaveOut, left*Volume, right*Volume);
                _balance = value;
            }
        }

        /// <summary>
        /// Gets the current WaveStream.
        /// </summary>
        public WaveStream WaveStream
        {
            get { return _stream; }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _playbackState; }
        }

        /// <summary>
        /// Gets the Device.
        /// </summary>
        public int Device
        {
            get { return _device; }
            set { _device = value; }
        }

        /// <summary>
        /// Gets the WaveOutHandle.
        /// </summary>
        public IntPtr WaveOutHandle
        {
            get { return _hWaveOut; }
        }

        /// <summary>
        /// Gets or sets the Latency.
        /// </summary>
        public int Latency
        {
            get { return _latency; }
            set { _latency = value; }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event PlaybackChangedEventHandler PlaybackChanged;

        /// <summary>
        /// Gets the available device amount.
        /// </summary>
        /// <returns>Int32.</returns>
        public static int GetDeviceCount()
        {
            return NativeMethods.waveOutGetNumDevs();
        }

        /// <summary>
        /// Gets the caps of the device. 
        /// </summary>
        /// <param name="device">The Device.</param>
        /// <returns>WaveOutCaps.</returns>
        public static WaveOutCaps GetDevice(int device)
        {
            var caps = new WaveOutCaps();
            NativeMethods.waveOutGetDevCaps((uint) device, out caps, (uint) Marshal.SizeOf(caps));
            return caps;
        }

        /// <summary>
        /// Gets all devices.
        /// </summary>
        /// <returns>Array of WaveOutCaps.</returns>
        public static WaveOutCaps[] GetDevices()
        {
            var caps = new WaveOutCaps[GetDeviceCount()];
            for (int i = 0; i < caps.Length; i++)
                caps[i] = GetDevice(i);
            return caps;
        }

        /// <summary>
        /// Initializes the playback.
        /// </summary>
        /// <param name="waveStream">The WaveStream.</param>
        public void Initialize(WaveStream waveStream)
        {
            lock (_lockObj)
            {
                _stream = waveStream;
                _hWaveOut = CreateWaveOut();
                int bufferSize = (_stream.Format.nAvgBytesPerSec/1000*_latency);
                _buffers = new List<WaveOutBuffer>();
                for (int i = 0; i < 2; i++)
                {
                    _buffers.Add(new WaveOutBuffer(this, bufferSize));
                    _buffers.Last().Initialize();
                }
            }
        }

        /// <summary>
        /// Plays the wavestream.
        /// </summary>
        public void Play()
        {
            if (_playbackState == PlaybackState.Stopped)
            {
                StartPlayback();
                _playbackState = PlaybackState.Playing;
            }
            else if (_playbackState == PlaybackState.Paused)
            {
                Resume();
                _playbackState = PlaybackState.Playing;
            }
            RaisePlaybackChanged();
        }

        /// <summary>
        /// Pause the wavestream.
        /// </summary>
        public void Pause()
        {
            if (_playbackState == PlaybackState.Playing)
            {
                lock (_lockObj)
                {
                    NativeMethods.waveOutPause(_hWaveOut);
                }
                _playbackState = PlaybackState.Paused;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Resumes the wavestream.
        /// </summary>
        public void Resume()
        {
            if (_playbackState == PlaybackState.Paused)
            {
                lock (_lockObj)
                {
                    NativeMethods.waveOutRestart(_hWaveOut);
                }
                _playbackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Stops the wavestream.
        /// </summary>
        public void Stop()
        {
            if (_playbackState != PlaybackState.Stopped)
            {
                _playbackState = PlaybackState.Stopped;
                lock (_lockObj)
                {
                    MMResult result = NativeMethods.waveOutReset(_hWaveOut);
                    WaveOutResult.Try(result);
                }
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Creates new WaveOut.
        /// </summary>
        /// <returns></returns>
        private IntPtr CreateWaveOut()
        {
            IntPtr handle;
            WaveOutResult.Try(NativeMethods.waveOutOpen(out handle,
                (IntPtr) _device,
                _stream.Format,
                _callback,
                IntPtr.Zero,
                NativeMethods.CALLBACK_FUNCTION));

            return handle;
        }

        /// <summary>
        /// Callback func.
        /// </summary>
        /// <param name="handle">The WaveOutHandle.</param>
        /// <param name="msg">The WaveMessage.</param>
        /// <param name="user">The UserData.</param>
        /// <param name="header">The WaveHdr.</param>
        /// <param name="reserved">Reserved.</param>
        private void Callback(IntPtr handle, WaveMessage msg, UIntPtr user, WaveHdr header, UIntPtr reserved)
        {
            if (_hWaveOut != handle)
                return;

            if (msg == WaveMessage.WOM_DONE)
            {
                var hBuffer = (GCHandle) header.dwUser;
                var buffer = hBuffer.Target as WaveOutBuffer;
                Interlocked.Decrement(ref _activeBuffers);

                if (buffer == null) return;
                if (_playbackState != PlaybackState.Stopped)
                {
                    lock (_lockObj)
                    {
                        if (buffer.WriteData())
                            Interlocked.Increment(ref _activeBuffers);
                    }
                }

                if (_activeBuffers == 0)
                {
                    _playbackState = PlaybackState.Stopped;
                    RaisePlaybackChanged();
                }
            }
            else if (msg == WaveMessage.WOM_CLOSE)
            {
                _playbackState = PlaybackState.Stopped;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Starts the playback.
        /// </summary>
        private void StartPlayback()
        {
            foreach (WaveOutBuffer buffer in _buffers)
            {
                if (!buffer.IsInQueue)
                {
                    if (buffer.WriteData())
                    {
                        Interlocked.Increment(ref _activeBuffers);
                    }
                    else
                    {
                        _playbackState = PlaybackState.Stopped;
                        RaisePlaybackChanged();
                        break;
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
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (_hWaveOut == IntPtr.Zero)
                return;

            Stop();
            lock (_lockObj)
            {
                if (_buffers != null)
                {
                    foreach (WaveOutBuffer buffer in _buffers)
                    {
                        buffer.Dispose();
                    }

                    _buffers.Clear();
                }
                WaveOutResult.Try(NativeMethods.waveOutClose(_hWaveOut));
                _hWaveOut = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Sets the Volume.
        /// </summary>
        /// <param name="waveOut">The WaveOut.</param>
        /// <param name="left">The Left.</param>
        /// <param name="right">The Right.</param>
        public static void SetVolume(IntPtr waveOut, float left, float right)
        {
            uint tmp = (uint) (left*0xFFFF) + ((uint) (right*0xFFFF) << 16);
            MMResult result = NativeMethods.waveOutSetVolume(waveOut, tmp);
            if (result != MMResult.MMSYSERR_NOERROR)
                WaveOutResult.Try(NativeMethods.waveOutSetVolume(waveOut, tmp));
        }

        /// <summary>
        /// Raises the PlaybackChanged event.
        /// </summary>
        private void RaisePlaybackChanged()
        {
            if (PlaybackChanged != null)
            {
                PlaybackChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Deconstructs the WaveOut class.
        /// </summary>
        ~WaveOut()
        {
            Dispose(false);
        }
    }
#endif
}