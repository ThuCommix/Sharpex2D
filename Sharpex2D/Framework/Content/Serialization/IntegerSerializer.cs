using System;
using System.IO;

namespace Sharpex2D.Framework.Content.Serialization
{
    public class IntegerSerializer : ContentSerializer<Int32>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override int Read(BinaryReader reader)
        {
            return reader.ReadInt32();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, int value)
        {
            writer.Write(value);
        }
    }
}
