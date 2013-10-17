using System;
using System.IO;

namespace SharpexGL.Framework.Content.Serialization
{
    public class LongSerializer : ContentSerializer<Int64>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override long Read(BinaryReader reader)
        {
            return reader.ReadInt64();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, long value)
        {
            writer.Write(value);
        }
    }
}
