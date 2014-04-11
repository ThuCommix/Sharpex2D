using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Physics
{
    public interface IPhysicProvider : IGameHandler
    {
        /// <summary>
        /// Subscribes a particle to the current PhysicProvider class.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        void Subscribe(Particle particle);
        /// <summary>
        /// Unsubscribes a particle from the current PhysicProvider class.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        void Unsubscribe(Particle particle);
        /// <summary>
        /// Sets or gets the lower bound.
        /// </summary>
        float LowerBound { set; get; }
        /// <summary>
        /// Sets or gets the upper bound.
        /// </summary>
        float UpperBound { set; get; }
        /// <summary>
        /// Sets or gets the left bound.
        /// </summary>
        float BoundLeft { set; get; }
        /// <summary>
        /// Sets or gets the right bound.
        /// </summary>
        float BoundRight { set; get; }
        /// <summary>
        /// Adds the velocity to the particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <param name="velocity">The Velocity.</param>
        void AddParticleVelocity(Particle particle, Vector2 velocity);
        /// <summary>
        /// Sets the velocity of the particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <param name="velocity">The Velocity.</param>
        void SetParticleVelocity(Particle particle, Vector2 velocity);
    }
}
