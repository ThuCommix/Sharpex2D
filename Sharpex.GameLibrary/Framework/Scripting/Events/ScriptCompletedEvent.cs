using System;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Scripting.Events
{
    public class ScriptCompletedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new ScriptCompletedEvent class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        public ScriptCompletedEvent(Guid guid)
        {
            ScriptGuid = guid;
        }

        /// <summary>
        /// Gets the name of the running scrip.t
        /// </summary>
        public Guid ScriptGuid { get; private set; }
    }
}
