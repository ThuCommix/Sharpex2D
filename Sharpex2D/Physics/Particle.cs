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
using Sharpex2D.Entities;
using Sharpex2D.Math;
using Sharpex2D.Physics.Collision;
using Sharpex2D.Physics.Shapes;
using Sharpex2D.Rendering;
using Rectangle = Sharpex2D.Physics.Shapes.Rectangle;

namespace Sharpex2D.Physics
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Obsolete("The old physic system will be removed in the future. Please use alternatives.")]
    public class Particle : Entity
    {
        private readonly IPhysicProvider _physicProvider;
        private float _damping = 1f;
        private float _elasticity = 1f;
        private float _inverseMass;
        private Vector2 _velocity = new Vector2(0, 0);

        /// <summary>
        /// Initializes a new Particle.
        /// </summary>
        public Particle()
        {
            Position = new Vector2(0, 0);
            Shape = new Rectangle();
            Gravity = true;
        }

        /// <summary>
        /// Initializes a new Particle.
        /// </summary>
        /// <param name="physicProvider">The PhysicProvider.</param>
        public Particle(IPhysicProvider physicProvider)
        {
            _physicProvider = physicProvider;
            if (_physicProvider != null)
            {
                physicProvider.Subscribe(this);
            }
            Position = new Vector2(0, 0);
            Shape = new Rectangle();
            Gravity = true;
        }

        #region IShape Implementation

        /// <summary>
        /// Gets or sets the type of the shape.
        /// </summary>
        public IShape Shape { set; get; }

        #endregion

        /// <summary>
        /// Sets or gets the mass of the object.
        /// </summary>
        public float Mass
        {
            set { _inverseMass = 1.0f/value; }
            get { return 1.0f/_inverseMass; }
        }

        /// <summary>
        /// Sets or gets the elasticity of the object.
        /// </summary>
        public float Elasticity
        {
            set { _elasticity = value; }
            get { return _elasticity; }
        }

        /// <summary>
        /// Sets or gets the velocity of the object.
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Sets or gets the damping value of the object.
        /// </summary>
        public float Damping
        {
            get { return _damping; }
            set { _damping = value; }
        }

        /// <summary>
        /// Sets or gets whether gravity should be used.
        /// </summary>
        public bool Gravity { set; get; }

        /// <summary>
        /// Gets the linked PhysicProvider.
        /// </summary>
        public IPhysicProvider PhysicProvider
        {
            get { return _physicProvider; }
        }

        /// <summary>
        /// Called after a non-elastic impact.
        /// </summary>
        /// <param name="penetration">The PenetrationParams.</param>
        public virtual void OnPenetration(PenetrationParams penetration)
        {
        }

        /// <summary>
        /// Called after an elastic impact.
        /// </summary>
        /// <param name="recoil">The RecoilParams.</param>
        public virtual void OnRecoil(RecoilParams recoil)
        {
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Renders the particle.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}