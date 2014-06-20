using System.Collections;
using System.Collections.Generic;

namespace Sharpex2D.Framework.Game
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameComponentManager : IEnumerable<IGameComponent>
    {
        private readonly GameComponentComparer _comparer;
        private readonly List<IGameComponent> _components;

        /// <summary>
        ///     Initializes a new GameComponentCollection class.
        /// </summary>
        public GameComponentManager()
        {
            _components = new List<IGameComponent>();
            _comparer = new GameComponentComparer();
        }

        /// <summary>
        ///     Gets the Components.
        /// </summary>
        public IGameComponent[] Components
        {
            get { return _components.ToArray(); }
        }

        /// <summary>
        ///     Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator of IGameComponent.</returns>
        public IEnumerator<IGameComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        /// <summary>
        ///     Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator of IGameComponent.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds a new IGameComponent.
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
        ///     Removes a IGameComponent.
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
            ///     Compares two IGameComponents.
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