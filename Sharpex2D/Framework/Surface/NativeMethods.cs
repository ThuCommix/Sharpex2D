using System;
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Surface
{
    public class NativeMethods
    {
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