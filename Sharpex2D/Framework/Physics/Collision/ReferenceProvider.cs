using System.Collections.Generic;

namespace Sharpex2D.Framework.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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