namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface ITimeable
    {
        /// <summary>
        ///     Sets or gets the Interval.
        /// </summary>
        float Interval { get; set; }
    }
}