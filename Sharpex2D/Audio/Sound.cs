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
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Sound : IContent
    {
        private readonly WaveFile _waveFile;

        /// <summary>
        /// Initializes a new Sound class.
        /// </summary>
        /// <param name="title">The Title.</param>
        /// <param name="album">The Album.</param>
        /// <param name="artist">The Artist.</param>
        /// <param name="year">The Year.</param>
        /// <param name="format">The WaveFormat.</param>
        /// <param name="waveFile">The WaveFile.</param>
        internal Sound(string title, string album, string artist, int year, WaveFormat format, WaveFile waveFile)
        {
            Title = title;
            Album = album;
            Artist = artist;
            Year = year == 0 ? (int?) null : year;
            Format = format;
            _waveFile = waveFile;
            Duration = new TimeSpan(0, 0, (int)_waveFile.WaveHeader.DataSize / Format.AvgBytesPerSec);
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { private set; get; }

        /// <summary>
        /// Gets the album.
        /// </summary>
        public string Album { private set; get; }

        /// <summary>
        /// Gets the artist.
        /// </summary>
        public string Artist { private set; get; }

        /// <summary>
        /// gets the year.
        /// </summary>
        public int? Year { private set; get; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        public WaveFormat Format { private set; get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration { private set; get; }

        /// <summary>
        /// Gets the audio data.
        /// </summary>
        /// <returns>Bytes.</returns>
        internal byte[] GetAudioData()
        {
            return _waveFile.GetAudioData();
        }
    }
}