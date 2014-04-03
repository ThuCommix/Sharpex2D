using SharpexGL.Framework.Events;
using SharpexGL.Framework.Input.XInput;

namespace SharpexGL.Framework.Input.Events
{
    public class XboxControllerStateChangedEventArgs : IEvent
    {
        /// <summary>
        /// Sets or gets the CurrentInputState.
        /// </summary>
        public XInputState CurrentInputState { get; set; }
        /// <summary>
        /// Sets or gets the PreviousInputState.
        /// </summary>
        public XInputState PreviousInputState { get; set; }
    }
}
