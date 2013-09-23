using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Events
{
    public class Unsubscriber : IDisposable
    {
        private List<dynamic> _observers;
        private IObserver<IEvent> _observer;
        /// <summary>
        /// Initializes a new Unsubscriber instance.
        /// </summary>
        /// <param name="observers">The Observers.</param>
        /// <param name="observer">The Observer.</param>
        public Unsubscriber(List<dynamic> observers, IObserver<IEvent> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        /// <summary>
        /// Removes the Observer.
        /// </summary>
        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
