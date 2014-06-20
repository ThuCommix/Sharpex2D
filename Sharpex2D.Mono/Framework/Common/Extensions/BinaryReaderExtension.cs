using System.IO;

namespace Sharpex2D.Framework.Common.Extensions
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class BinaryReaderExtension
    {
        private const int AllocatedBuffer = 1024;

        /// <summary>
        ///     Reads all Bytes (Alloc:1024).
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            var buffer = new byte[AllocatedBuffer];
            using (var mStream = new MemoryStream())
            {
                int readedBytes;
                while ((readedBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    mStream.Write(buffer, 0, readedBytes);
                }
                return mStream.ToArray();
            }
        }
    }
}