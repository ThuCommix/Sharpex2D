namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum SGLState
    {
        /// <summary>
        ///     SGL is not initialized.
        /// </summary>
        NotInitialized = 0,

        /// <summary>
        ///     SGL is currently initializing.
        /// </summary>
        Initializing = 1,

        /// <summary>
        ///     SGL is initialized.
        /// </summary>
        Initialized = 2,

        /// <summary>
        ///     SGL is running.
        /// </summary>
        Running = 3
    }
}