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
using System.IO;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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

        private readonly Dictionary<string, IContent> _contentCache;

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

            _contentCache = new Dictionary<string, IContent>();
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
            //query content cache first.
            T data;
            if (QueryCache(asset, out data))
            {
                return data;
            }

            if (!File.Exists(Path.Combine(RootPath, asset)))
            {
                throw new ContentLoadException("Asset not found.");
            }

            IContentProcessor processor = ContentProcessor.Select<T>();
            var contentData = (T)processor.ReadData(Path.Combine(RootPath, asset));

            ApplyCache(asset, contentData);

            return contentData;
        }

        /// <summary>
        /// Apply the cache.
        /// </summary>
        /// <param name="asset">The Asset.</param>
        /// <param name="data">The Data.</param>
        private void ApplyCache(string asset, IContent data)
        {
            if (!_contentCache.ContainsKey(asset))
            {
                _contentCache.Add(asset, data);
            }
        }

        /// <summary>
        /// Queries the cache.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <param name="contentData">The ContentData.</param>
        /// <returns>True on success.</returns>
        internal bool QueryCache<T>(string asset, out T contentData) where T : IContent
        {
            foreach (var data in _contentCache)
            {
                if (data.Value is T && data.Key == asset)
                {
                    contentData = (T)data.Value;
                    return true;
                }
            }

            contentData = default(T);
            return false;
        }
    }
}