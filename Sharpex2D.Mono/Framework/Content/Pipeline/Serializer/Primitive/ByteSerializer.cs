using System.IO;
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Content.Pipeline.Serializer.Primitive
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class ByteSerializer : PrimitiveSerializer<byte>
    {
        /// <summary>
        ///     Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override byte Read(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        /// <summary>
        ///     Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }
    }
}