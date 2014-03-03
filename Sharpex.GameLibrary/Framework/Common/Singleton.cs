
namespace SharpexGL.Framework.Common
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly T StaticInstance = new T();

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static T Instance
        {
            get { return StaticInstance; }
        }
    }
}
