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
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class MediaPlayer : IComponent, IDisposable
    {
        internal readonly SoundManager SoundManager;
        private readonly List<SoundEffectGroup> _audioEffectGroups;
        private readonly ISoundPlayer _audioProvider;

        /// <summary>
        /// Initializes a new MediaPlayer class.
        /// </summary>
        /// <param name="soundManager">The SoundManager.</param>
        internal MediaPlayer(SoundManager soundManager)
        {
            _audioEffectGroups = new List<SoundEffectGroup>();
            Logger logger = LogManager.GetClassLogger();
            Queue = new Queue<Sound>();

            if (soundManager == null)
            {
                logger.Warn("The specified audio initializer was null.");
            }
            else if (!soundManager.IsSupported)
            {
                logger.Warn("The specified AudioProvider is not supported.");
            }
            else
            {
                SoundManager = soundManager;
                _audioProvider = soundManager.Create();
                _audioProvider.PlaybackChanged += PlaybackChanged;


#if DEBUG
                MetaDataCollection metadata = MetaDataReader.ReadMetaData(_audioProvider);
                if (metadata.ContainsKey("Title"))
                {
                    logger.Info("Audiosystem initialized with {0}.", metadata["Title"]);
                }
                else
                {
                    logger.Info("Audiosystem initialized with unknown.");
                }
#endif
            }
        }

        /// <summary>
        /// Gets an array of all AudioEffectGroups of this instance.
        /// </summary>
        public SoundEffectGroup[] AudioEffectGroups
        {
            get { return _audioEffectGroups.ToArray(); }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _audioProvider.PlaybackState; }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public long Length
        {
            get { return _audioProvider.Length; }
        }

        /// <summary>
        /// Gets the Position.
        /// </summary>
        public long Position
        {
            set { Seek(value); }
            get { return _audioProvider.Position; }
        }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _audioProvider.Volume; }
            set { _audioProvider.Volume = value; }
        }

        /// <summary>
        /// Gets or sets the Pan.
        /// </summary>
        public float Pan
        {
            get { return _audioProvider.Pan; }
            set { _audioProvider.Pan = value; }
        }

        /// <summary>
        /// Gets the sound queue.
        /// </summary>
        public Queue<Sound> Queue { private set; get; }

        #region IComponent Implementation

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("825A61CB-7761-4574-AA50-AA41BDBC4951"); }
        }

        #endregion

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (_audioProvider != null) _audioProvider.Dispose();
        }

        /// <summary>
        /// Raises when the playback state changed.
        /// </summary>
        public event EventHandler<EventArgs> PlaybackStateChanged;

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PlaybackChanged(object sender, EventArgs e)
        {
            if (PlaybackStateChanged != null)
                PlaybackStateChanged(this, e);

            if (PlaybackState == PlaybackState.Stopped)
            {
                if (Queue.Count > 0)
                {
                    var sound = Queue.Dequeue();
                    Initialize(sound);
                    Play();
                }
            }
        }

        /// <summary>
        /// Initializes the audio manager with the given sound source.
        /// </summary>
        /// <param name="sound">The Sound.</param>
        public void Initialize(Sound sound)
        {
            _audioProvider.Initialize(sound.GetStream());
        }

        /// <summary>
        /// Plays the specified audio source.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        public void Play(PlaybackMode playbackMode)
        {
            _audioProvider.Play(playbackMode);
        }

        /// <summary>
        /// Plays the specified audio source.
        /// </summary>
        public void Play()
        {
            Play(PlaybackMode.None);
        }

        /// <summary>
        /// Pause the current playback.
        /// </summary>
        public void Pause()
        {
            _audioProvider.Pause();
        }

        /// <summary>
        /// Resumes the current playback.
        /// </summary>
        public void Resume()
        {
            _audioProvider.Resume();
        }

        /// <summary>
        /// Stops the current playback.
        /// </summary>
        public void Stop()
        {
            _audioProvider.Stop();
        }

        /// <summary>
        /// Seeks to the specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _audioProvider.Seek(position);
        }

        /// <summary>
        /// Adds an AudioEffectGroup to this instance.
        /// </summary>
        /// <param name="audioEffectGroup">The AudioEffectGroup.</param>
        internal void AddEffectGroup(SoundEffectGroup audioEffectGroup)
        {
            _audioEffectGroups.Add(audioEffectGroup);
        }
    }
}