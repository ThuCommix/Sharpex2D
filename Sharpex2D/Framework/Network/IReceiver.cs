namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IReceiver
    {
        /// <summary>
        ///     Receives a package.
        /// </summary>
        void BeginReceive();
    }
}