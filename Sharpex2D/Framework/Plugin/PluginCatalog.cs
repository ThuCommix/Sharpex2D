// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Plugin
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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
            string[] resultfiles = Directory.GetFiles(WorkingDirectory, "*.dll");

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
                    LogManager.GetClassLogger().Warn(ex.Message);
                }
            }

            return pluginContainer;
        }
    }
}