namespace Sharpex2D.Framework.Game.Simulation.Weather
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IWeather
    {
        /// <summary>
        ///     Gets the current weather.
        /// </summary>
        WeatherType CurrentWeather { get; }
    }
}