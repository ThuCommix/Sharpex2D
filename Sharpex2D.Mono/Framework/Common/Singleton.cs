namespace Sharpex2D.Framework.Common
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Singleton<T> where T : class, new()
    {
        private static readonly T StaticInstance = new T();

        /// <summary>
        ///     Gets the Instance.
        /// </summary>
        public static T Instance
        {
            get { return StaticInstance; }
        }
    }
}