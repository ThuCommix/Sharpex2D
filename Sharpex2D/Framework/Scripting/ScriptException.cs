using System;

namespace Sharpex2D.Framework.Scripting
{
    public class ScriptException : Exception
    {
        /// <summary>
        /// Initializes a new ScriptException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public ScriptException(string message)
        {
            _message = message;
        }

        private readonly string _message;

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
