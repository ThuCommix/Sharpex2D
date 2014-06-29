using System;
using System.IO;
using System.Text;

namespace Sharpex2D.Framework.Debug.Logging.Adapters.Streaming
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class StreamingAdapterBase : IAdapter
    {
        /// <summary>
        /// Gets the Encoding.
        /// </summary>
        public Encoding Encoding { private set; get; }

        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new StreamingAdapterBase class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="encoding">The Encoding.</param>
        protected StreamingAdapterBase(Stream stream, Encoding encoding)
        {
            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("The stream is marked as readonly.");
            }

            _writer = new StreamWriter(stream, encoding) {AutoFlush = true};
            Encoding = encoding;
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="writer">The StreamWriter.</param>
        public abstract void Write(string message, StreamWriter writer);

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        void IAdapter.Write(string message)
        {
            Write(message, _writer);
        }
    }
}
