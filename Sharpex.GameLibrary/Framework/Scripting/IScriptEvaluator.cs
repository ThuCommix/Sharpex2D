
namespace SharpexGL.Framework.Scripting
{
    public interface IScriptEvaluator
    {
        /// <summary>
        /// Evaluate the script content.
        /// </summary>
        /// <param name="scriptContent">The ScriptContent.</param>
        void Evaluate(string scriptContent);
    }
}
