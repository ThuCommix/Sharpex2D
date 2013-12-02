using System.IO;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Content.Serialization
{
    public class TypefaceSerializer : ContentSerializer<Typeface>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Typeface Read(BinaryReader reader)
        {
            var typeface = new Typeface
            {
                FamilyName = reader.ReadString(),
                Size = reader.ReadSingle(),
                Style = (TypefaceStyle) reader.ReadInt32()
            };
            return typeface;
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Typeface value)
        {
            writer.Write(value.FamilyName);
            writer.Write(value.Size);
            writer.Write((int)value.Style);
        }
    }
}
