
namespace SharpexGL.Framework.Game.Simulation.Weather
{
    public interface IWeather
    {
        /// <summary>
        /// Gets the current weather.
        /// </summary>
        WeatherType CurrentWeather { get; }
    }
}
