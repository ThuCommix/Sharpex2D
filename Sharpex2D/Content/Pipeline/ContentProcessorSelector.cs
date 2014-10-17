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
using Sharpex2D.Content.Pipeline.Processor;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ContentProcessorSelector
    {
        private readonly List<IContentProcessor> _contentProcessors;

        /// <summary>
        /// Initializes a new ContentProcessorSelector class.
        /// </summary>
        public ContentProcessorSelector()
        {
            _contentProcessors = new List<IContentProcessor>();

            Add(new SoundContentProcessor());
            Add(new GamerContentProcessor());
        }

        /// <summary>
        /// Adds a new ContentProcessor.
        /// </summary>
        /// <param name="contentProcessor"></param>
        public void Add(IContentProcessor contentProcessor)
        {
            _contentProcessors.Add(contentProcessor);
            LogManager.GetClassLogger().Engine("Registered ContentProcessor: {0}.", contentProcessor.Type.Name);
        }

        /// <summary>
        /// Selects a ContentProcessor.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>ContentProcessor T.</returns>
        public IContentProcessor Select<T>() where T : IContent
        {
            Type requestedType = typeof (T);
            return Select(requestedType);
        }

        /// <summary>
        /// Selects a ContentProcessor.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>ContentProcessor T.</returns>
        public IContentProcessor Select(Type type)
        {
            foreach (IContentProcessor processor in _contentProcessors)
            {
                if (processor.Type == type)
                {
                    return processor;
                }

                if (processor.Type.BaseType == type)
                {
                    return processor;
                }
            }

            //also query the render.

            if (SGL.SpriteBatch.Graphics.ContentProcessors != null)
            {
                foreach (IContentProcessor processor in SGL.SpriteBatch.Graphics.ContentProcessors)
                {
                    if (processor.Type == type)
                    {
                        return processor;
                    }

                    if (processor.Type.BaseType == type)
                    {
                        return processor;
                    }
                }
            }

            throw new InvalidOperationException("ContentProcessor for type " + type.Name + " not found.");
        }
    }
}