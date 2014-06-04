namespace Sharpex2D.Framework.Game.Services
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class LaunchParameter
    {
        /// <summary>
        ///     Initializes a new LaunchParameter class.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <param name="value">The Value.</param>
        public LaunchParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///     Gets the Key.
        /// </summary>
        public string Key { private set; get; }

        /// <summary>
        ///     Gets the Value.
        /// </summary>
        public string Value { private set; get; }
    }
}