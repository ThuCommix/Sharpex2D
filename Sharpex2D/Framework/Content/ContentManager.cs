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

        /// <summary>
        ///     Initializes a new ContentManager.
        /// </summary>
        public ContentManager()
        {
            RootPath = Path.Combine(Environment.CurrentDirectory, "Content");
            ContentVerifier = new ContentVerifier();
            ContentProcessor = new ContentProcessorSelector();

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

            var processor = ContentProcessor.Select<T>();
            var resourceName = typeof (T).Name;

            try
            {
                resourceName = ContentProcessorHelper.GetAttribute<ContentAttribute>(typeof (T)).DisplayName;
            }
            catch (Exception)
            {
                Log.Next("Unable to read the ContentAttribute.", LogLevel.Warning, LogMode.StandardOut);
            }

            Log.Next(
                "Loaded " + resourceName + " with " + processor.GetType().Name + " (" + processor.Guid +
                ")",
                LogLevel.Engine, LogMode.StandardOut);


            return processor.ReadData(Path.Combine(RootPath, asset));
        }
    }
}