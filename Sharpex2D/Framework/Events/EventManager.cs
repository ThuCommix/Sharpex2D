// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class EventManager : IComponent, IObservable<IEvent>
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CF0AEB96-141D-4D2B-8C28-0A64BFDFE277"); }
        }

        #endregion

        private readonly LinkedList<IObserver<IEvent>> _observers;

        public EventManager()
        {
            _observers = new LinkedList<IObserver<IEvent>>();
        }

        /// <summary>
        ///     Subscribes to a special event.
        /// </summary>
        /// <param name="observer">The Observer.</param>
        /// <returns>Unsubscriber</returns>
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");
            return new Unsubscriber(_observers.AddLast(observer));
        }

        /// <summary>
        ///     Gets the observer of a special event.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IObserver<IEvent>> GetObservers<T>()
        {
            return _observers.Where(observer => observer is T);
        }

        /// <summary>
        ///     Publishs a Event to all observers.
        /// </summary>
        /// <typeparam name="TEvent">The Event.</typeparam>
        /// <param name="e">The Event.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            foreach (var observer in GetObservers<TEvent>().Cast<IObserver<TEvent>>())
            {
                observer.OnNext(e);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly LinkedListNode<IObserver<IEvent>> _observer;

            /// <summary>
            ///     Initializes a new Unsubscriber instance.
            /// </summary>
            /// <param name="observer">The Observer.</param>
            public Unsubscriber(LinkedListNode<IObserver<IEvent>> observer)
            {
                _observer = observer;
            }

            /// <summary>
            ///     Removes the Observer.
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