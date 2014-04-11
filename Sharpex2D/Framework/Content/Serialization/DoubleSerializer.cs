using System.IO;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class DoubleSerializer : ContentSerializer<double>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override double Read(BinaryReader reader)
        {
            return reader.ReadDouble();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, double value)
        {
            writer.Write(value);
        }
    }
}
