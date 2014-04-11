using System;

namespace Sharpex2D.Framework.Game.Services.Availability
{
    public class GameUpdateRequiredException : Exception
    {
        /// <summary>
        /// Initializes a new GameUpdateRequiredException class.
        /// </summary>
        public GameUpdateRequiredException()
        {
            _message = "The game needs to be updated.";
        }
        /// <summary>
        /// Initializes a new GameUpdateRequiredException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public GameUpdateRequiredException(string message)
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
