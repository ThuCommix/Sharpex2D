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

namespace Sharpex2D.Framework.Common.Threads
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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