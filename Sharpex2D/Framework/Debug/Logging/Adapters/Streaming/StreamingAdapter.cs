using System.IO;
using System.Text;

namespace Sharpex2D.Framework.Debug.Logging.Adapters.Streaming
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class StreamingAdapter : StreamingAdapterBase
    {

        /// <summary>
        /// Initializes a new StreamingAdapter class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        public StreamingAdapter(Stream stream) : this(stream, Encoding.UTF8)
        {
            
        }

        /// <summary>
        /// Initializes a new StreamingAdapter class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="encoding">The Encoding.</param>
        public StreamingAdapter(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="writer">The StreamWriter.</param>
        public override void Write(string message, StreamWriter writer)
        {
            writer.WriteLine(message);
        }
    }
}
