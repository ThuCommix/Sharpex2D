using System.IO;

namespace SharpexGL.Framework.Content
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
