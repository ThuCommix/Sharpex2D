
namespace Sharpex2D.Framework.Media.Sound.Wave
{
    public class WaveSoundInitializer : ISoundInitializer
    {
        #region ISoundInitializer Implementation
        /// <summary>
        /// Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            return new WaveSoundProvider();
        }
        #endregion
    }
}
