using System;
using System.Collections.Generic;
using System.Linq;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Events
{
    public class EventManager : IComponent, IObservable<IEvent>
    {
        public EventManager()
        {
            _observers = new List<dynamic>();
        }

        private List<dynamic> _observers;

        /// <summary>
        /// Subscribes to a special event.
        /// </summary>
        /// <param name="observer">The Observer.</param>
        /// <returns>Unsubscriber</returns>
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        /// <summary>
        /// Unsubscribes from a special event.
        /// </summary>
        /// <param name="observer">The Observer.</param>
        public void Unsubscribe(IObserver<IEvent> observer)
        {
            if (observer != null && _observers.Contains(observer))
                _observers.Remove(observer);
        }

        /// <summary>
        /// Gets the observer of a special event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<dynamic> GetObservers<T>() where T : IEvent
        {
            var type = typeof (T);
            return _observers.Where(observer => observer == type).ToList();
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
    }
}
