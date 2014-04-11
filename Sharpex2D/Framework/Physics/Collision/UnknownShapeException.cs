using System;

namespace Sharpex2D.Framework.Physics.Collision
{
    public class UnknownShapeException : Exception
    {
        /// <summary>
        /// Initializes a new UnknownShapeException class.
        /// </summary>
        public UnknownShapeException()
        {
            
        }
        /// <summary>
        /// Initializes a new UnknownShapeException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public UnknownShapeException(string message)
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
