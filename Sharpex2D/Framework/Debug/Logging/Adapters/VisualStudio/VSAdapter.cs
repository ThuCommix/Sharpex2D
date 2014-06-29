
namespace Sharpex2D.Framework.Debug.Logging.Adapters.VisualStudio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class VSAdapter : IAdapter
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Write(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
