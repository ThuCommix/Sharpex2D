using System.IO;
using System.Reflection;

namespace SharpexGL.Framework.Plugins
{
    public class PluginImplementer
    {
        /// <summary>
        /// Loads a assembly into SGL.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="path">The Path.</param>
        /// <returns>T</returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given resource could not be located.");
            }
            var assembly = Assembly.LoadFrom(path);
            if (assembly.GetType() == typeof(T))
            {
                return (T)((object)assembly);
            }
            throw new PluginException("The resource is not a valid " + typeof(T).FullName + ".");
        }
    }
}
