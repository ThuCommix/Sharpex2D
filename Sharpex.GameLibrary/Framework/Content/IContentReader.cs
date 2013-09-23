using System.IO;

namespace SharpexGL.Framework.Content
{
    public interface IContentReader
    {
        /// <summary>
        /// Starts reading contentdata.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns>Object</returns>
        object Read(BinaryReader reader);
    }
}
