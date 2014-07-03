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
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Audio.WaveOut;
using Sharpex2D.Framework.Input.XInput;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class NativeMethods
    {
#if Windows

        public enum GdiRasterOperations
        {
            SRCCOPY = 13369376,
            SRCPAINT = 15597702,
            SRCAND = 8913094,
            SRCINVERT = 6684742,
            SRCERASE = 4457256,
            NOTSRCCOPY = 3342344,
            NOTSRCERASE = 1114278,
            MERGECOPY = 12583114,
            MERGEPAINT = 12255782,
            PATCOPY = 15728673,
            PATPAINT = 16452105,
            PATINVERT = 5898313,
            DSTINVERT = 5570569,
            BLACKNESS = 66,
            WHITENESS = 16711778
        }

        /// <summary>
        ///     Allocates the Console.
        /// </summary>
        /// <returns>Int32.</returns>
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int AllocConsole();

        /// <summary>
        ///     Gets the console handle.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();

        /// <summary>
        ///     Shows a window specified by its handle.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <param name="nCmdShow">The Command.</param>
        /// <returns>Int32.</returns>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        ///     Sets the console title.
        /// </summary>
        /// <param name="lpConsoleTitle">The Title.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool SetConsoleTitle([MarshalAs(UnmanagedType.LPWStr)] String lpConsoleTitle);

        /// <summary>
        ///     Gets the XInput state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pState">The InputState.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputGetState
            (
            int dwUserIndex,
            ref XInputState pState
            );

        /// <summary>
        ///     Sets the Input state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pVibration">The Vibration.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputSetState
            (
            int dwUserIndex,
            ref XInputVibration pVibration
            );

        /// <summary>
        ///     Gets the Capabilities.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="dwFlags">The dwFlags.</param>
        /// <param name="pCapabilities">The Capabilities.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputGetCapabilities
            (
            int dwUserIndex,
            int dwFlags,
            ref XInputCapabilities pCapabilities
            );

        /// <summary>
        ///     Gets the Battery information.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="devType">The DevType.</param>
        /// <param name="pBatteryInformation">The BatteryInformation.</param>
        /// <returns></returns>
        internal static int XInputGetBatteryInformation
            (
            int dwUserIndex,
            byte devType,
            ref XInputBatteryInformation pBatteryInformation
            )
        {
            return 0;
        }

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        ///     Deletes the specified device context (DC).
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <returns>
        ///     If the function succeeds, the return value is <c>true</c>. If the function fails, the return value is
        ///     <c>false</c>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        internal static extern bool DeleteDC([In] IntPtr hdc);

        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hObjSource, int nXSrc, int nYSrc, GdiRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        internal static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest,
            int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            GdiRasterOperations dwRop);

        /// <summary>
        ///     A value indicating whether the Window is valid.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <returns>True if window is valid</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

        #region WaveOutAPI

        /// <summary>
        ///     Gets the number of devices.
        /// </summary>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutGetNumDevs();

        /// <summary>
        /// Gets the dev caps.
        /// </summary>
        /// <param name="deviceID">The DeviceId.</param>
        /// <param name="waveOutCaps">The WaveOutCaps.</param>
        /// <param name="cbwaveOutCaps">The Size.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int waveOutGetDevCaps(uint deviceID, out WaveOutCaps waveOutCaps, uint cbwaveOutCaps);

        /// <summary>
        ///     Prepares the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutPrepareHeader(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        ///     Unprepares the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutUnprepareHeader(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        ///     Writes the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutWrite(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        ///     Opens a WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="uDeviceID">The DeviceHandle.</param>
        /// <param name="lpFormat">The WaveFormat.</param>
        /// <param name="dwCallback">The Callback.</param>
        /// <param name="dwInstance">The Instance.</param>
        /// <param name="dwFlags">The Flags.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutOpen(out IntPtr hWaveOut, uint uDeviceID, WaveFormat lpFormat,
            WaveDelegate dwCallback, IntPtr dwInstance, uint dwFlags);

        /// <summary>
        ///     Resets the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutReset(IntPtr hWaveOut);

        /// <summary>
        ///     Closes the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutClose(IntPtr hWaveOut);

        /// <summary>
        ///     Pauses the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutPause(IntPtr hWaveOut);

        /// <summary>
        ///     Restarts the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutRestart(IntPtr hWaveOut);

        /// <summary>
        ///     Sets the volume.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwVolume">The Volume.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutSetVolume(IntPtr hWaveOut, uint dwVolume);

        /// <summary>
        ///     Gets the Volume.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwVolume">The Volume.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutGetVolume(IntPtr hWaveOut, out uint dwVolume);

        /// <summary>
        ///     Gets the Pitch.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="pdwPitch">The Pitch.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutGetPitch(IntPtr hWaveOut, IntPtr pdwPitch);

        /// <summary>
        ///     Sets the Pitch.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwPitch">The Pitch.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutSetPitch(IntPtr hWaveOut, int dwPitch);

        /// <summary>
        ///     No Error.
        /// </summary>
        internal const int MMSYSERR_NOERROR = 0;

        /// <summary>
        ///     Open.
        /// </summary>
        internal const int MM_WOM_OPEN = 0x3BB;

        /// <summary>
        ///     Close.
        /// </summary>
        internal const int MM_WOM_CLOSE = 0x3BC;

        /// <summary>
        ///     Done.
        /// </summary>
        internal const int MM_WOM_DONE = 0x3BD;

        /// <summary>
        ///     Callbackfunction number.
        /// </summary>
        internal const int CALLBACK_FUNCTION = 0x00030000;

        /// <summary>
        ///     Time in ms.
        /// </summary>
        internal const int TIME_MS = 0x0001;

        /// <summary>
        ///     Time in samples.
        /// </summary>
        internal const int TIME_SAMPLES = 0x0002;

        /// <summary>
        ///     Time in bytes.
        /// </summary>
        internal const int TIME_BYTES = 0x0004;

        /// <summary>
        ///     WaveDelegate.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        /// <param name="message">The Message.</param>
        /// <param name="user">The User.</param>
        /// <param name="waveHeader">The WaveHeader.</param>
        /// <param name="reserved">Reserved for driver.</param>
        internal delegate void WaveDelegate(IntPtr handle, int message, IntPtr user, WaveHdr waveHeader, IntPtr reserved
            );

        #endregion

#endif
    }
}