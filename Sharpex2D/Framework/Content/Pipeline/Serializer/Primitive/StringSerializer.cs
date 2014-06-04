using System.IO;
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Content.Pipeline.Serializer.Primitive
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class StringSerializer : PrimitiveSerializer<string>
    {
        /// <summary>
        ///     Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override string Read(BinaryReader reader)
        {
            return reader.ReadString();
        }

        /// <summary>
        ///     Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, string value)
        {
            writer.Write(value);
        }
    }
}