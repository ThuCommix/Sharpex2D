using System;

namespace Sharpex2D.Framework.Media.Sound
{
    public class SoundProviderNotInitializedException : Exception
    {
        public SoundProviderNotInitializedException()
        {
            
        }

        public override string Message
        {
            get
            {
                return "ISoundProvider is not initialized. Use ISoundInitializer to initialize SoundManager.";
            }
        }
    }
}
