using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Scripting.Events
{
    public class ScriptRunningEvent : IEvent
    {
        /// <summary>
        /// Initializes a new ScriptRunningEvent class.
        /// </summary>
        /// <param name="name"></param>
        public ScriptRunningEvent(string name)
        {
            ScriptName = name;
        }

        /// <summary>
        /// Gets the name of the running scrip.t
        /// </summary>
        public string ScriptName { get; private set; }
    }
}
