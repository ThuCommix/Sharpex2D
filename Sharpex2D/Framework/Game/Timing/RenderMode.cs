namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum RenderMode
    {
        /// <summary>
        ///     Limits the render call to a fixed fps amount.
        /// </summary>
        Limited,

        /// <summary>
        ///     No limitations.
        /// </summary>
        Unlimited
    }
}