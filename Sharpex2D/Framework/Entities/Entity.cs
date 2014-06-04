using Sharpex2D.Framework.Entities.Events;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Entities
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class Entity
    {
        private readonly EventManager _eventManager;
        private bool _componentsEnabled;
        private Vector2 _position;

        /// <summary>
        ///     Initializes a new Entity class.
        /// </summary>
        protected Entity()
        {
            Id = 0;
            _position = new Vector2(0, 0);
            EntityContainer = new EntityContainer();
            _componentsEnabled = true;
            RaiseEvents = true;
            _eventManager = SGL.Components.Get<EventManager>();
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
        public EntityContainer EntityContainer { private set; get; }

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
        ///     Called, if the Position changed.
        /// </summary>
        /// <param name="delta">The Delta.</param>
        public virtual void OnPositionChanged(Vector2 delta)
        {
            if (RaiseEvents)
            {
                _eventManager.Publish(new EntityPositionChangedEvent(delta));
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
                _eventManager.Publish(new EntityDestroyedEvent(this));
            }
        }

        /// <summary>
        ///     Processes a Tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Tick(GameTime gameTime)
        {
            if (_componentsEnabled)
            {
                foreach (Entity entity in EntityContainer.GetEntities())
                {
                    entity.Tick(gameTime);
                }
            }

            IsDirty = false;
        }

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Render(IRenderer renderer, GameTime gameTime)
        {
            if (_componentsEnabled)
            {
                foreach (Entity entity in EntityContainer.GetEntities())
                {
                    entity.Render(renderer, gameTime);
                }
            }
        }
    }
}