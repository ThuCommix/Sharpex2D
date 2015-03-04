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

namespace Sharpex2D.Input.Implementation.JoystickApi
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal enum JoyFlags
    {
        /// <summary>
        /// Returns all informations.
        /// </summary>
        JOY_RETURNALL =
            (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ | JOY_RETURNR | JOY_RETURNU | JOY_RETURNV | JOY_RETURNPOV |
             JOY_RETURNBUTTONS),

        /// <summary>
        /// Returns button information.
        /// </summary>
        JOY_RETURNBUTTONS = 128,

        /// <summary>
        /// Returns the centered value.
        /// </summary>
        JOY_RETURNCENTERED = 1024,

        /// <summary>
        /// Returns the pov.
        /// </summary>
        JOY_RETURNPOV = 64,

        /// <summary>
        /// Returns the pov cts.
        /// </summary>
        JOY_RETURNPOVCTS = 512,

        /// <summary>
        /// Returns uncalibrated values.
        /// </summary>
        JOY_RETURNRAWDATA = 256,

        /// <summary>
        /// Returns the X position.
        /// </summary>
        JOY_RETURNX = 1,

        /// <summary>
        /// Returns the Y position.
        /// </summary>
        JOY_RETURNY = 2,

        /// <summary>
        /// Returns the Z position.
        /// </summary>
        JOY_RETURNZ = 4,

        /// <summary>
        /// Returns the fourth axis.
        /// </summary>
        JOY_RETURNR = 8,

        /// <summary>
        /// Returns the fifth axis.
        /// </summary>
        JOY_RETURNU = 16,

        /// <summary>
        /// Returns the sixth axis.
        /// </summary>
        JOY_RETURNV = 32,

        /// <summary>
        /// User dead zone.
        /// </summary>
        JOY_USEDEADZONE = 2048
    }
}