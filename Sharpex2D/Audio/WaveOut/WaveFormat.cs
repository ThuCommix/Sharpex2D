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


using System.Runtime.InteropServices;

namespace Sharpex2D.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Sequential)]
    internal class WaveFormat
    {
        /// <summary>
        /// Waveform-audio format type.
        /// </summary>
        public short wFormatTag;

        /// <summary>
        /// Channels.
        /// </summary>
        public short nChannels;

        /// <summary>
        /// Samples per second.
        /// </summary>
        public int nSamplesPerSec;

        /// <summary>
        /// Average bytes per seconds.
        /// </summary>
        public int nAvgBytesPerSec;

        /// <summary>
        /// BlockAlign.
        /// </summary>
        public short nBlockAlign;

        /// <summary>
        /// Bits per sample.
        /// </summary>
        public short wBitsPerSample;

        /// <summary>
        /// Extra attributes.
        /// </summary>
        public short cbSize;

        /// <summary>
        /// Initializes a new WaveFormat class.
        /// </summary>
        /// <param name="rate">The SamplesPerSecond.</param>
        /// <param name="bits">The BitsPerSample.</param>
        /// <param name="channels">The Channels.</param>
        public WaveFormat(int rate, int bits, int channels)
        {
            wFormatTag = (short) WaveFormats.Pcm;
            nChannels = (short) channels;
            nSamplesPerSec = rate;
            wBitsPerSample = (short) bits;
            cbSize = 0;

            nBlockAlign = (short) (channels*(bits/8));
            nAvgBytesPerSec = nSamplesPerSec*nBlockAlign;
        }
    }
}