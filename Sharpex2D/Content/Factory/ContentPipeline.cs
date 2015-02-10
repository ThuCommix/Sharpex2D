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
using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Content.Factory
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ContentPipeline
    {
        private readonly List<IAttachableFactory> _factories;
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new ContentPipeline class.
        /// </summary>
        public ContentPipeline()
        {
            _factories = new List<IAttachableFactory>();
            _logger = LogManager.GetClassLogger();
        }

        /// <summary>
        /// Attaches a factory to the content pipeline.
        /// </summary>
        /// <param name="factory">The IAttachableFactory.</param>
        public void Attach(IAttachableFactory factory)
        {
            if (!_factories.Contains(factory))
            {
                _factories.Add(factory);
#if DEBUG
                _logger.Info("Factory attached. Export data: {0}.", factory.Type.Name);
#endif
            }
        }

        /// <summary>
        /// Detaches a factory from the content pipeline.
        /// </summary>
        /// <param name="factory">IAttachableFactory.</param>
        public void Detach(IAttachableFactory factory)
        {
            if (_factories.Contains(factory))
            {
                _factories.Remove(factory);
#if DEBUG
                _logger.Info("Factory detached. Export data: {0}.", factory.Type.Name);
#endif
            }
#if DEBUG
            else
            {
                _logger.Info("Tried to remove an unattached factory.");
            }
#endif
        }

        /// <summary>
        /// Processes the requested datatype through the content pipeline.
        /// </summary>
        /// <typeparam name="T">The requested datatype.</typeparam>
        /// <param name="path">The path.</param>
        /// <returns>T.</returns>
        public T ProcessDatatype<T>(string path) where T : IContent
        {
            try
            {
                return (T) SelectFactory<T>().CreateContent(path);
            }
            catch (Exception unknown)
            {
                throw new ContentLoadException("An error occured while processing the requested datatype.", unknown);
            }
        }

        /// <summary>
        /// Processes the requested datatype through the content pipeline.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="type">The type.</param>
        /// <returns>IContent.</returns>
        public IContent ProcessDatatype(string path, Type type)
        {
            try
            {
                return SelectFactory(type).CreateContent(path);
            }
            catch (Exception unknown)
            {
                throw new ContentLoadException("An error occured while processing the requested datatype.", unknown);
            }
        }

        /// <summary>
        /// Selects IAttachableFactory which matches the requested datatype.
        /// </summary>
        /// <typeparam name="T">The requested datatype.</typeparam>
        /// <returns>IAttachableFactory.</returns>
        private IAttachableFactory SelectFactory<T>() where T : IContent
        {
            foreach (
                var factory in
                    _factories.Where(factory => factory.Type == typeof (T)))
            {
                return factory;
            }

            throw new ContentLoadException(string.Format("No factory attached for {0}.", typeof (T).Name));
        }

        /// <summary>
        /// Selects IAttachableFactory which matches the requested datatype.
        /// </summary>
        /// <param name="type">The requested datatype.</param>
        /// <returns>IAttachableFactory.</returns>
        private IAttachableFactory SelectFactory(Type type)
        {
            foreach (var factory in _factories.Where(factory => factory.Type == type))
            {
                return factory;
            }

            throw new ContentLoadException(string.Format("No factory attached for {0}.", type.Name));
        }
    }
}