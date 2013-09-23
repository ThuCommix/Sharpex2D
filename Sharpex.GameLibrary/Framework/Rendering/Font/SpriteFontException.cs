using System;

namespace SharpexGL.Framework.Rendering.Font
{
    public class SpriteFontException : Exception
    {
        private string _message = "";
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        /// <summary>
        /// Initializes a new SpriteFontException.
        /// </summary>
        public SpriteFontException()
        {
        }
        /// <summary>
        /// Initializes a new SpriteFontException.
        /// </summary>
        /// <param name="message">The Message.</param>
        public SpriteFontException(string message)
        {
            _message = message;
        }
    }
}
