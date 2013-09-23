using System;

namespace SharpexGL.Framework.Plugins
{
    [Serializable]
    public class PluginException : Exception
    {
        private readonly string _message = "";
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        /// <summary>
        /// Initializes a new PluginException.
        /// </summary>
        public PluginException()
        {
        }
        /// <summary>
        /// Initializes a new PluginException.
        /// </summary>
        /// <param name="message">The Message.</param>
        public PluginException(string message)
        {
            _message = message;
        }

                /// <summary>
        /// Initializes a new PluginException class.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The SerializationContext.</param>
        protected PluginException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
