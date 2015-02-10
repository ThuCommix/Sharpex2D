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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Explicit)]
    public class WaveFormat
    {
        /// <summary>
        /// Waveform-audio format type.
        /// </summary>
        [FieldOffset(0)] public WaveFormats Format;

        /// <summary>
        /// Channels.
        /// </summary>
        [FieldOffset(2)] public short Channels;

        /// <summary>
        /// Samples per second.
        /// </summary>
        [FieldOffset(4)] public int SamplesPerSec;

        /// <summary>
        /// Average bytes per seconds.
        /// </summary>
        [FieldOffset(8)] public int AvgBytesPerSec;

        /// <summary>
        /// BlockAlign.
        /// </summary>
        [FieldOffset(12)] public short BlockAlign;

        /// <summary>
        /// Bits per sample.
        /// </summary>
        [FieldOffset(14)] public short BitsPerSample;

        /// <summary>
        /// Extra attributes.
        /// </summary>
        [FieldOffset(16)] internal short cbSize;

        /// <summary>
        /// Initializes a new WaveFormat struct.
        /// </summary>
        public WaveFormat()
            : this(22050, 16, 2)
        {
        }

        /// <summary>
        /// Initializes a new WaveFormat struct.
        /// </summary>
        /// <param name="rate">The SamplesPerSecond.</param>
        /// <param name="bits">The BitsPerSample.</param>
        /// <param name="channels">The Channels.</param>
        public WaveFormat(int rate, int bits, int channels)
        {
            Format = WaveFormats.Pcm;
            Channels = (short) channels;
            SamplesPerSec = rate;
            BitsPerSample = (short) bits;
            cbSize = 0;

            BlockAlign = (short) (channels*(bits/8));
            AvgBytesPerSec = SamplesPerSec*BlockAlign;
        }
    }
}