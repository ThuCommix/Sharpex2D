using System.IO;
using Sharpex2D.Framework.Common.Extensions;

namespace Sharpex2D.Framework.Content.Serialization
{
   public class ByteArraySerializer : ContentSerializer<byte[]>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override byte[] Read(BinaryReader reader)
        {
            return reader.ReadAllBytes();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, byte[] value)
        {
            writer.Write(value);
        }
    }
}
