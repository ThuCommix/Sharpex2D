// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Security.Cryptography;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ContentVerifier
    {
        /// <summary>
        ///     Initializes a new ContentVerifier class.
        /// </summary>
        internal ContentVerifier()
        {
        }

        /// <summary>
        ///     Checks whether the content file is modified.
        /// </summary>
        /// <param name="contentPath">The ContentPath.</param>
        /// <param name="expectedSha256">The ExpectedSha256Hash.</param>
        /// <returns>True if the file was NOT modified.</returns>
        public bool Verify(string contentPath, string expectedSha256)
        {
            return expectedSha256 == Sha256(contentPath);
        }

        /// <summary>
        ///     Checks whether the content file is modified.
        /// </summary>
        /// <param name="fileStream">The FileStream.</param>
        /// <param name="expectedSha256">The ExpectedSha256Hash.</param>
        /// <returns>True if the file was NOT modified.</returns>
        public bool Verify(Stream fileStream, string expectedSha256)
        {
            return expectedSha256 == Sha256(fileStream);
        }

        /// <summary>
        ///     Gets the Sha256-Hash of a file.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>String</returns>
        private static string Sha256(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        /// <summary>
        ///     Gets the Sha256-Hash of a file.
        /// </summary>
        /// <param name="fileStream">The FileStream.</param>
        /// <returns>String</returns>
        private static string Sha256(Stream fileStream)
        {
            using (fileStream)
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(fileStream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
    }
}