using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Physics.Collision;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Physics
{
    public class PhysicProvider : IPhysicProvider
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("5C5C22FE-85F3-4FBE-A567-E201C40AEF82"); }
        }

        #endregion

        #region IGameHandler Implementation
        /// <summary>
        /// Constructs the Component
        /// </summary>
        public void Construct()
        {
        }
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (var particle in _particles)
            {
                UpdateParticles(particle, elapsed);
            }
            //Clear the references
            _referenceProvider.ClearReferences();
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            
        }

        #endregion

        #region IPhysicProvider Implementation
        /// <summary>
        /// Subscribes a particle to the current PhysicProvider class.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        public void Subscribe(Particle particle)
        {
            _particles.Add(particle);
        }
        /// <summary>
        /// Unsubscribes a particle from the current PhysicProvider class.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        public void Unsubscribe(Particle particle)
        {
            _particles.Remove(particle);
        }
        /// <summary>
        /// Sets or gets the lower bound.
        /// </summary>
        public float LowerBound { get; set; }
        /// <summary>
        /// Sets or gets the upper bound.
        /// </summary>
        public float UpperBound { get; set; }
        /// <summary>
        /// Sets or gets the left bound.
        /// </summary>
        public float BoundLeft { get; set; }
        /// <summary>
        /// Sets or gets the right bound.
        /// </summary>
        public float BoundRight { get; set; }
        /// <summary>
        /// Sets the velocity of the particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <param name="velocity">The Velocity.</param>
        public void AddParticleVelocity(Particle particle, Vector2 velocity)
        {
            particle.Velocity += velocity;
        }

        /// <summary>
        /// Sets the velocity of the particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <param name="velocity">The Velocity.</param>
        public void SetParticleVelocity(Particle particle, Vector2 velocity)
        {
            particle.Velocity = velocity;
        }

        #endregion

        #region PhysicProvider
        /// <summary>
        /// Initializes a new PhysicProvider class.
        /// </summary>
        public PhysicProvider()
        {
            _particles = new List<Particle>();
            LowerBound = 468.0f;
            UpperBound = -99999.0f;
            BoundLeft = 99999.0f;
            BoundRight = 99999.0f;
            Gravity = PhysicalConstants.Gravitation;
            CollisionManager = new CollisionManager();
            _referenceProvider = new ReferenceProvider();
            SGL.Components.Get<IGameLoop>().Subscribe(this);
            SGL.Components.AddComponent(this);
        }

        private readonly List<Particle> _particles;
        private readonly ReferenceProvider _referenceProvider;

        /// <summary>
        /// Indicating whether the gravity should be emulated.
        /// </summary>
        public bool EnableGravity { set; get; }
        /// <summary>
        /// Sets or gets the custom gravity.
        /// </summary>
        public float Gravity { set; get; }
        /// <summary>
        /// Gets the current CollisionManager.
        /// </summary>
        public ICollision CollisionManager { private set; get; }
        /// <summary>
        /// Updates the given particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <param name="elapsed">The Elapsed.</param>
        private void UpdateParticles(Particle particle, float elapsed)
        {
            //Check if the gravity should be used for our particle.
            //Do not apply if our position is already on the lower bound
            if (EnableGravity && particle.Gravity)
            {
                var particleVelocity = VelocityOfFall(elapsed);
                particle.Velocity = new Vector2(particle.Velocity.X, particle.Velocity.Y + particleVelocity);
            }

            //save the current position
            var posBeforUpdate = particle.Position;

            //Apply damping, we need something to brake the particle
            particle.Velocity = ParticleDamping(particle.Velocity, particle.Damping);

            //After all Vigours update our position.
            var calibratedPosition = particle.Position + particle.Velocity;

            //After setting the position we should check out the world bounds
            //Below the basic world collision

            //Check if our particle hit the lower bound
            if (calibratedPosition.Y >= LowerBound)
            {
                //We should reset the particles y coord back to the lower bound
                //else it would flip out of the world area
                calibratedPosition = new Vector2(calibratedPosition.X, LowerBound);

                //but also we should apply ealstic impact or non elastic impact:
                if (System.Math.Abs(particle.Elasticity) < 0.01f)
                {
                    var energy = Ekin(particle);
                    particle.Velocity = new Vector2(0, 0);
                    particle.OnPenetration(new PenetrationParams
                    {
                        InnerEnergy = energy,
                        InvolvedParticles = new[] {particle}
                    });
                }
                else
                {
                    var callback = ElasticImpact(particle,
                        new Particle(null) {Mass = 99999f, Elasticity = 1f, Velocity = new Vector2(0, 0)});
                    particle.Velocity = callback[0];
                    particle.OnRecoil(new RecoilParams {InvolvedParticles = new[] {particle}});
                }
            }
            //Check if our particle hit the upper bound
            if (calibratedPosition.Y <= UpperBound)
            {
                //It is the same as for lower bound, but now we should set the y coord
                //to the upperbound
                calibratedPosition = new Vector2(calibratedPosition.X, UpperBound);
            }
            //Check if our particle hit the left bound
            if (calibratedPosition.X <= -BoundLeft)
            {
                //Reset the x coord back to the maximum
                calibratedPosition = new Vector2(BoundLeft, calibratedPosition.Y);
            }
            //Check if our particle hit the right bound
            if (calibratedPosition.X >= BoundRight)
            {
                calibratedPosition = new Vector2(BoundRight, calibratedPosition.Y);
            }

            //apply calibratedPosition:
            particle.Position = LowerBound - calibratedPosition.Y < 0.2f ? new Vector2(calibratedPosition.X, LowerBound) : calibratedPosition;

            for (var i = 0; i <= _particles.Count - 1; i++)
            {
                if (_particles[i] != particle)
                {
                    //Check if we already processed this collision
                    if (_referenceProvider.IsProcessed(particle, _particles[i]) == false)
                    {
                        if (CollisionManager.IsIntersecting(particle, _particles[i]))
                        {
                            _referenceProvider.AddReference(new CollisionReference(particle, _particles[i]));

                            //Check if elastic impact or non elastic
                            if (System.Math.Abs(particle.Elasticity) < 0.01f &&
                                System.Math.Abs(_particles[i].Elasticity) < 0.01f)
                            {
                                //Compute non elastic, and apply new velocities
                                var ekinParticle = MathHelper.Abs(Ekin(particle) + Ekin(_particles[i]));
                                var callback = NonElasticImpact(particle, _particles[i]);
                                var ekinParticleAfter = Ekin(particle);
                                var param = new PenetrationParams
                                {
                                    InvolvedParticles = new[] {particle, _particles[i]},
                                    InnerEnergy = MathHelper.Abs(ekinParticle - ekinParticleAfter)
                                };
                                particle.Velocity = callback;
                                _particles[i].Velocity = callback;
                                particle.OnPenetration(param);
                                _particles[i].OnPenetration(param);
                                particle.Position = posBeforUpdate;
                            }
                            else
                            {
                                //compute elastic impact and apply new velocities
                                var callback = ElasticImpact(particle, _particles[i]);
                                var param = new RecoilParams {InvolvedParticles = new[] {particle, _particles[i]}};
                                particle.Velocity = callback[0];
                                _particles[i].Velocity = callback[1];
                                particle.OnRecoil(param);
                                _particles[i].OnRecoil(param);
                                //_particles[i].Position += _particles[i].Velocity; Update in next tick call
                                particle.Position = posBeforUpdate;
                            }
                        }
                    }
                }
            }
        }

        #region Vigours

        /// <summary>
        /// Gets the velocity of fall of the object.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        /// <returns>Velocity in meter per second</returns>
        private float VelocityOfFall(float elapsed)
        {
            /*/
             * v = g * t
             * v = velocity
             * g = gravity constant
             * t = elapsed time in seconds
            /*/

            var tSecond = elapsed/1000f;
            var gravity = Gravity;

            var velocity = gravity*tSecond;
            return velocity;
        }

        /// <summary>
        /// Returns the damped velocity of the object.
        /// </summary>
        /// <param name="velocity">The Velocity.</param>
        /// <param name="damping">The Damping.</param>
        /// <returns>Damped Velocity</returns>
        private Vector2 ParticleDamping(Vector2 velocity, float damping)
        {
            float xDamped;
            float yDamped;
            var dampingValue = damping / 15;
            if (velocity.X > 0)
            {
                xDamped = velocity.X - dampingValue >= 0 ? velocity.X - dampingValue : 0;
            }
            else
            {
                xDamped = velocity.X + dampingValue <= 0 ? velocity.X + dampingValue : 0;
            }
            if (velocity.Y > 0)
            {
                yDamped = velocity.Y + dampingValue >= 0 ? velocity.Y - dampingValue : 0;
            }
            else
            {
                yDamped = velocity.Y - dampingValue <= 0 ? velocity.Y + dampingValue : 0;
            }

            return new Vector2(xDamped, yDamped);
        }
        /// <summary>
        /// Damps the particle on impact.
        /// </summary>
        /// <param name="originVelocity">The Velocity.</param>
        /// <returns>Damped Velocity</returns>
        private Vector2 DampOnImpact(Vector2 originVelocity)
        {
            // lets say we loos 20% of power after impact
            return new Vector2(originVelocity.X * 80 / 100, originVelocity.Y * 80 / 100);
        }

        /// <summary>
        /// Returns the new velocity of two particles after an elastic impact.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>Velocity</returns>
        private Vector2[] ElasticImpact(Particle particle1, Particle particle2)
        {
            /*/
             * u1 = m1 * v1 + m2 * (2 * v2 − v1) / m1 + m2
             * u2 = m2 * v2 + m1 * (2 * v1 − v2) / m1 + m2
             * u1 = velocity of particle 1
             * u2 = velocity of particle 2
             * m1 = mass of velocity of particle 1
             * m2 = mass of velocity of paricle 2
             * v1 = velocity of particle 1
             * v2 = velocity of particle 2
            /*/

            //We also need to apply the new angle: angle in = angle out

            if (System.Math.Abs(particle1.Velocity.X) > 0)
            {
                particle1.Velocity = new Vector2(-particle1.Velocity.X, particle1.Velocity.Y);
            }
            if (System.Math.Abs(particle2.Velocity.X) > 0)
            {
                particle2.Velocity = new Vector2(-particle2.Velocity.X, particle2.Velocity.Y);
            }

            //check if object2 got a very hight mass compared to obj1
            if (particle2.Mass/particle1.Mass > 10)
            {
                //only use reflection:
                particle1.Velocity = DampOnImpact(particle1.Velocity);
                return new[] {-particle1.Velocity*particle1.Elasticity, particle2.Velocity};
            }

            //get min elasticity
            var elasticity = MathHelper.Min(particle1.Elasticity, particle2.Elasticity);


            //check if obj2.velocity = 0 and the mass of both objects are the same
            //then velocity of obj1 = velocity of obj2, obj1.velocity = 0
            if (particle2.Velocity == new Vector2(0, 0) && System.Math.Abs(particle1.Mass - particle2.Mass) < 0.01f)
            {
                return new[] {new Vector2(0, 0), particle1.Velocity};
            }
            var u1 = particle1.Velocity*particle1.Mass +
                     (particle2.Velocity*2 - particle1.Velocity)*particle2.Mass/
                     (particle1.Mass + particle2.Mass);
            var u2 = particle2.Velocity*particle2.Mass +
                     (particle1.Velocity*2 - particle2.Velocity)*particle1.Mass/
                     (particle1.Mass + particle2.Mass);
            var velocity1 = DampOnImpact(u1*elasticity);
            var velocity2 = DampOnImpact(u2*elasticity);
            return new[] {velocity1, velocity2};
        }

        /// <summary>
        /// Returns the new velocity after a non elastic impact.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>Velocity</returns>
        private Vector2 NonElasticImpact(Particle particle1, Particle particle2)
        {
            /*/
             * v2 = m1 * v1 + m2 * v2 / (m1 + m2)
             * v2 = velocity of the two objects
             * m1 = mass of obj1
             * m2 = mass of obj2
             * v1 = velocity of obj1
             * v2 = velocity of obj2
            /*/

            var v2 = particle1.Velocity*particle1.Mass +
                     particle2.Velocity*particle2.Mass/(particle1.Mass + particle2.Mass);

            return v2;
        }
        /// <summary>
        /// Returns the kinetic energy of the given particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <returns>Ekin</returns>
        private float Ekin(Particle particle)
        {
            //ekin = Mass * velocity² / 2
            return particle.Mass*MathHelper.Pow(particle.Velocity.Length, 2)/2;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the kinetic energy of the given particle.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <returns>Ekin</returns>
        public float GetEkin(Particle particle)
        {
            return Ekin(particle);
        }
        /// <summary>
        /// Gets the velocity of fall of the object.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        /// <returns>Velocity in meter per second</returns>
        public float GetVelocityOfFall(float elapsed)
        {
            return VelocityOfFall(elapsed);
        }
        /// <summary>
        /// Returns the damped velocity of the object.
        /// </summary>
        /// <param name="particle">The Particle.</param>
        /// <returns>Damped Velocity</returns>
        public Vector2 DampParticle(Particle particle)
        {
            return ParticleDamping(particle.Velocity, particle.Damping);
        }
        /// <summary>
        /// Returns the new velocity of two particles after an elastic impact.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>Velocity</returns>
        public Vector2[] GetElasticImpactValues(Particle particle1, Particle particle2)
        {
            return ElasticImpact(particle1, particle2);
        }
        /// <summary>
        /// Returns the new velocity after a non elastic impact.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>Velocity</returns>
        public Vector2 GetNonElasticImpactValues(Particle particle1, Particle particle2)
        {
            return NonElasticImpact(particle1, particle2);
        }

        #endregion

        #endregion
    }
}
