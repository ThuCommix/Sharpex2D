using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Content.Pipeline.Processor;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ContentProcessorSelector
    {
        private readonly List<IContentProcessor> _contentProcessors;

        /// <summary>
        ///     Initializes a new ContentProcessorSelector class.
        /// </summary>
        public ContentProcessorSelector()
        {
            _contentProcessors = new List<IContentProcessor>();

            Add(new SoundContentProcessor());
            Add(new VideoContentProcessor());
            Add(new GamerContentProcessor());
        }

        /// <summary>
        ///     Adds a new ContentProcessor.
        /// </summary>
        /// <param name="contentProcessor"></param>
        public void Add(IContentProcessor contentProcessor)
        {
            _contentProcessors.Add(contentProcessor);
            LogManager.GetClassLogger().Engine("Registered ContentProcessor: {0}.", contentProcessor.Type.Name);
        }

        /// <summary>
        ///     Selects a ContentProcessor.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>ContentProcessor T.</returns>
        public IContentProcessor Select<T>() where T : IContent
        {
            Type requestedType = typeof (T);

            foreach (IContentProcessor processor in _contentProcessors)
            {
                if (processor.Type == requestedType)
                {
                    return processor;
                }

                if (processor.Type.BaseType == requestedType)
                {
                    return processor;
                }
            }

            //also query the render.

            if (SGL.RenderDevice.ContentProcessors != null)
            {
                foreach (IContentProcessor processor in SGL.RenderDevice.ContentProcessors)
                {
                    if (processor.Type == requestedType)
                    {
                        return processor;
                    }

                    if (processor.Type.BaseType == requestedType)
                    {
                        return processor;
                    }
                }
            }

            throw new InvalidOperationException("ContentProcessor for type " + requestedType.Name + " not found.");
        }
    }
}