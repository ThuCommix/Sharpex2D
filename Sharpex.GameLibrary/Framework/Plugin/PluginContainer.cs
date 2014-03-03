using System;
using SharpexGL.Framework.Collections;

namespace SharpexGL.Framework.Plugin
{
    public class PluginContainer<T> : BufferedCollection<T>
    {
        /// <summary>
        /// Sets or gets the Description.
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// Sets or gets the Guid.
        /// </summary>
        public Guid Guid { set; get; }
        /// <summary>
        /// Initializes a new PluginContainer class.
        /// </summary>
        public PluginContainer()
        {
            Guid = Guid.NewGuid();
        }
    }
}
