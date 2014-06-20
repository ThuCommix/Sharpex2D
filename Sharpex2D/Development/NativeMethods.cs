using System;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Input.XInput;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    internal static class NativeMethods
    {
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
    }
}