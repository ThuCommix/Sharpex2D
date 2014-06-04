namespace Sharpex2D.Framework.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class DebugConsole
    {
        private static bool _created;
        private static bool _isOpen;

        /// <summary>
        ///     Open the DebugConsole.
        /// </summary>
        public static void Open()
        {
            Open("Sharpex2D DebugConsole");
        }

        /// <summary>
        ///     Open the DebugConsole.
        /// </summary>
        /// <param name="debugName">The DebugName.</param>
        public static void Open(string debugName)
        {
            if (!_isOpen)
            {
                _isOpen = true;
                if (!_created)
                {
                    NativeMethods.AllocConsole();
                    _created = true;
                }

                NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), 5);
                NativeMethods.SetConsoleTitle(debugName);
            }
        }

        /// <summary>
        ///     Close the DebugConsole.
        /// </summary>
        public static void Close()
        {
            if (_isOpen)
            {
                _isOpen = false;
                NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), 0);
            }
        }
    }
}