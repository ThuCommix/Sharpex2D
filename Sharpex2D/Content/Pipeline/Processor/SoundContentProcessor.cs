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
using System.Runtime.InteropServices;
using Sharpex2D.Audio;

namespace Sharpex2D.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class SoundContentProcessor : ContentProcessor<Sound>
    {
        /// <summary>
        /// Initializes a new SoundContentProcessor class.
        /// </summary>
        public SoundContentProcessor()
            : base(new Guid("3F5D0CE8-26B8-4034-8C3C-2F0DB2D6F25A"))
        {
        }

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Sound.</returns>
        public override Sound ReadData(string filepath)
        {
            return new Sound(filepath);
        }

        /// <summary>
        /// Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(Sound data, string destinationpath)
        {
            try
            {
                File.Copy(data.ResourcePath, destinationpath);
            }
            catch (Exception ex)
            {
                throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
            }
        }
    }
}