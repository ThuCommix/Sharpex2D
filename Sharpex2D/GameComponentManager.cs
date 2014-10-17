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

using System.Collections;
using System.Collections.Generic;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GameComponentManager : IEnumerable<IGameComponent>
    {
        private readonly GameComponentComparer _comparer;
        private readonly List<IGameComponent> _components;

        /// <summary>
        /// Initializes a new GameComponentCollection class.
        /// </summary>
        public GameComponentManager()
        {
            _components = new List<IGameComponent>();
            _comparer = new GameComponentComparer();
        }

        /// <summary>
        /// Gets the Components.
        /// </summary>
        public IGameComponent[] Components
        {
            get { return _components.ToArray(); }
        }

        /// <summary>
        /// Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator of IGameComponent.</returns>
        public IEnumerator<IGameComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        /// <summary>
        /// Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator of IGameComponent.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a new IGameComponent.
        /// </summary>
        /// <param name="gameComponent">The IGameComponent.</param>
        public void Add(IGameComponent gameComponent)
        {
            if (!_components.Contains(gameComponent))
            {
                _components.Add(gameComponent);
                _components.Sort(_comparer);
            }
        }

        /// <summary>
        /// Removes a IGameComponent.
        /// </summary>
        /// <param name="gameComponent">The IGameComponent.</param>
        public void Remove(IGameComponent gameComponent)
        {
            if (_components.Contains(gameComponent))
            {
                _components.Remove(gameComponent);
                _components.Sort(_comparer);
            }
        }

        private class GameComponentComparer : IComparer<IGameComponent>
        {
            /// <summary>
            /// Compares two IGameComponents.
            /// </summary>
            /// <param name="x">The first IGameComponent.</param>
            /// <param name="y">The second IGameComponent.</param>
            /// <returns>Int32.</returns>
            public int Compare(IGameComponent x, IGameComponent y)
            {
                return x.Order.CompareTo(y.Order);
            }
        }
    }
}