namespace Sharpex2D.Framework.Media.Video
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IVideoInitializer
    {
        /// <summary>
        ///     Creates the IVideoProvider.
        /// </summary>
        /// <returns>IVideoProvider</returns>
        IVideoProvider CreateProvider();
    }
}