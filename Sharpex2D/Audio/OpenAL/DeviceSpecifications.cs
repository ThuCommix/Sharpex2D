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

namespace Sharpex2D.Framework.Audio.OpenAL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal enum DeviceSpecifications
    {
        /// <summary>
        /// Indicates the Version of ALC.
        /// </summary>
        Version = 0xB002,

        /// <summary>
        /// Indicates the default device specifier.
        /// </summary>
        DefaultDeviceSpecifier = 0x1004,

        /// <summary>
        /// Indicates the device specifier.
        /// </summary>
        DeviceSpecifier = 0x1005,

        /// <summary>
        /// Indicates the capture device specifier.
        /// </summary>
        CaptureDeviceSpecifier = 0x310,

        /// <summary>
        /// Indicates the capture default device specifier.
        /// </summary>
        CaptureDefaultDeviceSpecifier = 0x311,

        /// <summary>
        /// Indicates the all device specifier.
        /// </summary>
        AllDevicesSpecifier = 0x1013,

        /// <summary>
        /// Indicates the default all device specifier.
        /// </summary>
        DefaultAllDevicesSpecifier = 0x1012
    }
}