using System;

namespace Sharpex2D.Framework.Game.Services.Availability
{
    public class NetworkNotAvailableException : Exception
    {
        /// <summary>
        /// Initializes a new NetworkNotAvailableException class.
        /// </summary>
        public NetworkNotAvailableException()
        {
            
        }
        /// <summary>
        /// Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public NetworkNotAvailableException(string message)
        {
            _message = message;
        }

        private readonly string _message = "";

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
