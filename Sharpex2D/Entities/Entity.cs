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
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace Sharpex2D.Entities
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public abstract class Entity
    {
        /// <summary>
        ///     EntityDestroyedEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void EntityDestroyedEventHandler(object sender, EventArgs e);

        /// <summary>
        ///     EntityPositionEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void EntityPositionEventHandler(object sender, EntityPositionEventArgs e);

        private bool _componentsEnabled;
        private Vector2 _position;

        /// <summary>
        ///     Initializes a new Entity class.
        /// </summary>
        protected Entity()
        {
            Id = 0;
            _position = new Vector2(0, 0);
            Entities = new List<Entity>();
            _componentsEnabled = true;
            RaiseEvents = true;
        }

        /// <summary>
        ///     Sets or gets the Position of the Entity.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                OnPositionChanged(value - _position);
                _position = value;
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets or gets the Id of the Entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets the EntityContainer.
        /// </summary>
        public List<Entity> Entities { private set; get; }

        /// <summary>
        ///     A value indicating whether the Entity is dirty.
        /// </summary>
        public bool IsDirty { set; get; }

        /// <summary>
        ///     A value indicating whether the Entity is destroyed.
        /// </summary>
        public bool IsDestroyed { private set; get; }

        /// <summary>
        ///     A value indicating whether the Entity can raise events.
        /// </summary>
        public bool RaiseEvents { set; get; }

        /// <summary>
        ///     PositionChanged event.
        /// </summary>
        public event EntityPositionEventHandler PositionChanged;

        /// <summary>
        ///     Destroyed event.
        /// </summary>
        public event EntityDestroyedEventHandler Destroyed;

        /// <summary>
        ///     Called, if the Position changed.
        /// </summary>
        /// <param name="delta">The Delta.</param>
        public virtual void OnPositionChanged(Vector2 delta)
        {
            if (RaiseEvents)
            {
                if (PositionChanged != null)
                {
                    PositionChanged(this, new EntityPositionEventArgs(delta));
                }
            }
        }

        /// <summary>
        ///     Enables Container updates.
        /// </summary>
        public void EnableContainerUpdates()
        {
            _componentsEnabled = true;
        }

        /// <summary>
        ///     Disbale Container updates.
        /// </summary>
        public void DisableContainerUpdates()
        {
            _componentsEnabled = false;
        }

        /// <summary>
        ///     Destroys the Entity.
        /// </summary>
        public void Destroy()
        {
            IsDestroyed = true;
            if (RaiseEvents)
            {
                if (Destroyed != null)
                {
                    Destroyed(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (_componentsEnabled)
            {
                foreach (Entity entity in Entities)
                {
                    entity.Update(gameTime);
                }
            }

            IsDirty = false;
        }

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Render(RenderDevice renderer, GameTime gameTime)
        {
            if (_componentsEnabled)
            {
                foreach (Entity entity in Entities)
                {
                    entity.Render(renderer, gameTime);
                }
            }
        }
    }
}