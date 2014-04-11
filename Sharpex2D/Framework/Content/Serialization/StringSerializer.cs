using System.IO;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class StringSerializer : ContentSerializer<string>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override string Read(BinaryReader reader)
        {
            return reader.ReadString();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, string value)
        {
            writer.Write(value);
        }
    }
}
