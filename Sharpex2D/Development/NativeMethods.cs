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
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHOpenALEnums THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;
using System.Security;
using Sharpex2D.Audio;
using Sharpex2D.Audio.WaveOut;
using Sharpex2D.Input;
using Sharpex2D.Input.Windows.JoystickApi;
using Sharpex2D.Input.Windows.Touch;
using Sharpex2D.Input.Windows.XInput;
using Sharpex2D.Rendering.OpenGL.Windows;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class NativeMethods
    {
#if Windows

        internal enum GdiRasterOperations
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
        /// Allocates the Console.
        /// </summary>
        /// <returns>Int32.</returns>
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int AllocConsole();

        /// <summary>
        /// Check if a Library is present.
        /// </summary>
        /// <param name="fileName">The FileName.</param>
        /// <returns>True if present.</returns>
        internal static bool CheckLibrary(string fileName)
        {
            return LoadLibrary(fileName) == IntPtr.Zero;
        }

        /// <summary>
        /// Gets the console handle.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Shows a window specified by its handle.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <param name="nCmdShow">The Command.</param>
        /// <returns>Int32.</returns>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Sets the console title.
        /// </summary>
        /// <param name="lpConsoleTitle">The Title.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool SetConsoleTitle([MarshalAs(UnmanagedType.LPWStr)] String lpConsoleTitle);

        /// <summary>
        /// Gets the XInput state.
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
        /// Sets the Input state.
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
        /// Gets the Capabilities.
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
        /// Gets the Battery information.
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
        /// Deletes the specified device context (DC).
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <returns>
        /// If the function succeeds, the return value is <c>true</c>. If the function fails, the return value is
        /// <c>false</c>.
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
        /// A value indicating whether the Window is valid.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <returns>True if window is valid</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        /// Gets the current key state.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Key.</returns>
        [DllImport("user32.dll")]
        internal static extern short GetAsyncKeyState(Keys key);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("gdi32.dll")]
        public static extern int SwapBuffers(IntPtr hDC);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int RelaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Gets the current render context.
        /// </summary>
        /// <returns>The current render context.</returns>
        [DllImport("opengl32.dll")]
        public static extern IntPtr wglGetCurrentContext();

        /// <summary>
        /// Make the specified render context current.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport("opengl32.dll")]
        public static extern int wglMakeCurrent(IntPtr hdc, IntPtr hrc);

        /// <summary>
        /// Creates a render context from the device context.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <returns>The handle to the render context.</returns>
        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        /// <summary>
        /// Deletes the render context.
        /// </summary>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport("opengl32.dll")]
        public static extern int wglDeleteContext(IntPtr hrc);

        [DllImport("gdi32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern int ChoosePixelFormat(IntPtr deviceContext, ref PixelFormatDescriptor pixelFormatDescriptor);

        public static bool SetPixelFormat(IntPtr deviceContext, int pixelFormat,
            ref PixelFormatDescriptor pixelFormatDescriptor)
        {
            LoadLibrary("opengl32.dll");
            return _SetPixelFormat(deviceContext, pixelFormat, ref pixelFormatDescriptor);
        }

        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat", SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern bool _SetPixelFormat(IntPtr deviceContext, int pixelFormat,
            ref PixelFormatDescriptor pixelFormatDescriptor);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("opengl32.dll")]
        public static extern bool wglUseFontBitmaps(IntPtr hDC, uint first, uint count, uint listBase);

        [DllImport("opengl32.dll")]
        public static extern IntPtr wglGetProcAddress(string name);

        public const uint WHITE_BRUSH = 0;
        public const uint LTGRAY_BRUSH = 1;
        public const uint GRAY_BRUSH = 2;
        public const uint DKGRAY_BRUSH = 3;
        public const uint BLACK_BRUSH = 4;
        public const uint NULL_BRUSH = 5;
        public const uint HOLLOW_BRUSH = NULL_BRUSH;
        public const uint WHITE_PEN = 6;
        public const uint BLACK_PEN = 7;
        public const uint NULL_PEN = 8;
        public const uint OEM_FIXED_FONT = 10;
        public const uint ANSI_FIXED_FONT = 11;
        public const uint ANSI_VAR_FONT = 12;
        public const uint SYSTEM_FONT = 13;
        public const uint DEVICE_DEFAULT_FONT = 14;
        public const uint DEFAULT_PALETTE = 15;
        public const uint SYSTEM_FIXED_FONT = 16;
        public const uint DEFAULT_GUI_FONT = 17;
        public const uint DC_BRUSH = 18;
        public const uint DC_PEN = 19;

        public const uint DEFAULT_PITCH = 0;
        public const uint FIXED_PITCH = 1;
        public const uint VARIABLE_PITCH = 2;

        public const uint DEFAULT_QUALITY = 0;
        public const uint DRAFT_QUALITY = 1;
        public const uint PROOF_QUALITY = 2;
        public const uint NONANTIALIASED_QUALITY = 3;
        public const uint ANTIALIASED_QUALITY = 4;
        public const uint CLEARTYPE_QUALITY = 5;
        public const uint CLEARTYPE_NATURAL_QUALITY = 6;

        public const uint CLIP_DEFAULT_PRECIS = 0;
        public const uint CLIP_CHARACTER_PRECIS = 1;
        public const uint CLIP_STROKE_PRECIS = 2;
        public const uint CLIP_MASK = 0xf;

        public const uint OUT_DEFAULT_PRECIS = 0;
        public const uint OUT_STRING_PRECIS = 1;
        public const uint OUT_CHARACTER_PRECIS = 2;
        public const uint OUT_STROKE_PRECIS = 3;
        public const uint OUT_TT_PRECIS = 4;
        public const uint OUT_DEVICE_PRECIS = 5;
        public const uint OUT_RASTER_PRECIS = 6;
        public const uint OUT_TT_ONLY_PRECIS = 7;
        public const uint OUT_OUTLINE_PRECIS = 8;
        public const uint OUT_SCREEN_OUTLINE_PRECIS = 9;
        public const uint OUT_PS_ONLY_PRECIS = 10;

        public const uint ANSI_CHARSET = 0;
        public const uint DEFAULT_CHARSET = 1;
        public const uint SYMBOL_CHARSET = 2;

        public const uint FW_DONTCARE = 0;
        public const uint FW_THIN = 100;
        public const uint FW_EXTRALIGHT = 200;
        public const uint FW_LIGHT = 300;
        public const uint FW_NORMAL = 400;
        public const uint FW_MEDIUM = 500;
        public const uint FW_SEMIBOLD = 600;
        public const uint FW_BOLD = 700;
        public const uint FW_EXTRABOLD = 800;
        public const uint FW_HEAVY = 900;

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement,
            int nOrientation, uint fnWeight, uint fdwItalic, uint fdwUnderline, uint
                fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint
                    fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

#endif
    }
}