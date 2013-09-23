using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Localization
{
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
    }
}
