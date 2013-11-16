using System;

namespace SharpexGL.Framework.Exceptions
{
    public class BaseException : Exception
    {
        /// <summary>
        /// Initializes a new BaseException class.
        /// </summary>
        public BaseException()
        {
            
        }
        /// <summary>
        /// Initializes a new BaseException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public BaseException(string message)
        {
            _message = message;
        }
        /// <summary>
        /// Initializes a new BaseException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="innerException">The InnerException</param>
        public BaseException(string message, BaseException innerException)
        {
            _message = message;
            InnerException = innerException;
        }

        private readonly string _message = "";

        public override string Message
        {
            get
            {
                return _message;
            }
        }
        /// <summary>
        /// Gets the InnerException.
        /// </summary>
        public new BaseException InnerException { private set; get; } 
    }
}
