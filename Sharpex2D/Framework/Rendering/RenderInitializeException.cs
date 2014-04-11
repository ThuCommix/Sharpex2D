using System;

namespace Sharpex2D.Framework.Rendering
{
    public class RenderInitializeException : Exception
    {
        public RenderInitializeException(string message)
        {
            _message = message;
        }

        public RenderInitializeException()
        {
            
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
