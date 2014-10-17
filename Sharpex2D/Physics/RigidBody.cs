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
using Sharpex2D.Math;

namespace Sharpex2D.Physics
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class RigidBody
    {
        private readonly float _invertedInertia;
        private readonly float _invertedMass;
        private readonly Vector2 _origCenter;
        private readonly Polygon _origShape;
        private Vector2 _forces;
        private float _torque;

        /// <summary>
        /// Initializes a new RigidBody class.
        /// </summary>
        /// <param name="mass">The Mass.</param>
        /// <param name="shape">The Shape.</param>
        public RigidBody(float mass, Polygon shape) : this(mass, shape, new Vector2(0, 0), 0)
        {
        }

        /// <summary>
        /// Initializes a new RigidBody class.
        /// </summary>
        /// <param name="mass">The Mass.</param>
        /// <param name="points">The Points of a Polygon.</param>
        public RigidBody(float mass, params Vector2[] points) : this(mass, new Polygon(points))
        {
        }

        /// <summary>
        /// Initializes a new RigidBody class.
        /// </summary>
        /// <param name="mass">The Mass.</param>
        /// <param name="shape">The Shape.</param>
        /// <param name="velocity">The Velocity.</param>
        /// <param name="angularVelocity">The AngularVelocity.</param>
        public RigidBody(float mass, Polygon shape, Vector2 velocity, float angularVelocity)
        {
            if (mass < 0)
            {
                throw new InvalidOperationException("Invalid mass. Mass must be greater zero or greater than zero.");
            }

            if (!shape.IsValid)
            {
                throw new InvalidOperationException("The specified shape is not valid.");
            }

            _origShape = shape;
            _origCenter = shape.Center;
            Shape = shape;

            Center = shape.Center;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            Mass = mass;
            _invertedMass = 1f/mass;
            _torque = 0;
            _forces = new Vector2(0, 0);

            float denom = 0;
            float num = 0;
            for (int i = 0; i < Shape.Points.Length - 1; i++)
            {
                Vector2 a = Shape.Points[i + 1] - Center;
                Vector2 b = Shape.Points[i] - Center;
                float f = MathHelper.Abs(a.X*b.Y - a.Y*b.X);
                denom += f*(Vector2.Dot(a, a) + Vector2.Dot(a, b) + Vector2.Dot(b, b));
                num += f;
            }

            Inertia = (mass*denom)/(6*num);
            _invertedInertia = Inertia == 0 ? 0 : 1f/Inertia;
        }

        /// <summary>
        /// Gets the Center point.
        /// </summary>
        public Vector2 Center { private set; get; }

        /// <summary>
        /// Gets the linear Velocity.
        /// </summary>
        public Vector2 Velocity { private set; get; }

        /// <summary>
        /// Gets the Mass.
        /// </summary>
        public float Mass { private set; get; }

        /// <summary>
        /// Gets the AngularVelocity.
        /// </summary>
        public float AngularVelocity { private set; get; }

        /// <summary>
        /// Gets the Rotation angle.
        /// </summary>
        public float Rotation { private set; get; }

        /// <summary>
        /// gets the shape.
        /// </summary>
        public Polygon Shape { private set; get; }

        /// <summary>
        /// Gets the Inertia.
        /// </summary>
        public float Inertia { private set; get; }

        /// <summary>
        /// Gets the InvertedMass.
        /// </summary>
        public float InvertedMass
        {
            get { return _invertedMass; }
        }

        /// <summary>
        /// Gets the InvertedInertia.
        /// </summary>
        public float InvertedInertia
        {
            get { return _invertedInertia; }
        }

        /// <summary>
        /// A value indicating whether the RigidBody is static.
        /// </summary>
        public bool IsStatic
        {
            get { return Mass == 0; }
        }

        /// <summary>
        /// Applys an impulse.
        /// </summary>
        /// <param name="impulse">The Impulse.</param>
        /// <param name="impactPoint">The ImpactPoint.</param>
        public void ApplyImpulse(Vector2 impulse, Vector2 impactPoint)
        {
            if (Mass == 0) return;

            Velocity += impulse*_invertedMass;
            AngularVelocity += _invertedInertia*Vector2.VectorProduct(impactPoint, impulse);
        }

        /// <summary>
        /// Applys a force.
        /// </summary>
        /// <param name="force">The Force.</param>
        /// <param name="impactPoint">The ImpactPoint.</param>
        public void ApplyForce(Vector2 force, Vector2 impactPoint)
        {
            if (Mass == 0) return;

            _forces += force;
            Vector2 v1 = impactPoint - Center;
            float len = force.Length*v1.Length;
            float t = MathHelper.Atan2(force.Y, force.X) - MathHelper.Atan2(v1.Y, v1.X);
            _torque += len*MathHelper.Sin(t);
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Update(float elapsed)
        {
            if (Mass == 0) return;

            AngularVelocity += _torque*_invertedInertia*elapsed;
            Velocity += _forces*_invertedMass*elapsed;

            Rotation += AngularVelocity*elapsed;
            Center += Velocity*elapsed;
            Shape =
                new Polygon(
                    (Matrix2x3.Translation(Center)*Matrix2x3.Rotation(Rotation)*Matrix2x3.Translation(-_origCenter))
                        .ApplyTo(_origShape.Points));

            RemoveForces();
        }

        /// <summary>
        /// A value indicating whether two shapes are colliding.
        /// </summary>
        /// <param name="rigidBody">The Other Rigidbody.</param>
        /// <param name="minimumTranslationVector">The MinimumTranslationVector.</param>
        /// <returns>True if colliding.</returns>
        public bool Intersects(RigidBody rigidBody, out Vector2 minimumTranslationVector)
        {
            return rigidBody.Intersects(this, out minimumTranslationVector);
        }

        /// <summary>
        /// Removes all forces.
        /// </summary>
        public void RemoveForces()
        {
            _forces = new Vector2(0, 0);
            _torque = 0;
        }
    }
}