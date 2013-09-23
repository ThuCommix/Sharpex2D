using System;

namespace SharpexGL.Framework.Localization
{
    [Serializable]
    public class LanguageSerializationException : Exception
    {
        /// <summary>
        /// Creates a new LanguageSerializationException.
        /// </summary>
        public LanguageSerializationException()
        {

        }

        /// <summary>
        /// Creates a new LanguageSerializationException.
        /// </summary>
        /// <param name="message">The Message.</param>
        public LanguageSerializationException(string message)
        {
            _message = message;
        }

        private string _message = "";

        public override string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// Initializes a new LanguageSerializationException class.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The SerializationContext.</param>
        protected LanguageSerializationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
