using System.IO;

namespace SharpexGL.Framework.Common.Extensions
{
    public static class BinaryReaderExtension
    {
        private const int AllocatedBuffer = 1024;
        /// <summary>
        /// Reads all Bytes (Alloc:1024).
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            var buffer = new byte[AllocatedBuffer];
            var readedBytes = 0;
            using (var mStream = new MemoryStream())
            {
                while ((readedBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    mStream.Write(buffer, 0, readedBytes);
                }
                return mStream.ToArray();
            }
        }
    }
}
