using System;
using Sharpex2D.Framework.Collections;

namespace Sharpex2D.Framework.Plugin
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PluginContainer<T> : BufferedCollection<T>
    {
        /// <summary>
        ///     Initializes a new PluginContainer class.
        /// </summary>
        public PluginContainer()
        {
            Guid = Guid.NewGuid();
        }

        /// <summary>
        ///     Sets or gets the Description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        ///     Sets or gets the Guid.
        /// </summary>
        public Guid Guid { set; get; }
    }
}