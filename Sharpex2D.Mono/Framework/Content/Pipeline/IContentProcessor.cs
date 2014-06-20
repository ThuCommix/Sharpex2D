using System;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IContentProcessor
    {
        /// <summary>
        ///     Gets the Type.
        /// </summary>
        Type Type { get; }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Object.</returns>
        object ReadData(string filepath);

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        void WriteData(object data, string destinationpath);
    }
}