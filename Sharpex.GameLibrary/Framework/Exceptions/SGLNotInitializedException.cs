
namespace SharpexGL.Framework.Exceptions
{
    public class SGLNotInitializedException : BaseException
    {
        /// <summary>
        /// Initializes a new SGLNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public SGLNotInitializedException(string message) : base(message)
        {
            
        }
        /// <summary>
        /// Initializes a new SGLNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="innerException">The InnerException.</param>
        public SGLNotInitializedException(string message, BaseException innerException) : base(message, innerException)
        {
            
        }
    }
}
