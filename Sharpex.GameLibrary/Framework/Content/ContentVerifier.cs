using System;
using System.IO;
using System.Security.Cryptography;

namespace SharpexGL.Framework.Content
{
    public class ContentVerifier
    {
        /// <summary>
        /// Initializes a new ContentVerifier class.
        /// </summary>
        internal ContentVerifier()
        {
            
        }

        /// <summary>
        /// Checks whether the content file is modified.
        /// </summary>
        /// <param name="contentPath">The ContentPath.</param>
        /// <param name="expectedSha256">The ExpectedSha256Hash.</param>
        /// <returns>True if the file was NOT modified.</returns>
        public bool Verify(string contentPath, string expectedSha256)
        {
            return expectedSha256 == Sha256(contentPath);
        }

        /// <summary>
        /// Gets the Sha256-Hash of a file.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>String</returns>
        private static string Sha256(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                var checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
    }
}
