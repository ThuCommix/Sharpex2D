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
using Sharpex2D.Content;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AudioEffect : IContent
    {
        private readonly IAudioProvider _audioProvider;

        /// <summary>
        /// Initializes a new AudioEffect class.
        /// </summary>
        public AudioEffect() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new AudioEffect class.
        /// </summary>
        /// <param name="audioSource">The AudioSource.</param>
        public AudioEffect(AudioSource audioSource)
        {
            AudioSource = audioSource;
            var audioInitializer = SGL.QueryComponents<AudioManager>().AudioInitializer;
            var logger = LogManager.GetClassLogger();

            if (audioInitializer == null)
            {
                logger.Warn("The specified audio initializer was null.");
            }
            else if (!audioInitializer.IsSupported)
            {
                logger.Warn("The specified AudioProvider is not supported.");
            }
            else
            {
                _audioProvider = audioInitializer.Create();
                _audioProvider.PlaybackChanged += PlaybackChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AudioSource.
        /// </summary>
        public AudioSource AudioSource { set; get; }

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
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _audioProvider.PlaybackState; }
        }

        /// <summary>
        /// Initializes the audio effect.
        /// </summary>
        public void Initialize()
        {
            if (AudioSource == null) throw new NullReferenceException("AudioSource was null.");
            _audioProvider.Initialize(AudioSource.Instance);
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
        /// Plays the specified audio source.
        /// </summary>
        public void Play()
        {
            _audioProvider.Play(PlaybackMode.None);
        }
    }
}