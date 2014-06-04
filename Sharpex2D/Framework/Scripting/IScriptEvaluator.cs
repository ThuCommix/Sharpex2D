namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IScriptEvaluator
    {
        /// <summary>
        ///     Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        void Evaluate(IScript script, params object[] objects);
    }
}