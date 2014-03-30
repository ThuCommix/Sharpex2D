
namespace SharpexGL.Framework.Scripting
{
    public interface IScriptEvaluator
    {
        /// <summary>
        /// Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        void Evaluate(IScript script, params object[] objects);
    }
}
