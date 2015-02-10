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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class AudioMixer
    {
        private float _pan;
        private float _volume;

        /// <summary>
        /// Initializes a new AudioMixer class.
        /// </summary>
        public AudioMixer()
        {
            Volume = 1f;
            Pan = 0;
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public float Volume
        {
            set
            {
                if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("value");

                _volume = value;
            }
            get { return _volume; }
        }

        /// <summary>
        /// Gets or sets the Pan.
        /// </summary>
        public float Pan
        {
            set
            {
                if (value < -1 || value > 1) throw new ArgumentOutOfRangeException("value");

                _pan = value;
            }
            get { return _pan; }
        }

        /// <summary>
        /// Applys the effects of the audio mixer to the given audio data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="format">The WaveFormat.</param>
        /// <remarks>Currently supports volume and panning for stereo sources and volume only for mono sources.</remarks>
        public void ApplyEffects(byte[] data, WaveFormat format)
        {
            if (format.BitsPerSample != 8 && format.BitsPerSample != 16 || format.Channels != 2)
            {
                return;
            }

            var left = System.Math.Min(1, Pan + 1);
            var right = System.Math.Abs(System.Math.Max(-1, Pan - 1));
            left *= Volume;
            right *= Volume;

            if (format.Channels == 2)
            {
                switch (format.BitsPerSample)
                {
                    case 8:
                        for (var n = 0; n < data.Length; n += 2)
                        {
                            var leftChannel = data[n];
                            var rightChannel = data[n + 1];

                            data[n] = (byte) (leftChannel*left);
                            data[n + 1] = (byte) (rightChannel*right);
                        }
                        break;
                    case 16:
                        for (int n = 0; n < data.Length; n += 4)
                        {
                            int leftChannel = BitConverter.ToInt16(data, n);
                            int rightChannel = BitConverter.ToInt16(data, n + 2);

                            byte[] sampleleft = BitConverter.GetBytes((short) (leftChannel*left));
                            byte[] sampleright = BitConverter.GetBytes((short) (rightChannel*right));


                            data[n] = sampleleft[0];
                            data[n + 1] = sampleleft[1];
                            data[n + 2] = sampleright[0];
                            data[n + 3] = sampleright[1];
                        }
                        break;
                }
            }
            else if (format.Channels == 1)
            {
                switch (format.BitsPerSample)
                {
                    case 8:
                        for (var n = 0; n < data.Length; n += 1)
                        {
                            var channel = data[n];

                            data[n] = (byte) (channel*_volume);
                        }
                        break;
                    case 16:
                        for (int n = 0; n < data.Length; n += 2)
                        {
                            int channel = BitConverter.ToInt16(data, n);

                            byte[] sample = BitConverter.GetBytes((short) (channel*_volume));


                            data[n] = sample[0];
                            data[n + 1] = sample[1];
                        }
                        break;
                }
            }
        }
    }
}