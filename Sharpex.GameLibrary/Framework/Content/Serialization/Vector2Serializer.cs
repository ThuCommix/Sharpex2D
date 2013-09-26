using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Content.Serialization
{
    public class Vector2Serializer : ContentSerializer<Vector2>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Vector2 Read(BinaryReader reader)
        {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Vector2 value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
        }
    }
}
