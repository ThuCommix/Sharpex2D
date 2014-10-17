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
using Sharpex2D.Common;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AudioManager : Singleton<AudioManager>, IComponent
    {
        private readonly ISoundInitializer _soundInitializer;
        private readonly ISoundProvider _soundProvider;

        /// <summary>
        /// Initializes a new AudioManager class.
        /// </summary>
        public AudioManager()
        {
            ISoundInitializer soundInitializer = SGL.QueryComponents<EngineConfiguration>().SoundInitializer;
            if (soundInitializer == null)
            {
                LogManager.GetClassLogger().Warn("No suitable audio interface found.");
                return;
                //throw new AudioException("No suitable audio interface found.");
            }

            if (!soundInitializer.IsSupported)
            {
                LogManager.GetClassLogger().Warn("The specified audio interface is not supported.");
                return;
                //throw new AudioException("The specified audio interface is not supported.");
            }

            SoundEffectGroups = new List<SoundEffectGroup>();
            _soundInitializer = soundInitializer;
            _soundProvider = soundInitializer.Create();
            _soundProvider.PlaybackChanged += PlaybackChanged;
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _soundProvider.PlaybackState; }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public long Length
        {
            get { return _soundProvider.Length; }
        }

        /// <summary>
        /// Gets the Position.
        /// </summary>
        public long Position
        {
            set { Seek(value); }
            get { return _soundProvider.Position; }
        }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _soundProvider.Volume; }
            set { _soundProvider.Volume = value; }
        }

        /// <summary>
        /// Gets or sets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _soundProvider.Balance; }
            set { _soundProvider.Balance = value; }
        }

        /// <summary>
        /// Gets the SoundEffectGroups.
        /// </summary>
        public List<SoundEffectGroup> SoundEffectGroups { private set; get; }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("7C7E6EA0-45BE-457C-8726-463E0D5B72BC"); }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event PlaybackChangedEventHandler PlaybackStateChanged;

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PlaybackChanged(object sender, EventArgs e)
        {
            if (PlaybackStateChanged != null)
                PlaybackStateChanged(this, e);
        }

        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The Sound.</param>
        public void Play(Sound sound)
        {
            _soundProvider.Play(sound);
        }

        /// <summary>
        /// Pause the current playback.
        /// </summary>
        public void Pause()
        {
            _soundProvider.Pause();
        }

        /// <summary>
        /// Resumes the current playback.
        /// </summary>
        public void Resume()
        {
            _soundProvider.Resume();
        }

        /// <summary>
        /// Stops the current playback.
        /// </summary>
        public void Stop()
        {
            _soundProvider.Stop();
        }

        /// <summary>
        /// Seeks to the specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            _soundProvider.Seek(position);
        }

        /// <summary>
        /// Creates a new Instance of ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider.</returns>
        internal ISoundProvider CreateInstance()
        {
            return _soundInitializer.Create();
        }
    }
}