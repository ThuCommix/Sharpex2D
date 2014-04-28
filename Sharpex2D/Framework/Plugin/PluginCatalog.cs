using System;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Plugin
{
    public class PluginCatalog<T>
    {
        /// <summary>
        /// Sets or gets the WorkingDirectory.
        /// </summary>
        public string WorkingDirectory { set; get; }

        /// <summary>
        /// Initializes a new PluginCatalog class.
        /// </summary>
        public PluginCatalog() : this(Environment.CurrentDirectory)
        {
            
        }
        /// <summary>
        /// Initializes a new PluginCatalog class.
        /// </summary>
        /// <param name="workingDirectory">The WorkingDirectory.</param>
        public PluginCatalog(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }
        /// <summary>
        /// Compose the plugins.
        /// </summary>
        /// <returns>PluginContainer with type T.</returns>
        public PluginContainer<T> Compose()
        {
            var resultfiles = SGL.Components.Get<ContentManager>().FileSystem.GetFiles(WorkingDirectory);

            var pluginContainer = new PluginContainer<T>
            {
                Guid = Guid.NewGuid(),
                Description = "PluginContainer with type " + typeof (T).Name + " composed by PluginCatalog."
            };

            foreach (var file in resultfiles)
            {
                try
                {
                    pluginContainer.Add(PluginLoader.Load<T>(file));
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
