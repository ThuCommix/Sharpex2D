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

namespace Sharpex2D.Audio.WaveOut
{
#if Windows
    public class WaveOutProvider : ISoundProvider
    {
        private readonly WaveOut _waveOut;

        /// <summary>
        /// Initializes a new WaveOutProvider class.
        /// </summary>
        public WaveOutProvider()
        {
            _waveOut = new WaveOut();
            _waveOut.PlaybackChanged += PlaybackChangedEvent;
        }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("4ADFDB7D-984E-476E-9EF3-46B0ED8C7F6A"); }
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        public event PlaybackChangedEventHandler PlaybackChanged;

        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _waveOut.Balance; }
            set { _waveOut.Balance = value; }
        }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _waveOut.Volume; }
            set { _waveOut.Volume = value; }
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _waveOut.WaveStream.Length/_waveOut.WaveStream.Format.nAvgBytesPerSec*1000; }
            set { Seek(value); }
        }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        public PlaybackState PlaybackState
        {
            get { return _waveOut.PlaybackState; }
        }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _waveOut.WaveStream.Length/_waveOut.WaveStream.Format.nAvgBytesPerSec*1000; }
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="soundFile">The Soundfile.</param>
        public void Play(Sound soundFile)
        {
            _waveOut.Dispose();
            _waveOut.Initialize(new WaveStream(new MemoryStream(soundFile.Data)));
            _waveOut.Play();
        }

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _waveOut.Resume();
        }

        /// <summary>
        /// Pause a sound.
        /// </summary>
        public void Pause()
        {
            _waveOut.Pause();
        }

        /// <summary>
        /// Stops the sound.
        /// </summary>
        public void Stop()
        {
            _waveOut.Stop();
        }

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            long requestesPostion = position/1000*_waveOut.WaveStream.Format.nAvgBytesPerSec;
            _waveOut.WaveStream.Position = requestesPostion;
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            _waveOut.Dispose();
        }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PlaybackChangedEvent(object sender, EventArgs e)
        {
            if (PlaybackChanged != null)
            {
                PlaybackChanged(this, e);
            }
        }
    }
#endif
}