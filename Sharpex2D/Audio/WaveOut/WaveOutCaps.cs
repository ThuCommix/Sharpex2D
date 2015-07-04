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


using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct WaveOutCaps
    {
        /// <summary>
        /// The Manufacturer.
        /// </summary>
        public short wMid;

        /// <summary>
        /// The ProductId.
        /// </summary>
        public short wPid;

        /// <summary>
        /// The DriverVersion.
        /// </summary>
        public uint vDriverVersion;

        /// <summary>
        /// The Devicename.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szPname;

        /// <summary>
        /// Supported formats.
        /// </summary>
        public WaveCapsFormats dwFormats;

        /// <summary>
        /// Amount of supported channels.
        /// </summary>
        public short wChannels;

        /// <summary>
        /// Driver reserved.
        /// </summary>
        public short wReserved1;

        /// <summary>
        /// </summary>
        public WaveCapsSupported dwSupport;
    }
}