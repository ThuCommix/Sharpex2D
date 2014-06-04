
namespace Sharpex2D.Framework.Media.Sound.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
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
