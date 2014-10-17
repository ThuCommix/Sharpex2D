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

namespace Sharpex2D.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Flags]
    internal enum WaveHeaderFlags
    {
        /// <summary>
        /// Header done.
        /// </summary>
        WHDR_DONE = 0x00000001,

        /// <summary>
        /// Header prepared.
        /// </summary>
        WHDR_PREPARED = 0x00000002,

        /// <summary>
        /// Header begin loop.
        /// </summary>
        WHDR_BEGINLOOP = 0x00000004,

        /// <summary>
        /// Header end loop.
        /// </summary>
        WHDR_ENDLOOP = 0x00000008,

        /// <summary>
        /// Header in queue.
        /// </summary>
        WHDR_INQUEUE = 0x00000010
    }
}