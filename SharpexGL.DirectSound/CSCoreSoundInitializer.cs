
namespace SharpexGL.Framework.Media.Sound.DiectSound
{
    public class CSCoreSoundInitializer : ISoundInitializer
    {
        public ISoundProvider CreateProvider()
        {
            return new CSCoreSoundProvider();
        }
    }
}
