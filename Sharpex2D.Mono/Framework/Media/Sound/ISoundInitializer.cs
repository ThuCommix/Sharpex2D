namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface ISoundInitializer
    {
        /// <summary>
        ///     Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        ISoundProvider CreateProvider();
    }
}