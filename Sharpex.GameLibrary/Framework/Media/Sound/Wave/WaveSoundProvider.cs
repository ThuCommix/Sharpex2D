using System;
using System.Windows.Media;
using SharpexGL.Framework.Common.Threads;

namespace SharpexGL.Framework.Media.Sound.Wave
{
    public class WaveSoundProvider : ISoundProvider
    {
        #region ISoundProvider Implementation

        /// <summary>
        /// Disposes the WaveSoundProvider class.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Clonses the WaveSoundProvider class.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="soundFile">The Soundfile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound soundFile, PlayMode playMode)
        {
            _sound = soundFile;
            _playMode = playMode;
            _threadInvoker.Invoke(() =>
            {
                _soundPlayer.Open(new Uri(soundFile.ResourcePath));
                _soundPlayer.Play();
            });
            IsPlaying = true;
        }

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _threadInvoker.Invoke(() => _soundPlayer.Play());
            IsPlaying = true;
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _threadInvoker.Invoke(() => _soundPlayer.Pause());
            IsPlaying = false;
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _threadInvoker.Invoke(() => _soundPlayer.Position = new TimeSpan(0, 0, 0, 0, (int)position));
        }

        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get
            {
                var balance = 0f;
                _threadInvoker.Invoke(() =>
                {
                    balance = (float)_soundPlayer.Balance;
                });

                return balance;
            }
            set
            {
                _threadInvoker.Invoke(() => _soundPlayer.Balance = value);
            }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get
            {
                var volume = 0f;
                _threadInvoker.Invoke(() =>
                {
                    volume = (float)_soundPlayer.Volume;
                });

                return volume;
            }
            set
            {
                _threadInvoker.Invoke(() => _soundPlayer.Volume = value);
            }
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get
            {
                long position = 0;
                _threadInvoker.Invoke(() =>
                {
                    position = (long)_soundPlayer.Position.TotalMilliseconds;
                });

                return position;
            }
            set
            {
                Seek(value);
            }
        }

        /// <summary>
        /// A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get
            {
                long length = 0;
                _threadInvoker.Invoke(() =>
                {
                    length = (long) _soundPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                });

                return length;
            }
        }

        #endregion

        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("E0A6D82A-9841-4E99-A170-6691805043DC"); }
        }

        #endregion

        private readonly IThreadInvoker _threadInvoker;
        private MediaPlayer _soundPlayer;
        private PlayMode _playMode;
        private Sound _sound;

        /// <summary>
        /// Initialize a new WaveSoundProvider class.
        /// </summary>
        internal WaveSoundProvider()
        {
            _threadInvoker = new ThreadInvoker();
            _threadInvoker.Invoke(() =>
            {
                _soundPlayer = new MediaPlayer();
                _soundPlayer.MediaEnded += _soundPlayer_MediaEnded;
            });
        }

        private void _soundPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (_playMode == PlayMode.Loop)
            {
                Play(_sound, PlayMode.Loop);
            }
            IsPlaying = false;
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        public void Play(Sound sound)
        {
            Play(sound, PlayMode.None);
        }
    }
}
