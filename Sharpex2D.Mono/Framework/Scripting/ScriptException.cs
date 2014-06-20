using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class ScriptException : Exception
    {
        /// <summary>
        ///     Initializes a new ScriptException class.
        /// </summary>
        public ScriptException()
        {
        }

        /// <summary>
        ///     Initializes a new ScriptException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public ScriptException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new ScriptException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public ScriptException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new ScriptException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public ScriptException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}