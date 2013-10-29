using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharpexGL.Framework.Game.Services;

namespace SharpexGL.Framework.Content.Serialization
{
    public class GameSettingsSerializer : ContentSerializer<GameSettings>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override GameSettings Read(BinaryReader reader)
        {
            return (GameSettings)new BinaryFormatter().Deserialize(reader.BaseStream);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, GameSettings value)
        {
            using (var mStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(mStream, value);
                writer.Write(mStream.ToArray(), 0, mStream.ToArray().Length);
            }
        }
    }
}
