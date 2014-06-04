using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Plugin
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PluginActivator : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("520649BC-EAF2-48D4-9938-C240502E8681"); }
        }

        #endregion

        /// <summary>
        ///     Loads a plugin into SGL.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="path">The Path.</param>
        /// <returns>T</returns>
        public static T CreateInstance<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given resource could not be located.");
            }
            Assembly assembly = Assembly.LoadFrom(path);

            if (assembly.GetTypes().Any(type => type == typeof (T)))
            {
                return (T) ((object) assembly);
            }

            throw new PluginException("The resource is not a valid " + typeof (T).FullName + ".");
        }
    }
}