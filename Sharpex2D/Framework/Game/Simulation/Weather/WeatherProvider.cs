using System;
using Sharpex2D.Framework.Common.Randomization;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Game.Timing;

namespace Sharpex2D.Framework.Game.Simulation.Weather
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public class WeatherProvider : IUpdateable, IWeather, IComponent
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("B0C7C972-2C6F-4A8F-8BC6-9ABFADE5AF90"); }
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        ///     Processes a Game tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Tick(GameTime gameTime)
        {
            //Only update after 5 seconds to save cpu.
            _passedTime += gameTime.ElapsedGameTime;
            if (_passedTime > 5000)
            {
                UpdateWeather();
                _passedTime = 0f;
            }
        }

        /// <summary>
        ///     Constructs the Component
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<IGameLoop>().Subscribe(this);
        }

        #endregion

        #region IWeather Implementation

        /// <summary>
        ///     Gets the current weather.
        /// </summary>
        public WeatherType CurrentWeather { get; private set; }

        #endregion

        private readonly GameRandom _gRandom;
        private readonly Time.GameTime _gameTime;
        private DateTime _lastDateTime;
        private float _passedTime;

        /// <summary>
        ///     Initializes a new WeatherProvider class.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        /// <param name="initialWeather">The initial Weather.</param>
        public WeatherProvider(Time.GameTime gameTime, WeatherType initialWeather = WeatherType.Dry)
        {
            _gameTime = gameTime;
            _gRandom = new GameRandom();
            _lastDateTime = _gameTime.DayTime;
            CurrentWeather = initialWeather;
            SGL.Components.AddComponent(this);
        }

        /// <summary>
        ///     Updates the weather.
        /// </summary>
        private void UpdateWeather()
        {
            TimeSpan timeDifference = _gameTime.DayTime - _lastDateTime;
            //Update th weather every 2 game hours
            if (timeDifference >= new TimeSpan(2, 0, 0))
            {
                //The chance to change the weather is 33%
                if (_gRandom.NextBoolean(0.33))
                {
                    //Check the last weather type and make possible decisions
                    switch (CurrentWeather)
                    {
                        case WeatherType.Dry:
                            //After dry the weather could be cloudy
                            CurrentWeather = WeatherType.Cloudy;
                            break;
                        case WeatherType.Cloudy:
                            //After cloudy the weather could be wet or foggy or dry 33%
                            if (_gRandom.NextBoolean(0.33))
                            {
                                CurrentWeather = WeatherType.Wet;
                                break;
                            }
                            if (_gRandom.NextBoolean(0.33))
                            {
                                CurrentWeather = WeatherType.Foggy;
                                break;
                            }

                            CurrentWeather = WeatherType.Dry;

                            break;
                        case WeatherType.Foggy:
                            //After foggy the weather could be wet or cloudy 50%
                            CurrentWeather = _gRandom.NextBoolean(0.5) ? WeatherType.Cloudy : WeatherType.Wet;
                            break;
                        case WeatherType.Wet:
                            CurrentWeather = WeatherType.Cloudy;
                            break;
                    }

                    //update lastDateTime
                    _lastDateTime = _gameTime.DayTime;
                }
            }
        }
    }
}