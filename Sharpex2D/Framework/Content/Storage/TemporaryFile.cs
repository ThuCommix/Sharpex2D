using System;
using System.IO;

namespace Sharpex2D.Framework.Content.Storage
{
    public class TemporaryFile : IDisposable
    {

        private bool _isDisposed;

        /// <summary>
        /// Gets the Path.
        /// </summary>
        public string Path { private set; get; }

        /// <summary>
        /// Gets the Stream.
        /// </summary>
        public Stream Stream { private set; get; }

        /// <summary>
        /// Initializes a new TemporaryFile class.
        /// </summary>
        /// <param name="path">The Path.</param>
        internal TemporaryFile(string path)
        {
            Path = path;
            Stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <remarks>Use Close() instead of dispose to terminate the temporary file.</remarks>
        public void Dispose()
        {
            if (_isDisposed) return;

            Stream.Close();
            Stream.Dispose();

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            _isDisposed = true;
        }
        /// <summary>
        /// Closes the temporary file.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>TemporaryFile.</returns>
        public static TemporaryFile Open(string path)
        {
            return new TemporaryFile(path);
        }
    }
}
