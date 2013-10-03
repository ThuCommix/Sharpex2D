
namespace SharpexGL.Framework.Scripting
{
    public abstract class ScriptInterpreter
    {
        /// <summary>
        /// Initializes a new ScriptInterpreter class.
        /// </summary>
        protected ScriptInterpreter()
        {

        }
        /// <summary>
        /// Sets or gets the ScriptProvider.
        /// </summary>
        public ScriptProvider ScriptProvider { get; set; }

        /// <summary>
        /// Evaluate the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        public virtual void Evaluate(IScript script)
        {
        }
    }
}
