using System;
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class NativeMethods
    {
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
    }
}