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
using System.IO;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    public class ALSoundPlayer : ISoundPlayer
    {
        /// <summary>
        /// Gets or sets the pan
        /// </summary>
        public float Pan
        {
            get { return _alPlayback?.Position ?? 0; }
            set
            {
                if (_alPlayback != null)
                {
                    _alPlayback.Pan = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume
        {
            get { return _alPlayback?.Volume ?? 0; }
            set
            {
                if (_alPlayback != null)
                {
                    _alPlayback.Volume = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the position
        /// </summary>
        public long Position
        {
            get { return _alPlayback?.Position ?? 0; }
            set
            {
                if (_alPlayback != null)
                {
                    _alPlayback.Position = value;
                }
            }
        }

        /// <summary>
        /// Gets the playback state
        /// </summary>
        public PlaybackState PlaybackState => _alPlayback?.PlaybackState ?? PlaybackState.Stopped;

        /// <summary>
        /// Gets the audio length in ms
        /// </summary>
        public long Length => _alPlayback?.Length ?? 0;

        /// <summary>
        /// Raises when the playback changed
        /// </summary>
        public event EventHandler<EventArgs> PlaybackChanged;

        private ALPlayback _alPlayback;
        private ALDevice _alDevice;

        /// <summary>
        /// Initializes a new ALSoundPlayer class
        /// </summary>
        public ALSoundPlayer()
        {
            _alDevice = ALDevice.DefaultDevice;
            _alDevice.Initialize();
        }

        /// <summary>
        /// Initializes the openal sound player
        /// </summary>
        /// <param name="stream">The Stream</param>
        /// <param name="format">The Format</param>
        public void Initialize(Stream stream, WaveFormat format)
        {
            if (_alPlayback != null)
            {
                _alPlayback.Stop();
                _alPlayback.Dispose();
            }

            _alPlayback = new ALPlayback(_alDevice);
            _alPlayback.PlaybackChanged += PlaybackChanged;
            _alPlayback.Initialize(stream, format);
        }

        /// <summary>
        /// Plays the stream
        /// </summary>
        /// <param name="playbackMode">The playback mode</param>
        public void Play(PlaybackMode playbackMode)
        {
            _alPlayback?.Play(playbackMode);
        }

        /// <summary>
        /// Resumes the stream
        /// </summary>
        public void Resume()
        {
            _alPlayback?.Resume();
        }

        /// <summary>
        /// Pause the stream
        /// </summary>
        public void Pause()
        {
            _alPlayback?.Pause();
        }

        /// <summary>
        /// Stops the stream
        /// </summary>
        public void Stop()
        {
            _alPlayback?.Stop();
        }

        /// <summary>
        /// Seeks the stream
        /// </summary>
        /// <param name="position">The position</param>
        public void Seek(long position)
        {
            if (_alPlayback != null)
            {
                _alPlayback.Position = position;
            }
        }

        /// <summary>
        /// Disposes the openal sound player
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the openal sound player
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
               _alPlayback?.Dispose();
            }
        }
    }
}
