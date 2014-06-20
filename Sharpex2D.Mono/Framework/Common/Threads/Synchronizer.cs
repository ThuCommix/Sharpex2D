using System;
using System.Collections.Generic;

namespace Sharpex2D.Framework.Common.Threads
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    internal class Synchronizer
    {
        private readonly List<Locker> _objects;

        /// <summary>
        ///     Initializes a new Synchronizer class.
        /// </summary>
        public Synchronizer()
        {
            _objects = new List<Locker>();
        }

        /// <summary>
        ///     A value indicating whether the two Objects are synced.
        /// </summary>
        public bool IsSynced
        {
            get { return InternalIsSynced(); }
        }

        /// <summary>
        ///     InternalIsSynced.
        /// </summary>
        /// <returns>True if synced.</returns>
        private bool InternalIsSynced()
        {
            for (int i = 0; i <= _objects.Count - 1; i++)
            {
                if (!_objects[i].Synced)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Synchronizes into the Synchronizer.
        /// </summary>
        /// <returns>SynchronizeObject</returns>
        public Locker Synchronize()
        {
            var syncobject = new Locker(Guid.NewGuid());
            _objects.Add(syncobject);
            return syncobject;
        }

        /// <summary>
        ///     Asynchronizes from the Synchronizer.
        /// </summary>
        /// <param name="syncObject">The SynchronizeObject</param>
        public void Asynchron(Locker syncObject)
        {
            if (_objects.Contains(syncObject))
            {
                _objects.Remove(syncObject);
            }
        }

        internal class Locker
        {
            /// <summary>
            ///     Initializes a new SynchronizeObject class.
            /// </summary>
            /// <param name="guid">The Guid.</param>
            internal Locker(Guid guid)
            {
                Guid = guid;
                Synced = true;
            }

            /// <summary>
            ///     A value indicating whether the SynchronizeObject is synced.
            /// </summary>
            public bool Synced { set; get; }

            /// <summary>
            ///     Gets the Guid.
            /// </summary>
            public Guid Guid { private set; get; }
        }
    }
}