
namespace Sharpex2D.Framework.Media.Sound.WaveOut
{
    public class WaveOutSoundInitializer : ISoundInitializer
    {
        /// <summary>
        /// Creates a new SoundProvider.
        /// </summary>
        /// <returns>SoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            return new WaveOutSoundProvider();
        }
    }
}
