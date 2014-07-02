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
using Sharpex2D.Framework.Common.Randomization;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Game.Timing;

namespace Sharpex2D.Framework.Game.Simulation.Weather
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
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