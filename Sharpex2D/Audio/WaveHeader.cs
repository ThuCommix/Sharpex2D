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

namespace Sharpex2D.Framework.Audio
{
    internal struct WaveHeader
    {
        /// <summary>
        /// Gets or sets the bit.
        /// </summary>
        public ushort Bit;

        /// <summary>
        /// Gets or sets the block size.
        /// </summary>
        public ushort BlockSize;

        /// <summary>
        /// Gets or sets the bytes per seconds.
        /// </summary>
        public uint BytesPerSec;

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public ushort Channels;

        /// <summary>
        /// Gets or sets the data id.
        /// </summary>
        public byte[] DataId;

        /// <summary>
        /// Gets or sets the data size.
        /// </summary>
        public uint DataSize;

        /// <summary>
        /// Gets or sets the fmt id.
        /// </summary>
        public byte[] FmtId;

        /// <summary>
        /// Gets or sets the fmt size.
        /// </summary>
        public uint FmtSize;

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public ushort Format;

        /// <summary>
        /// Gets or sets the riff id.
        /// </summary>
        public byte[] RiffId;

        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        public uint SampleRate;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public uint Size;

        /// <summary>
        /// Gets or sets the wav id.
        /// </summary>
        public byte[] WavId;
    }
}
