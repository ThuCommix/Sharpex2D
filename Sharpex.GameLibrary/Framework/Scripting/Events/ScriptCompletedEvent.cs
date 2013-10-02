using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Scripting.Events
{
    public class ScriptCompletedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new ScriptCompletedEvent class.
        /// </summary>
        /// <param name="name"></param>
        public ScriptCompletedEvent(string name)
        {
            ScriptName = name;
        }

        /// <summary>
        /// Gets the name of the running scrip.t
        /// </summary>
        public string ScriptName { get; private set; }
    }
}
