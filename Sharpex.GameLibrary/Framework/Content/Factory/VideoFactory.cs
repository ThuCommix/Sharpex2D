using System;
using System.IO;
using SharpexGL.Framework.Media.Video;

namespace SharpexGL.Framework.Content.Factory
{
    public class VideoFactory : IFactory<Video>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType
        {
            get { return typeof(Video); }
        }
        /// <summary>
        /// Creates a new Video from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>Video</returns>
        public Video Create(string file)
        {
            return new Video(file);
        }
        /// <summary>
        /// Creates a new Video from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Video</returns>
        public Video Create(Stream stream)
        {
            throw new NotSupportedException("Video create via stream is not supported yet.");
        }
    }
}
