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
using Sharpex2D.Physics.Controllers;

namespace Sharpex2D.Physics
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class World : IUpdateable, IDisposable
    {
        #region IDisposable Implementation

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">The Disposing State.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Bodies.Clear();
                Bodies = null;
                Controllers.Clear();
                Controllers = null;

                SGL.QueryComponents<IGameLoop>().Unsubscribe(this);
            }
        }

        #endregion

        /// <summary>
        ///     Initializes a new World class.
        /// </summary>
        public World()
        {
            Bodies = new List<RigidBody>();
            Controllers = new List<Controller>();

            if (SGL.State == EngineState.NotInitialized || SGL.State == EngineState.Initializing)
            {
                throw new InvalidOperationException("SGL not initialized.");
            }

            SGL.QueryComponents<IGameLoop>().Subscribe(this);
        }

        /// <summary>
        ///     Gets the Bodies.
        /// </summary>
        public List<RigidBody> Bodies { private set; get; }

        /// <summary>
        ///     Gets the Controllers.
        /// </summary>
        public List<Controller> Controllers { private set; get; }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            //update controllers

            for (int i = 0; i <= Controllers.Count - 1; i++)
            {
                if (Controllers[i].IsEnabled)
                {
                    Controllers[i].Update(this, 2);
                }
            }
        }
    }
}