using System;
using SharpexGL.Framework.Media.Sound;

namespace SharpexGL.Framework.Content.Factory
{
    public class SoundFactory : IFactory<Sound>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType {
            get { return typeof (Sound); }
        }
        /// <summary>
        /// Creates a new Sound from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns></returns>
        public Sound Create(string file)
        {
            return new Sound(file);
        }
    }
}
