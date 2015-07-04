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
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class WaveOut
    {
        private readonly MMInterops.WaveCallback _callback;
        private readonly object _lockObj = new object();
        internal SoundMixer AudioMixer;
        private int _activeBuffers;
        private List<WaveOutBuffer> _buffers;
        private int _latency = 150;
        private PlaybackState _playbackState = PlaybackState.Stopped;

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
        internal object LockObj
        {
            get { return _lockObj; }
        }

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
        public MemoryStream Stream { get; private set; }

        /// <summary>
        /// Gets the WaveFormat.
        /// </summary>
        public WaveFormat Format { get; private set; }

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
        public int Device { get; set; }

        /// <summary>
        /// Gets the WaveOutHandle.
        /// </summary>
        public IntPtr WaveOutHandle { get; private set; }

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
        public event EventHandler<EventArgs> PlaybackChanged;

        /// <summary>
        /// Gets the available device amount.
        /// </summary>
        /// <returns>Int32.</returns>
        public static int GetDeviceCount()
        {
            return MMInterops.waveOutGetNumDevs();
        }

        /// <summary>
        /// Gets the caps of the device. 
        /// </summary>
        /// <param name="device">The Device.</param>
        /// <returns>WaveOutCaps.</returns>
        public static WaveOutCaps GetDevice(int device)
        {
            var caps = new WaveOutCaps();
            MMInterops.waveOutGetDevCaps((uint) device, out caps, (uint) Marshal.SizeOf(caps));
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
        /// <param name="audioData">The AudioData.</param>
        /// <param name="format">The WaveFormat.</param>
        public void Initialize(byte[] audioData, WaveFormat format)
        {
            lock (_lockObj)
            {
                Stop();
                Stream = new MemoryStream(audioData);
                Format = format;
                WaveOutHandle = CreateWaveOut();
                int bufferSize = (format.AvgBytesPerSec/1000*_latency);
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
                RaisePlaybackChanged();
            }
            else if (_playbackState == PlaybackState.Paused)
            {
                Resume();
                _playbackState = PlaybackState.Playing;
                RaisePlaybackChanged();
            }
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
                    MMInterops.waveOutPause(WaveOutHandle);
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
                    MMInterops.waveOutRestart(WaveOutHandle);
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
            Stream.Position = 0;
            foreach (WaveOutBuffer buffer in _buffers)
            {
                if (!buffer.IsInQueue)
                {
                    lock (_lockObj)
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
                lock (_lockObj)
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

                lock (_lockObj)
                {
                    WaveOutResult.Try(MMInterops.waveOutClose(WaveOutHandle));
                    WaveOutHandle = IntPtr.Zero;
                }
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
            MMResult result = MMInterops.waveOutSetVolume(waveOut, tmp);
            if (result != MMResult.MMSYSERR_NOERROR)
                WaveOutResult.Try(MMInterops.waveOutSetVolume(waveOut, tmp));
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