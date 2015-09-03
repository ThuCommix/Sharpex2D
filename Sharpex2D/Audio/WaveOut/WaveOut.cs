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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sharpex2D.Framework.Audio.WaveOut
{
    internal class WaveOut
    {
        private readonly MMInterops.WaveCallback _callback;
        private int _activeBuffers;
        private List<WaveOutBuffer> _buffers;
        internal SoundMixer AudioMixer;
        private static WaveOutDevice[] _devices;

        /// <summary>
        /// Initializes a new WaveOut class.
        /// </summary>
        public WaveOut()
        {
            _callback = Callback;
            AudioMixer = new SoundMixer();
        }

        /// <summary>
        /// Just exists for lock.
        /// </summary>
        internal object LockObj { get; } = new object();

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return AudioMixer.Volume; }
            set { AudioMixer.Volume = value; }
        }

        /// <summary>
        /// Gets or sets the Pan.
        /// </summary>
        public float Pan
        {
            get { return AudioMixer.Pan; }
            set { AudioMixer.Pan = value; }
        }

        /// <summary>
        /// Gets the current Stream.
        /// </summary>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the WaveFormat.
        /// </summary>
        public WaveFormat Format { get; private set; }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState { get; private set; } = PlaybackState.Stopped;

        /// <summary>
        /// Gets the Device.
        /// </summary>
        public int Device { get; set; }

        /// <summary>
        /// Gets the WaveOutHandle.
        /// </summary>
        public IntPtr WaveOutHandle { get; private set; }

        /// <summary>
        /// Gets or sets the Latency.
        /// </summary>
        public int Latency { get; set; } = 150;

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Gets the available device amount.
        /// </summary>
        /// <returns>Int32.</returns>
        private static int GetDeviceCount()
        {
            return MMInterops.waveOutGetNumDevs();
        }

        /// <summary>
        /// Gets the caps of the device. 
        /// </summary>
        /// <param name="device">The Device.</param>
        /// <returns>WaveOutDevice</returns>
        private static WaveOutDevice GetDevice(int device)
        {
            var caps = new WaveOutCaps();
            MMInterops.waveOutGetDevCaps((uint) device, out caps, (uint) Marshal.SizeOf(caps));
            return new WaveOutDevice(caps, device);
        }

        /// <summary>
        /// Gets all devices.
        /// </summary>
        /// <returns>Array of WaveOutCaps.</returns>
        public static WaveOutDevice[] EnumerateDevices()
        {
            if (_devices == null)
            {
                _devices = new WaveOutDevice[GetDeviceCount()];
                for (int i = 0; i < _devices.Length; i++)
                    _devices[i] = GetDevice(i);
            }

            return _devices;
        }

        /// <summary>
        /// Initializes the playback.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="format">The WaveFormat.</param>
        public void Initialize(Stream stream, WaveFormat format)
        {
            lock (LockObj)
            {
                Stop();
                Stream = stream;
                Format = format;
                WaveOutHandle = CreateWaveOut();
                int bufferSize = (format.AvgBytesPerSec/1000*Latency);
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
            if (PlaybackState == PlaybackState.Stopped)
            {
                StartPlayback();
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
            else if (PlaybackState == PlaybackState.Paused)
            {
                Resume();
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Pause the wavestream.
        /// </summary>
        public void Pause()
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                lock (LockObj)
                {
                    MMInterops.waveOutPause(WaveOutHandle);
                }
                PlaybackState = PlaybackState.Paused;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Resumes the wavestream.
        /// </summary>
        public void Resume()
        {
            if (PlaybackState == PlaybackState.Paused)
            {
                lock (LockObj)
                {
                    MMInterops.waveOutRestart(WaveOutHandle);
                }
                PlaybackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Stops the wavestream.
        /// </summary>
        public void Stop()
        {
            if (PlaybackState != PlaybackState.Stopped)
            {
                PlaybackState = PlaybackState.Stopped;
                lock (LockObj)
                {
                    MMResult result = MMInterops.waveOutReset(WaveOutHandle);
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
            WaveOutResult.Try(MMInterops.waveOutOpen(out handle,
                (IntPtr) Device,
                Format,
                _callback,
                IntPtr.Zero,
                MMInterops.CALLBACK_FUNCTION));

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
            if (WaveOutHandle != handle)
                return;

            if (msg == WaveMessage.WOM_DONE)
            {
                var hBuffer = (GCHandle) header.dwUser;
                var buffer = hBuffer.Target as WaveOutBuffer;
                Interlocked.Decrement(ref _activeBuffers);

                if (buffer == null) return;
                if (PlaybackState != PlaybackState.Stopped)
                {
                    lock (LockObj)
                    {
                        if (buffer.WriteData())
                            Interlocked.Increment(ref _activeBuffers);
                    }
                }

                if (_activeBuffers == 0)
                {
                    PlaybackState = PlaybackState.Stopped;
                    RaisePlaybackChanged();
                }
            }
            else if (msg == WaveMessage.WOM_CLOSE)
            {
                PlaybackState = PlaybackState.Stopped;
                RaisePlaybackChanged();
            }
        }

        /// <summary>
        /// Starts the playback.
        /// </summary>
        private void StartPlayback()
        {
            Stream.Position = 0;
            foreach (WaveOutBuffer buffer in _buffers)
            {
                if (!buffer.IsInQueue)
                {
                    lock (LockObj)
                    {
                        if (buffer.WriteData())
                        {
                            Interlocked.Increment(ref _activeBuffers);
                        }
                        else
                        {
                            PlaybackState = PlaybackState.Stopped;
                            RaisePlaybackChanged();
                            break;
                        }
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
            if (_buffers != null)
            {
                lock (LockObj)
                {
                    foreach (WaveOutBuffer buffer in _buffers)
                    {
                        buffer.Dispose();
                    }
                }
                _buffers.Clear();
            }
            if (Stream != null)
                Stream.Close();

            if (disposing)
            {
                if (WaveOutHandle == IntPtr.Zero)
                    return;

                lock (LockObj)
                {
                    WaveOutResult.Try(MMInterops.waveOutClose(WaveOutHandle));
                    WaveOutHandle = IntPtr.Zero;
                }
            }
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
}
