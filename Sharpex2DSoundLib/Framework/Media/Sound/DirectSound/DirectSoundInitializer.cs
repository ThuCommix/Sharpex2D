
namespace Sharpex2D.Framework.Media.Sound.DirectSound
{
    public class DirectSoundInitializer : ISoundInitializer
    {
        /// <summary>
        /// Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            return new DirectSoundProvider();
        }
    }
}
