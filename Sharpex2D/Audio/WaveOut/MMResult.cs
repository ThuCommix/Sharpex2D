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

namespace Sharpex2D.Framework.Audio.WaveOut
{
    internal enum MMResult
    {
        /// <summary>
        /// No error.
        /// </summary>
        MMSYSERR_NOERROR = 0,

        /// <summary>
        /// Error.
        /// </summary>
        MMSYSERR_ERROR = 1,

        /// <summary>
        /// Device not available.
        /// </summary>
        MMSYSERR_BADDEVICEID = 2,

        /// <summary>
        /// WaveOut is disabled.
        /// </summary>
        MMSYSERR_NOTENABLED = 3,

        /// <summary>
        /// Allocated.
        /// </summary>
        MMSYSERR_ALLOCATED = 4,

        /// <summary>
        /// The Handle is invalid.
        /// </summary>
        MMSYSERR_INVALHANDLE = 5,

        /// <summary>
        /// No driver available.
        /// </summary>
        MMSYSERR_NODRIVER = 6,

        /// <summary>
        /// Out of memory.
        /// </summary>
        MMSYSERR_NOMEM = 7,

        /// <summary>
        /// Not supported.
        /// </summary>
        MMSYSERR_NOTSUPPORTED = 8,

        /// <summary>
        /// Bad error number.
        /// </summary>
        MMSYSERR_BADERRNUM = 9,

        /// <summary>
        /// Invalid flag.
        /// </summary>
        MMSYSERR_INVALFLAG = 10,

        /// <summary>
        /// Invalid parameter.
        /// </summary>
        MMSYSERR_INVALPARAM = 11,

        /// <summary>
        /// WaveOut currently bussy.
        /// </summary>
        MMSYSERR_HANDLEBUSY = 12,

        /// <summary>
        /// Invalid alias.
        /// </summary>
        MMSYSERR_INVALIDALIAS = 13,

        /// <summary>
        /// ???.
        /// </summary>
        MMSYSERR_BADDB = 14,

        /// <summary>
        /// Key not found.
        /// </summary>
        MMSYSERR_KEYNOTFOUND = 15,

        /// <summary>
        /// Read error.
        /// </summary>
        MMSYSERR_READERROR = 16,

        /// <summary>
        /// Write error.
        /// </summary>
        MMSYSERR_WRITEERROR = 17,

        /// <summary>
        /// Delete error.
        /// </summary>
        MMSYSERR_DELETEERROR = 18,

        /// <summary>
        /// Value not found.
        /// </summary>
        MMSYSERR_VALNOTFOUND = 19,

        /// <summary>
        /// No driver cb.
        /// </summary>
        MMSYSERR_NODRIVERCB = 20,

        /// <summary>
        /// Bad format.
        /// </summary>
        WAVERR_BADFORMAT = 32,

        /// <summary>
        /// WaveOut still playing.
        /// </summary>
        WAVERR_STILLPLAYING = 33,

        /// <summary>
        /// The header is not prepared.
        /// </summary>
        WAVERR_UNPREPARED = 34
    }
}
