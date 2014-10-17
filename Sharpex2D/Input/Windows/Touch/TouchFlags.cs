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

namespace Sharpex2D.Input.Windows.Touch
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal enum TouchFlags
    {
        /// <summary>
        /// Movement has occurred. Cannot be combined with TOUCHEVENTF_DOWN.
        /// </summary>
        TOUCHEVENTF_MOVE = 0x0001,

        /// <summary>
        /// The corresponding touch point was established through a new contact. Cannot be combined with TOUCHEVENTF_MOVE or
        /// TOUCHEVENTF_UP.
        /// </summary>
        TOUCHEVENTF_DOWN = 0x0002,

        /// <summary>
        /// A touch point was removed.
        /// </summary>
        TOUCHEVENTF_UP = 0x0004,

        /// <summary>
        /// A touch point is in range. This flag is used to enable touch hover support on compatible hardware.
        /// Applications that do not want support for hover can ignore this flag.
        /// </summary>
        TOUCHEVENTF_INRANGE = 0x0008,

        /// <summary>
        /// Indicates that this TOUCHINPUT structure corresponds to a primary contact point.
        /// See the following text for more information on primary touch points.
        /// </summary>
        TOUCHEVENTF_PRIMARY = 0x0010,

        /// <summary>
        /// When received using GetTouchInputInfo, this input was not coalesced.
        /// </summary>
        TOUCHEVENTF_NOCOALESCE = 0x0020,

        /// <summary>
        /// The touch event came from the user's palm.
        /// </summary>
        TOUCHEVENTF_PALM = 0x0080,
    }
}