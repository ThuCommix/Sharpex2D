using System.IO;

namespace Sharpex2D.Framework.Content
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
