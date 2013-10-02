using System;

namespace SharpexGL.Framework.Assemblys
{
    [Serializable]
    public class AssemblyException : Exception
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
        /// Initializes a new AssemblyException.
        /// </summary>
        public AssemblyException()
        {
        }
        /// <summary>
        /// Initializes a new AssemblyException.
        /// </summary>
        /// <param name="message">The Message.</param>
        public AssemblyException(string message)
        {
            _message = message;
        }

                /// <summary>
        /// Initializes a new AssemblyException class.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The SerializationContext.</param>
        protected AssemblyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
