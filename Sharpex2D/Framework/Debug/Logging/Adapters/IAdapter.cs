
namespace Sharpex2D.Framework.Debug.Logging.Adapters
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IAdapter
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        void Write(string message);
    }
}
