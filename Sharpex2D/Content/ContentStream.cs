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

using System.IO;

namespace Sharpex2D.Framework.Content
{
    public class ContentStream : FileStream
    {
        private readonly long _dataOffset;

        /// <summary>
        /// Initializes a new ContentStream class
        /// </summary>
        /// <param name="file">The File</param>
        /// <param name="offset">The Offset</param>
        internal ContentStream(string file, long offset)
            : base(file, FileMode.Open, FileAccess.Read)
        {
            _dataOffset = offset;
            base.Seek(_dataOffset, SeekOrigin.Begin);
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        public override long Length => base.Length - _dataOffset;

        /// <summary>
        /// Gets or sets the position
        /// </summary>
        public override long Position
        {
            get { return base.Position - _dataOffset; }
            set { Seek(value, SeekOrigin.Begin); }
        }

        /// <summary>
        /// Seeks the stream
        /// </summary>
        /// <param name="offset">The Offset</param>
        /// <param name="origin">The Origin</param>
        /// <returns>Long</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                return base.Seek(_dataOffset + offset, origin);
            }

            return base.Seek(offset, origin);
        }
    }
}
