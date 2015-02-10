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
using Sharpex2D.Audio.Converters;

namespace Sharpex2D.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class WaveOutAudioSource : IAudioSource
    {
        internal byte[] WaveData;
        internal WaveFormat WaveFormat;

        /// <summary>
        /// Initializes a new WaveOutAudioSource class.
        /// </summary>
        /// <param name="waveStream">The WaveStream.</param>
        internal WaveOutAudioSource(WaveStream waveStream)
        {
            waveStream.Seek(0, SeekOrigin.Begin);
            WaveData = new byte[waveStream.Length];
            waveStream.Read(WaveData, 0, WaveData.Length);
            waveStream.Close();
            WaveFormat = waveStream.Format;

            if (WaveFormat.Channels == 1) //try to convert to stereo for full audiomixer support
            {
                try
                {
                    new MonoToStereoConverter().ConvertAudioData(WaveData, ref WaveFormat);
                }
                catch (NotSupportedException)
                {
                }
            }
        }

        #region IAudioSource Implementation

        /// <summary>
        /// Gets the Name of the AudioSource object.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}