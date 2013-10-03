using System;
using SharpexGL.Framework.Common.Randomization;
using SharpexGL.Framework.Game.Simulation.Time;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Game.Simulation.Weather
{
    public class WeatherProvider : IGameHandler, IWeather
    {
        #region IGameHandler Implementation
        /// <summary>
        /// Constructs the Component
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<GameLoop>().Subscribe(this);
        }
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            //Only update after 5 seconds to save cpu.
            _passedTime += elapsed;
            if (_passedTime > 5000)
            {
                UpdateWeather();
                _passedTime = 0f;
            }
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IGraphicRenderer renderer, float elapsed)
        {
           
        }
        #endregion

        #region IWeather Implementation

        /// <summary>
        /// Gets the current weather.
        /// </summary>
        public WeatherType CurrentWeather { get; private set; }

        #endregion

        private readonly GameTime _gameTime;
        private DateTime _lastDateTime;
        private readonly GameRandom _gRandom;
        private float _passedTime;

        /// <summary>
        /// Initializes a new WeatherProvider class.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        /// <param name="initialWeather">The initial Weather.</param>
        public WeatherProvider(GameTime gameTime, WeatherType initialWeather = WeatherType.Dry)
        {
            _gameTime = gameTime;
            _gRandom = new GameRandom();
            _lastDateTime = _gameTime.DayTime;
            CurrentWeather = initialWeather;
            SGL.Components.AddComponent(this);
        }

        /// <summary>
        /// Updates the weather.
        /// </summary>
        private void UpdateWeather()
        {
            var timeDifference = _gameTime.DayTime - _lastDateTime;
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
                            if (_gRandom.NextBoolean(0.33))
                            {
                                CurrentWeather = WeatherType.Dry;
                            }
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
