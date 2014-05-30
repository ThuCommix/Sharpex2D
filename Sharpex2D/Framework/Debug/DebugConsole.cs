using System;
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Debug
{
    public static class DebugConsole
    {
        /// <summary>
        /// Allocates the Console.
        /// </summary>
        /// <returns>Int32.</returns>
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        /// <summary>
        /// Gets the console handle.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        /// <summary>
        /// Shows a window specified by its handle.
        /// </summary>
        /// <param name="hWnd">The Handle.</param>
        /// <param name="nCmdShow">The Command.</param>
        /// <returns>Int32.</returns>
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// Sets the console title.
        /// </summary>
        /// <param name="lpConsoleTitle">The Title.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTitle(String lpConsoleTitle);

        private static bool _created;
        private static bool _isOpen;

        /// <summary>
        /// Open the DebugConsole.
        /// </summary>
        public static void Open()
        {
            Open("Sharpex2D DebugConsole");
        }
        /// <summary>
        /// Open the DebugConsole.
        /// </summary>
        /// <param name="debugName">The DebugName.</param>
        public static void Open(string debugName)
        {
            if (!_isOpen)
            {
                _isOpen = true;
                if (!_created)
                {
                    AllocConsole();
                    _created = true;
                }

                ShowWindow(GetConsoleWindow(), 5);
                SetConsoleTitle(debugName);
            }
        }
        /// <summary>
        /// Close the DebugConsole.
        /// </summary>
        public static void Close()
        {
            if (_isOpen)
            {
                _isOpen = false;
                ShowWindow(GetConsoleWindow(), 0);
            }
        }
    }
}
