using System.IO;

namespace Sharpex2D.Framework.Content
{
    public interface IContentWriter
    {
        /// <summary>
        /// Starts writing contentdata.
        /// </summary>
        /// <param name="writer">The BinaryWriter</param>
        /// <param name="value">The Value.</param>
        void Write(BinaryWriter writer, object value);
    }
}
