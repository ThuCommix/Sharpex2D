namespace Sharpex2D.Framework.Media.Sound.DirectSound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectSoundInitializer : ISoundInitializer
    {
        /// <summary>
        ///     Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            return new DirectSoundProvider();
        }
    }
}