using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sharpex2D.Framework.Common.Cryptography
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class HashProvider
    {
        /// <summary>
        ///     Computes a hash from the given ByteArray.
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="data">The Data.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, byte[] data)
        {
            byte[] result = hashAlgorithm.ComputeHash(data);
            var sb = new StringBuilder();
            foreach (byte t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Computes a hash from the given String.
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="data">The Data.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            byte[] result = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(data));
            var sb = new StringBuilder();
            foreach (byte t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Computes a hash from the given Stream
        /// </summary>
        /// <param name="hashAlgorithm">The Algorithm.</param>
        /// <param name="stream">The Stream.</param>
        /// <returns>String</returns>
        public string ComputeHash(HashAlgorithm hashAlgorithm, Stream stream)
        {
            byte[] result = hashAlgorithm.ComputeHash(stream);
            var sb = new StringBuilder();
            foreach (byte t in result)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}