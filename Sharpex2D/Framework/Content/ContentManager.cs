using System;
using System.IO;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ContentManager : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0DD94218-396E-4EBA-9B3C-EAD05420A375"); }
        }

        #endregion

        private Logger _logger;

        /// <summary>
        ///     Initializes a new ContentManager.
        /// </summary>
        public ContentManager()
        {
            RootPath = Path.Combine(Environment.CurrentDirectory, "Content");
            ContentVerifier = new ContentVerifier();
            ContentProcessor = new ContentProcessorSelector();

            _logger = LogManager.GetClassLogger();

            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }
        }

        /// <summary>
        ///     Sets or gets the base ContentPath.
        /// </summary>
        public string RootPath { get; private set; }

        /// <summary>
        ///     Gets the ContentVerifier.
        /// </summary>
        public ContentVerifier ContentVerifier { private set; get; }

        /// <summary>
        ///     Gets the ContentProcessor.
        /// </summary>
        public ContentProcessorSelector ContentProcessor { private set; get; }

        /// <summary>
        ///     Loads an asset.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <returns>T.</returns>
        public T Load<T>(string asset) where T : IContent
        {
            if (!File.Exists(Path.Combine(RootPath, asset)))
            {
                throw new ContentLoadException("Asset not found.");
            }

            IContentProcessor processor = ContentProcessor.Select<T>();
            string resourceName = typeof (T).Name;

            try
            {
                resourceName = AttributeHelper.GetAttribute<ContentAttribute>(typeof (T)).DisplayName;
            }
            catch (Exception)
            {
                _logger.Warn("Unable to read the ContentAttribute.");
            }

            _logger.Engine(
                "Loaded {0} with {1} ({2}).", resourceName, processor.GetType().Name, processor.Guid);


            return (T) processor.ReadData(Path.Combine(RootPath, asset));
        }
    }
}