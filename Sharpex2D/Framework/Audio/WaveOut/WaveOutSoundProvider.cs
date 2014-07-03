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
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Audio.WaveOut
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]

    public class WaveOutSoundProvider : ISoundProvider
    {
        #region ISoundProvider Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

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
            CloseWav();
        }

        /// <summary>
        ///     Gets or sets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                float volume = Volume;

                float left = System.Math.Min(1, value + 1);
                float right = System.Math.Abs(System.Math.Max(-1, value - 1));

                _waveOut.SetBalance(left*volume, right*volume);
            }
        }

        /// <summary>
        ///     Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _volume; }
            set
            {
                _waveOut.SetVolume(value);
                _volume = value;
            }
        }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public long Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Seek(value);
            }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        ///     Gets the Length.
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        ///     Plays a sound file.
        /// </summary>
        /// <param name="soundFile">The SoundFile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound soundFile, PlayMode playMode)
        {
            CloseWav();
            _playMode = playMode;
            OpenWav(soundFile.Data);
            PlayWav();
        }

        /// <summary>
        ///     Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _waveOut.Resume();
            IsPlaying = true;
        }

        /// <summary>
        ///     Pause a sound.
        /// </summary>
        public void Pause()
        {
            _waveOut.Pause();
            IsPlaying = false;
        }

        /// <summary>
        ///     Seeks a sound.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_waveStream != null)
            {
                long requestesPostion = position/1000*_waveStream.WaveFormat.nAvgBytesPerSec;
                if (requestesPostion < _waveStream.Length)
                {
                    _waveStream.Seek(requestesPostion, SeekOrigin.Begin);
                }
            }
        }

        /// <summary>
        ///     Gets the SoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { private set; get; }

        #endregion

        /// <summary>
        /// Gets or sets the current device.
        /// </summary>
        public AudioDevice CurrentDevice { private set; get; }

        private WaveFormat _currentFormat;
        private WaveStream _waveStream;
        private WaveOut _waveOut;
        private float _balance;
        private long _position;
        private float _volume;
        private PlayMode _playMode;

        /// <summary>
        ///     Initializes a new WaveOutProvider class.
        /// </summary>
        /// <param name="soundInitializer">The assigned SoundInitializer.</param>
        internal WaveOutSoundProvider(ISoundInitializer soundInitializer)
        {
            SoundInitializer = soundInitializer;
            Guid = new Guid("B56F814F-5E22-4930-A22D-DC93D55173E3");

            var devices = WaveOut.EnumerateDevices();
            if (devices.Length == 0)
                throw new InvalidOperationException("There is not existing WavOut device available.");
            CurrentDevice = devices[0];

            //check device features

            if (!CurrentDevice.IsSupported(WaveCapsSupported.WAVECAPS_LRVOLUME) ||
                !CurrentDevice.IsSupported(WaveCapsSupported.WAVECAPS_VOLUME))
            {
                var logger = LogManager.GetClassLogger();
                logger.Warn("The current device does not support all required features.");
            }
        }

        /// <summary>
        ///     Opens the wav.
        /// </summary>
        /// <param name="bytes">The Bytes.</param>
        internal void OpenWav(byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            try
            {
                var waveStream = new WaveStream(memoryStream);
                if (waveStream.Length <= 0)
                {
                    throw new Exception("The specified wave file is invalid.");
                }
                _currentFormat = waveStream.WaveFormat;
                if (_currentFormat.wFormatTag != (short) WaveFormats.Pcm &&
                    _currentFormat.wFormatTag != (short) WaveFormats.Float)
                {
                    throw new Exception("Only PCM wav files are supported.");
                }

                _waveStream = waveStream;

                Length = _waveStream.Length/_waveStream.WaveFormat.nAvgBytesPerSec*1000;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        /// <summary>
        ///     Closes the wav.
        /// </summary>
        internal void CloseWav()
        {
            StopWav();
            if (_waveStream != null)
            {
                try
                {
                    _waveStream.Close();
                }
                finally
                {
                    _waveStream = null;
                }
            }
            IsPlaying = false;
        }

        /// <summary>
        ///     Stops the wav.
        /// </summary>
        internal void StopWav()
        {
            if (_waveOut != null)
            {
                try
                {
                    _waveOut.Dispose();
                }
                finally
                {
                    _waveOut = null;
                }
            }
        }

        /// <summary>
        ///     Plays the Wav.
        /// </summary>
        internal void PlayWav()
        {
            StopWav();
            if (_waveStream != null)
            {
                _waveStream.Position = 0;
                IsPlaying = true;
                _waveOut = new WaveOut(CurrentDevice.DeviceId, _currentFormat, 16384, 3, Filler);
            }
        }

        /// <summary>
        ///     Fills the WaveOut.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="size">The Size.</param>
        internal void Filler(IntPtr data, int size)
        {
            var b = new byte[size];
            if (_waveStream != null)
            {
                int pos = 0;
                while (pos < size)
                {
                    int toget = size - pos;
                    int got = _waveStream.Read(b, pos, toget);
                    if (got < toget && _playMode == PlayMode.Loop)
                    {
                        _waveStream.Position = 0;
                    }
                    pos += got;
                }

                _position = (_waveStream.Position/_waveStream.WaveFormat.nAvgBytesPerSec)*1000;
                IsPlaying = _waveStream.Position < _waveStream.Length;

                if (!IsPlaying)
                {
                    CloseWav();
                }
            }
            else
            {
                for (int i = 0; i < b.Length; i++)
                    b[i] = 0;
            }
            Marshal.Copy(b, 0, data, size);
        }
    }

#endif
}