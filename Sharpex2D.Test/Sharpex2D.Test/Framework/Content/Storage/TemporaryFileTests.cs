using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharpex2D.Framework.Content.Storage;

namespace Sharpex2D.Test.Framework.Content.Storage
{
    [TestClass]
    public class TemporaryFileTests
    {
        [TestMethod]
        public void CanWriteReadTemporaryFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Testfile.cs");

            var tempFile = TemporaryFile.Open(path);

            var buffer = Encoding.UTF8.GetBytes("TemporaryFile Test");
            tempFile.Stream.Write(buffer, 0,
                buffer.Length);

            tempFile.Stream.Seek(0, SeekOrigin.Begin);

            var readBuffer = new byte[buffer.Length];

            tempFile.Stream.Read(readBuffer, 0, buffer.Length);

            Assert.AreEqual("TemporaryFile Test", Encoding.UTF8.GetString(readBuffer));

            tempFile.Close();

            Assert.IsFalse(File.Exists(path));
        }
    }
}
