using System;
using SharpexGL.Framework.Entities.Events;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// Initializes a new Entity class.
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
            _position = new Vector2(0, 0);
            EntityContainer = new EntityContainer();
            _componentsEnabled = true;
            RaiseEvents = true;
            _eventManager = SGL.Components.Get<EventManager>();
        }

        private Vector2 _position;
        private bool _componentsEnabled;
        private readonly EventManager _eventManager;

        /// <summary>
        /// Sets or gets the Position of the Entity.
        /// </summary>
        public Vector2 Position {
            get { return _position; }
            set
            {
                OnPositionChanged(value - _position);
                _position = value;
                IsDirty = true;
            }
        }
        /// <summary>
        /// Sets or gets the Id of the Entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the EntityContainer.
        /// </summary>
        public EntityContainer EntityContainer { private set; get; }

        /// <summary>
        /// A value indicating whether the Entity is dirty.
        /// </summary>
        public bool IsDirty { set; get; }

        /// <summary>
        /// A value indicating whether the Entity is destroyed.
        /// </summary>
        public bool IsDestroyed { private set; get; }

        /// <summary>
        /// A value indicating whether the Entity can raise events.
        /// </summary>
        public bool RaiseEvents { set; get; }

        /// <summary>
        /// Called, if the Position changed.
        /// </summary>
        /// <param name="delta">The Delta.</param>
        public virtual void OnPositionChanged(Vector2 delta)
        {
            _eventManager.Publish(new EntityPositionChangedEvent(delta));
        }
        /// <summary>
        /// Enables Container updates.
        /// </summary>
        public void EnableContainerUpdates()
        {
            _componentsEnabled = true;
        }
        /// <summary>
        /// Disbale Container updates.
        /// </summary>
        public void DisableContainerUpdates()
        {
            _componentsEnabled = false;
        }

        /// <summary>
        /// Destroys the Entity.
        /// </summary>
        public void Destroy()
        {
            IsDestroyed = true;
            _eventManager.Publish(new EntityDestroyedEvent(this));
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            if (_componentsEnabled)
            {
                foreach (var entity in EntityContainer.GetEntities())
                {
                    entity.Tick(elapsed);
                }
            }

            IsDirty = false;
        }

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public virtual void Render(IRenderer renderer, float elapsed)
        {
            if (_componentsEnabled)
            {
                foreach (var entity in EntityContainer.GetEntities())
                {
                    entity.Render(renderer, elapsed);
                }
            }
        }
    }
}
