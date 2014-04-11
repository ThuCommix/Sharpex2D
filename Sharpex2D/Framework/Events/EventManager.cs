using System;
using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Events
{
    public class EventManager : IComponent, IObservable<IEvent>
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CF0AEB96-141D-4D2B-8C28-0A64BFDFE277"); }
        }

        #endregion

        public EventManager()
        {
            _observers = new LinkedList<IObserver<IEvent>>();
        }

        private readonly LinkedList<IObserver<IEvent>> _observers;

        /// <summary>
        /// Subscribes to a special event.
        /// </summary>
        /// <param name="observer">The Observer.</param>
        /// <returns>Unsubscriber</returns>
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");
            return new Unsubscriber(_observers.AddLast(observer));
        }

        /// <summary>
        /// Gets the observer of a special event.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IObserver<IEvent>> GetObservers<T>()
        {
            return _observers.Where(observer => observer is T);
        }

        /// <summary>
        /// Publishs a Event to all observers.
        /// </summary>
        /// <typeparam name="TEvent">The Event.</typeparam>
        /// <param name="e">The Event.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            foreach (IObserver<TEvent> observer in GetObservers<TEvent>())
            {
                observer.OnNext(e);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly LinkedListNode<IObserver<IEvent>> _observer;
            /// <summary>
            /// Initializes a new Unsubscriber instance.
            /// </summary>
            /// <param name="observer">The Observer.</param>
            public Unsubscriber(LinkedListNode<IObserver<IEvent>> observer)
            {
                _observer = observer;
            }
            /// <summary>
            /// Removes the Observer.
            /// </summary>
            public void Dispose()
            {
                if (_observer != null)
                {
                    _observer.List.Remove(_observer);
                }
            }
        }
    }
}
