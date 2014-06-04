using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.Framework.Game.Services
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameServiceContainer
    {
        private readonly List<IGameService> _gameServices;

        /// <summary>
        ///     Initializes a new GameServiceContainer class.
        /// </summary>
        public GameServiceContainer()
        {
            _gameServices = new List<IGameService>();
        }

        /// <summary>
        ///     Adds a new GameService.
        /// </summary>
        /// <param name="service">The GameService.</param>
        public void Add(IGameService service)
        {
            if (!_gameServices.Contains(service))
            {
                _gameServices.Add(service);
            }
        }

        /// <summary>
        ///     Removes a GameService.
        /// </summary>
        /// <param name="service">The GameService.</param>
        public void Remove(IGameService service)
        {
            if (_gameServices.Contains(service))
            {
                _gameServices.Remove(service);
            }
        }

        /// <summary>
        ///     Gets the Service.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>T.</returns>
        public T GetService<T>()
        {
            foreach (IGameService service in _gameServices.Where(service => service.GetType() == typeof (T)))
            {
                return (T) service;
            }

            throw new InvalidOperationException(typeof (T).FullName + " is not an available game service.");
        }
    }
}