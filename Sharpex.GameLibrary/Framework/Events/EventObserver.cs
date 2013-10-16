using System;

namespace SharpexGL.Framework.Events
{
    public class EventObserver<T> : IObserver<IEvent> where T : class
    {
        private readonly Action<T> _action;
        /// <summary>
        /// Tracks the event to it's delegate.
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(IEvent value)
        {
            if (_action != null)
            {
                _action((T)value);
            }
        }
        /// <summary>
        /// On Error.
        /// </summary>
        /// <param name="error">The Error.</param>
        public void OnError(Exception error)
        {
        
        }
        /// <summary>
        /// On Completed.
        /// </summary>
        public void OnCompleted()
        {
          
        }
        /// <summary>
        /// Creates a new EventObserver.
        /// </summary>
        /// <param name="action">The Delegate.</param>
        public EventObserver(Action<T> action)
        {
            _action = action;
        }
    }
}
