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

namespace Sharpex2D.Audio.Converters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class StereoToMonoConverter : IAudioDataConverter
    {
        /// <summary>
        /// Converts the AudioData.
        /// </summary>
        /// <param name="audioData">The AudioData.</param>
        /// <param name="format">The Inputformat. This also changes after processing into the output format.</param>
        /// <returns>Manipulated audio data.</returns>
        public byte[] ConvertAudioData(byte[] audioData, ref WaveFormat format)
        {
            if (format.Channels != 2) throw new InvalidOperationException("The source has to be stereo.");

            byte[] output = new byte[audioData.Length/2];
            int outputIndex = 0;

            switch (format.BitsPerSample)
            {
                case 8:
                    for (int n = 0; n < audioData.Length; n += 2)
                    {
                        var left = audioData[n];
                        var right = audioData[n];

                        byte mixed = (byte) ((left + right)/2);

                        output[outputIndex++] = mixed;
                    }
                    format = new WaveFormat(format.SamplesPerSec, 8, 1);
                    return output;
                case 16:
                    for (int n = 0; n < audioData.Length; n += 4)
                    {
                        int leftChannel = BitConverter.ToInt16(audioData, n);
                        int rightChannel = BitConverter.ToInt16(audioData, n + 2);
                        int mixed = (leftChannel + rightChannel)/2;
                        byte[] outSample = BitConverter.GetBytes((short) mixed);

                        output[outputIndex++] = outSample[0];
                        output[outputIndex++] = outSample[1];
                    }
                    format = new WaveFormat(format.SamplesPerSec, 16, 1);
                    return output;
                default:
                    throw new NotSupportedException(string.Format("{0}Bits are not supported.", format.BitsPerSample));
            }
        }
    }
}