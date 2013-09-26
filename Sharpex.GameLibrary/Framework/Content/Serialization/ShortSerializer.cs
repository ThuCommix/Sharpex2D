using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Content.Serialization
{
    public class ShortSerializer : ContentSerializer<Int16>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override short Read(BinaryReader reader)
        {
            return reader.ReadInt16();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, short value)
        {
            writer.Write(value);
        }
    }
}
