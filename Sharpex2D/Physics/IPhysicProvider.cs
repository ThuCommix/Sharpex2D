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
    [TestState(TestState.Tested)]
    [Obsolete("The old physic system will be removed in the future. Please use alternatives.")]
    public interface IPhysicProvider : IUpdateable, IComponent
    {
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