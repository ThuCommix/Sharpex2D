using System.IO;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class FloatSerializer : ContentSerializer<float>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override float Read(BinaryReader reader)
        {
            return reader.ReadSingle();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, float value)
        {
            writer.Write(value);
        }
    }
}
