using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Content.Serialization
{
    public class BooleanSerializer : ContentSerializer<bool>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override bool Read(BinaryReader reader)
        {
            return reader.ReadBoolean();
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, bool value)
        {
            writer.Write(value);
        }
    }
}
