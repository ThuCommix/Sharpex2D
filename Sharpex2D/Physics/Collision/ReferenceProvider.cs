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

using System.Collections.Generic;

namespace Sharpex2D.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class ReferenceProvider
    {
        private readonly List<CollisionReference> _references;

        /// <summary>
        ///     Initializes a new CollisionReferences class.
        /// </summary>
        public ReferenceProvider()
        {
            _references = new List<CollisionReference>();
        }

        /// <summary>
        ///     Adds a new CollisionReference to the ReferenceProvider.
        /// </summary>
        /// <param name="reference">The Reference.</param>
        public void AddReference(CollisionReference reference)
        {
            _references.Add(reference);
        }

        /// <summary>
        ///     Clears the current References, this should be called at the end of an update.
        /// </summary>
        public void ClearReferences()
        {
            _references.Clear();
        }

        /// <summary>
        ///     Indicates whether the particle collision is already processed.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True if processed</returns>
        public bool IsProcessed(Particle particle1, Particle particle2)
        {
            bool result = false;
            if (_references.Count == 0) return false;
            for (int i = 0; i <= _references.Count - 1; i++)
            {
                if (_references[i].C1 == particle1 || _references[i].C1 == particle2 && _references[i].C2 == particle1 ||
                    _references[i].C2 == particle2)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}