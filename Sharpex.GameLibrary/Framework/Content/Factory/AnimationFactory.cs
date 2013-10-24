using System;
using System.IO;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering.Sprites;

namespace SharpexGL.Framework.Content.Factory
{
    public class AnimationFactory : IFactory<Animation>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType { get { return typeof (Animation); } }
        /// <summary>
        /// Creates a new Sound from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>Animation</returns>
        public Animation Create(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Open))
            {
                return new AnimationSerializer().Read(new BinaryReader(fileStream));
            }
        }
        /// <summary>
        /// Creates a new Sound from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Animation</returns>
        public Animation Create(Stream stream)
        {
            using (stream)
            {
                return new AnimationSerializer().Read(new BinaryReader(stream));
            }
        }
    }
}
