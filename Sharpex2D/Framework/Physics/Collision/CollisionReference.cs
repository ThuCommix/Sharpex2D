using System;

namespace Sharpex2D.Framework.Physics.Collision
{
    [Serializable]
    internal class CollisionReference
    {
        /// <summary>
        /// Initializes a new CollisionReference class.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        public CollisionReference(Particle particle1, Particle particle2)
        {
            C1 = particle1;
            C2 = particle2;
        }
        /// <summary>
        /// Gets the first Particle.
        /// </summary>
        public Particle C1 {private set; get; }
        /// <summary>
        /// Gets the second Particle.
        /// </summary>
        public Particle C2 { private set; get; }
    }
}
