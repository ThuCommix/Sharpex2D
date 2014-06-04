namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IScriptEntry
    {
        /// <summary>
        ///     The Main method of the script.
        /// </summary>
        /// <param name="objects">The Objects.</param>
        void Main(params object[] objects);
    }
}