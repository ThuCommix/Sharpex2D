
namespace SharpexGL.Framework.Scripting
{
    public abstract class ScriptInterpreter
    {
        /// <summary>
        /// Initializes a new ScriptInterpreter class.
        /// </summary>
        /// <param name="scriptProvider">The ScriptProvider.</param>
        protected ScriptInterpreter(ScriptProvider scriptProvider)
        {
            ScriptProvider = scriptProvider;
        }

        protected ScriptProvider ScriptProvider { get; private set; }

        /// <summary>
        /// Evaluate the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        public virtual void Evaluate(IScript script)
        {
        }
    }
}
