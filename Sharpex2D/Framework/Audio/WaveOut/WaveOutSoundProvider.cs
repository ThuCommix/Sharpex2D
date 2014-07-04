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
using System.Threading.Tasks;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Audio.WaveOut
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class WaveOutSoundProvider : ISoundProvider
    {
        #region ISoundProvider Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("B56F814F-5E22-4930-A22D-DC93D55173E3"); }
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            CloseWaveOutSession();
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

                lock (_lockObj)
                {
                    _currentWaveOut.SetBalance(left*volume, right*volume);
                }
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
                _volume = value;
                lock (_lockObj)
                {
                    _currentWaveOut.SetVolume(value);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public long Position
        {
            get
            {
                lock (_lockObj)
                {
                    return _currentWaveOut.GetPosition();
                }
            }
            set { Seek(value); }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                lock (_lockObj)
                {
                    return _currentWaveOut.IsPlaying();
                }
            }
        }

        /// <summary>
        ///     Gets the Length.
        /// </summary>
        public long Length
        {
            get
            {
                lock (_lockObj)
                {
                    return _currentWaveOut.GetLength();
                }
            }
        }

        /// <summary>
        ///     Gets the SoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { get; private set; }

        /// <summary>
        ///     Plays a sound.
        /// </summary>
        /// <param name="soundFile">The Sound.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound soundFile, PlayMode playMode)
        {
            CloseWaveOutSession();
            new Task(() =>
            {
                _currentWaveOut = new WaveOut(_audioDevice, soundFile.GetStream(),
                    new WaveOutBufferDescription(16384, 3));
            }).Start();
        }

        /// <summary>
        ///     Resumes a sound.
        /// </summary>
        public void Resume()
        {
            lock (_lockObj)
            {
                _currentWaveOut.Resume();
            }
        }

        /// <summary>
        ///     Pause a sound.
        /// </summary>
        public void Pause()
        {
            lock (_lockObj)
            {
                _currentWaveOut.Pause();
            }
        }

        /// <summary>
        ///     Seeks a sound.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            lock (_lockObj)
            {
                _currentWaveOut.Seek(position);
            }
        }

        #endregion

        private readonly WaveOutDevice _audioDevice;
        private readonly object _lockObj;
        private float _balance;
        private WaveOut _currentWaveOut;
        private float _volume;

        /// <summary>
        ///     Initializes a new WaveOutSoundProvider class.
        /// </summary>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        internal WaveOutSoundProvider(ISoundInitializer soundInitializer)
        {
            _lockObj = new object();
            SoundInitializer = soundInitializer;

            if (WaveOut.EnumerateDevices().Length == 0)
            {
                throw new InvalidOperationException("There is not existing WavOut device available.");
            }

            //standard device
            _audioDevice = WaveOut.EnumerateDevices()[0];

            if (!_audioDevice.IsSupported(WaveCapsSupported.WAVECAPS_LRVOLUME) ||
                !_audioDevice.IsSupported(WaveCapsSupported.WAVECAPS_VOLUME))
            {
                Logger logger = LogManager.GetClassLogger();
                logger.Warn("The current device does not support all required features.");
            }
        }

        /// <summary>
        ///     Closes the WaveOutSession.
        /// </summary>
        private void CloseWaveOutSession()
        {
            new Task(() =>
            {
                if (_currentWaveOut != null)
                {
                    _currentWaveOut.Dispose();
                    _currentWaveOut = null;
                }
            }).Start();
        }
    }

#endif
}