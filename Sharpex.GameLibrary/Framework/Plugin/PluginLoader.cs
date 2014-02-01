using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpexGL.Framework.Plugin
{
    public class PluginLoader
    {
        /// <summary>
        /// Loads a plugin into SGL.
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

            if (assembly.GetTypes().Any(type => type == typeof (T)))
            {
                return (T)((object)assembly);
            }

            throw new PluginException("The resource is not a valid " + typeof(T).FullName + ".");
        }
    }
}
