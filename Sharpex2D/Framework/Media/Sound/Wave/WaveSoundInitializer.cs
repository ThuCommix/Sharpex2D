namespace Sharpex2D.Framework.Media.Sound.Wave
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class WaveSoundInitializer : ISoundInitializer
    {
        #region ISoundInitializer Implementation

        /// <summary>
        ///     Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            return new WaveSoundProvider();
        }

        #endregion
    }
}