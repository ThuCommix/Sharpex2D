using System.IO;
using System.Text;
using System.Security.Cryptography;
using SharpexGL.Framework.Implementation;

namespace SharpexGL.Framework.Common.Cryptography
{
    public class HashProvider : IImplementation
    {
        /// <summary>
        /// Initializes a new HashProvider class.
        /// </summary>
        public HashProvider()
        {
            
        }
        /// <summary>
        /// Computes a hash from the given ByteArray.
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="data">The Data.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, byte[] data)
        {
            var result = hashAlgorithm.ComputeHash(data);
            var sb = new StringBuilder();
            foreach (var t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Computes a hash from the given String.
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="data">The Data.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            var result = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(data));
            var sb = new StringBuilder();
            foreach (var t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Computes a hash from the given Stream
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="stream">The Stream.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, Stream stream)
        {
            var result = hashAlgorithm.ComputeHash(stream);
            var sb = new StringBuilder();
            foreach (var t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
