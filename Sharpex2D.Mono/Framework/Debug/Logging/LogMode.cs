namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum LogMode
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     StandardOut stream.
        /// </summary>
        StandardOut = 1,

        /// <summary>
        ///     StandardError stream.
        /// </summary>
        StandardError = 2,
    }
}