using System;
using System.IO;
using SharpexGL.Framework.Game.Services;

namespace SharpexGL.Framework.Content.Serialization
{
    public class GamerSerializer : ContentSerializer<Gamer>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Gamer Read(BinaryReader reader)
        {
            var displayName = reader.ReadString();
            var guid = new Guid(reader.ReadString());

            return new Gamer {DisplayName = displayName, Guid = guid};
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Gamer value)
        {
            writer.Write(value.DisplayName);
            writer.Write(value.Guid.ToString());
        }
    }
}
