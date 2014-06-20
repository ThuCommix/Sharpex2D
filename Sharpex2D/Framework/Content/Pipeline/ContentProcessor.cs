using System;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class ContentProcessor<T> : IContentProcessor where T : IContent
    {
        /// <summary>
        ///     Initializes a new ContentProcessor class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        protected ContentProcessor(Guid guid)
        {
            Guid = guid;
        }

        /// <summary>
        ///     Gets the Type.
        /// </summary>
        public Type Type
        {
            get { return typeof (T); }
        }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Object.</returns>
        object IContentProcessor.ReadData(string filepath)
        {
            return ReadData(filepath);
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        void IContentProcessor.WriteData(object data, string destinationpath)
        {
            WriteData((T) data, destinationpath);
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>T.</returns>
        public abstract T ReadData(string filepath);

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public abstract void WriteData(T data, string destinationpath);
    }
}