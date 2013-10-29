using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Game.Services
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
