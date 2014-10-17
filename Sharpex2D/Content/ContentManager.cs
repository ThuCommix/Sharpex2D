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
using System.Threading.Tasks;
using Sharpex2D.Content.Pipeline;

namespace Sharpex2D.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ContentManager : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0DD94218-396E-4EBA-9B3C-EAD05420A375"); }
        }

        #endregion

        /// <summary>
        /// BatchProgressEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void BatchProgressEventHandler(object sender, BatchProgressEventArgs e);

        private readonly List<IBatch> _batchList;
        private readonly Dictionary<string, IContent> _contentCache;
        private bool _batching;

        /// <summary>
        /// Initializes a new ContentManager.
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
            _batchList = new List<IBatch>();
        }

        /// <summary>
        /// Sets or gets the base ContentPath.
        /// </summary>
        public string RootPath { get; private set; }

        /// <summary>
        /// Gets the ContentVerifier.
        /// </summary>
        public ContentVerifier ContentVerifier { private set; get; }

        /// <summary>
        /// Gets the ContentProcessor.
        /// </summary>
        public ContentProcessorSelector ContentProcessor { private set; get; }

        /// <summary>
        /// BatchProgressChanged event.
        /// </summary>
        public event BatchProgressEventHandler BatchProgressChanged;

        /// <summary>
        /// Loads an asset.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <returns>T.</returns>
        public T Load<T>(string asset) where T : IContent
        {
            //make the path valid if not
            asset = asset.Replace("/", @"\");

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
            var contentData = (T) processor.ReadData(Path.Combine(RootPath, asset));

            ApplyCache(asset, contentData);

            return contentData;
        }

        /// <summary>
        /// Queues a Batch.
        /// </summary>
        /// <param name="batch">The Batch.</param>
        public void Queue(IBatch batch)
        {
            if (_batching)
            {
                throw new InvalidOperationException("Enqueue is running.");
            }

            _batchList.Add(batch);
        }

        /// <summary>
        /// Loads all batches.
        /// </summary>
        public void Enqueue()
        {
            if (_batching)
            {
                throw new InvalidOperationException("Enqueue is running already.");
            }

            _batching = true;
            new Task(EnqueueInner).Start();
        }

        /// <summary>
        /// Loads all batches.
        /// </summary>
        private void EnqueueInner()
        {
            long totalBytes = 0;
            long processedBytes = 0;
            for (int j = 0; j <= _batchList.Count - 1; j++)
            {
                totalBytes += new FileInfo(Path.Combine(RootPath, _batchList[j].Asset)).Length;
            }

            for (int i = 0; i <= _batchList.Count - 1; i++)
            {
                IContent data = Load(_batchList[i].Asset, _batchList[i].Type);
                _batchList[i].RaiseEvent(data);
                processedBytes += new FileInfo(Path.Combine(RootPath, _batchList[i].Asset)).Length;

                var eventArgs = new BatchProgressEventArgs
                {
                    Count = _batchList.Count,
                    Current = _batchList[i],
                    Processed = i,
                    ProcessedBytes = processedBytes,
                    TotalBytes = totalBytes,
                    ProgressPercentage = 100*i/_batchList.Count
                };

                if (BatchProgressChanged != null)
                {
                    BatchProgressChanged(this, eventArgs);
                }
            }
            _batching = false;
            _batchList.Clear();

            var completedArgs = new BatchProgressEventArgs
            {
                Count = _batchList.Count,
                Current = null,
                Processed = _batchList.Count,
                ProcessedBytes = processedBytes,
                TotalBytes = totalBytes,
                ProgressPercentage = 100,
                Completed = true
            };

            if (BatchProgressChanged != null)
            {
                BatchProgressChanged(this, completedArgs);
            }
        }

        /// <summary>
        /// Loads an asset.
        /// </summary>
        /// <param name="asset">The Asset.</param>
        /// <param name="type">The Type.</param>
        /// <returns>IContent.</returns>
        private IContent Load(string asset, Type type)
        {
            IContent data;
            if (QueryCache(asset, type, out data))
            {
                return data;
            }

            if (!File.Exists(Path.Combine(RootPath, asset)))
            {
                throw new ContentLoadException("Asset not found.");
            }

            IContentProcessor processor = ContentProcessor.Select(type);
            var contentData = (IContent) processor.ReadData(Path.Combine(RootPath, asset));

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
            foreach (KeyValuePair<string, IContent> data in _contentCache)
            {
                if (data.Value is T && data.Key == asset)
                {
                    contentData = (T) data.Value;
                    return true;
                }
            }

            contentData = default(T);
            return false;
        }

        /// <summary>
        /// Queries the cache.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <param name="asset">The Asset.</param>
        /// <param name="contentData">The ContentData.</param>
        /// <returns>True on success.</returns>
        internal bool QueryCache(string asset, Type type, out IContent contentData)
        {
            foreach (KeyValuePair<string, IContent> data in _contentCache)
            {
                if (data.Value.GetType() == type && data.Key == asset)
                {
                    contentData = data.Value;
                    return true;
                }
            }

            contentData = null;
            return false;
        }
    }
}