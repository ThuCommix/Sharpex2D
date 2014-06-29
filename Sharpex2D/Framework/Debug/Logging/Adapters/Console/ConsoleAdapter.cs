
namespace Sharpex2D.Framework.Debug.Logging.Adapters.Console
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ConsoleAdapter : IAdapter
    {
        /// <summary>
        /// Gets the OutputMode.
        /// </summary>
        public OutputMode OutputMode { private set; get; }

        /// <summary>
        /// Initializes a new ConsoleAdapter class.
        /// </summary>
        /// <param name="mode">The OutputMode.</param>
        public ConsoleAdapter(OutputMode mode)
        {
            OutputMode = mode;
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Write(string message)
        {
            if (OutputMode == OutputMode.Error)
            {
                System.Console.Error.WriteLine(message);
            }
            else
            {
                System.Console.WriteLine(message);
            }
        }
    }
}
