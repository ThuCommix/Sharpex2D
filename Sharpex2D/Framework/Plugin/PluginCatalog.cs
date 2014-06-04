using System;
using System.IO;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Plugin
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PluginCatalog<T>
    {
        /// <summary>
        ///     Initializes a new PluginCatalog class.
        /// </summary>
        public PluginCatalog() : this(Environment.CurrentDirectory)
        {
        }

        /// <summary>
        ///     Initializes a new PluginCatalog class.
        /// </summary>
        /// <param name="workingDirectory">The WorkingDirectory.</param>
        public PluginCatalog(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        /// <summary>
        ///     Sets or gets the WorkingDirectory.
        /// </summary>
        public string WorkingDirectory { set; get; }

        /// <summary>
        ///     Compose the plugins.
        /// </summary>
        /// <returns>PluginContainer with type T.</returns>
        public PluginContainer<T> Compose()
        {
            var resultfiles = Directory.GetFiles(WorkingDirectory, "*.dll");

            var pluginContainer = new PluginContainer<T>
            {
                Guid = Guid.NewGuid(),
                Description = "PluginContainer with type " + typeof (T).Name + " composed by PluginCatalog."
            };

            foreach (string file in resultfiles)
            {
                try
                {
                    pluginContainer.Add(PluginActivator.CreateInstance<T>(file));
                }
                catch (PluginException ex)
                {
                    Log.Next("PluginCatalog: " + ex.Message, LogLevel.Warning, LogMode.StandardOut);
                }
            }

            return pluginContainer;
        }
    }
}