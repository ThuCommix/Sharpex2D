using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sharpex2D.Framework.Content.Storage;

namespace Sharpex2D.Framework.Content
{
    public class AsyncAssetLoader<T> where T : IContent
    {

        private readonly Dictionary<string, string> _queue;
        private readonly ContentManager _contentManager;
        private readonly ContentStorage _storage;

        /// <summary>
        /// Initializes a new AssetLoader class.
        /// </summary>
        /// <param name="storage">The ContentStorage which will be used to store the data.</param>
        /// <param name="contentManager">The ContentManager.</param>
        public AsyncAssetLoader(ContentStorage storage, ContentManager contentManager)
        {
            _storage = storage;
            ProgressPercentage = 0;
            _contentManager = contentManager;
            _queue = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the ProgressPercentage.
        /// </summary>
        public int ProgressPercentage { get; private set; }
        /// <summary>
        /// Sets or gets the Description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Adds a new task to the queue.
        /// </summary>
        /// <param name="name">The later name of the ContentStorage item.</param>
        /// <param name="assetpath">The AssetPath.</param>
        public void Queque(string name, string assetpath)
        {
            if (_queue.ContainsKey(name))
            {
                throw new ArgumentException("The name already exists.");
            }
 
            _queue.Add(name, assetpath);
        }
        /// <summary>
        /// Loads all quequed assets async.
        /// </summary>
        public void LoadAsync()
        {
            ProgressPercentage = 0;
            var task = new Task(InternalLoadAsync);
            task.Start();
        }
        /// <summary>
        /// InternalLoadAsync.
        /// </summary>
        private void InternalLoadAsync()
        {
            var assetCount = _queue.Count;
            var processedAssets = 0;

            foreach (var asset in _queue)
            {
                _storage.Add(asset.Key, _contentManager.Load<T>(asset.Value));
                processedAssets++;
                ProgressPercentage = 100*processedAssets/assetCount;
            }

            _queue.Clear();
        }
    }
}
