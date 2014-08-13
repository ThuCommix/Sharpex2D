using Sharpex2D;
using Sharpex2D.Entities;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Entities
{
    public class Enemy : Entity, IDynamicHitbox
    {
        /// <summary>
        /// Gets the AttackDamage.
        /// </summary>
        public const int AttackDamage = 20;

        private Rectangle _healthBarGreen;
        private Rectangle _healthBarRed;
        private bool _isVisible;
        private int _maximumHealth;

        /// <summary>
        /// Initializes a new Enemy class.
        /// </summary>
        /// <param name="enemyTexture">The EnemyTexture.</param>
        public Enemy(Texture2D enemyTexture)
        {
            Sprite = new AnimatedSpriteSheet(enemyTexture) {AutoUpdate = true};

            Sprite.Add(new Keyframe(new Rectangle(0, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(47, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(94, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(141, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(188, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(235, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(282, 0, 47, 61), 30));
            Sprite.Add(new Keyframe(new Rectangle(329, 0, 47, 61), 30));

            Position = new Vector2(0, 0);
            Health = 100;
            MaximumHealth = Health;
            Velocity = 0.1f;
            _isVisible = true;
            EnableHPBar = true;
        }

        /// <summary>
        /// Gets the Health.
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Gets or sets the MaximumHealth.
        /// </summary>
        public int MaximumHealth
        {
            get { return _maximumHealth; }
            set
            {
                _maximumHealth = value;
                Health = value;
            }
        }

        /// <summary>
        /// Get the SpriteSheet.
        /// </summary>
        public AnimatedSpriteSheet Sprite { private set; get; }

        /// <summary>
        /// Gets or sets the Velocity.
        /// </summary>
        public float Velocity { set; get; }

        /// <summary>
        /// A value indicating whether hp bars should be activated.
        /// </summary>
        public bool EnableHPBar { set; get; }

        /// <summary>
        /// Gets the Bounds.
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// A value indicating whether the hitbox intersects with another.
        /// </summary>
        /// <param name="dynamicHitbox">The other DynamicHitbox.</param>
        /// <returns>True if intersecting.</returns>
        public bool IntersectsWith(IDynamicHitbox dynamicHitbox)
        {
            return Bounds.Intersects(dynamicHitbox.Bounds);
        }

        /// <summary>
        /// Damages the player.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void Damage(int value)
        {
            if (Health - value <= 0)
            {
                Health = 0;
                _isVisible = false;
            }
            else
            {
                Health -= value;
            }
        }

        /// <summary>
        /// Updates the Player.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            if (!_isVisible) return;

            Sprite.Update(gameTime);
            Bounds = new Rectangle(Position, new Vector2(47, 61));
            _healthBarRed = new Rectangle(Position.X, Position.Y - 10, 38, 2);
            _healthBarGreen = new Rectangle(Position.X, Position.Y - 10, 38f*Health/MaximumHealth, 2);
        }

        /// <summary>
        /// Renders the Player.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            if (!_isVisible) return;

            renderer.DrawTexture(Sprite, Position);

            //healthbar
            if (!EnableHPBar) return;

            renderer.FillRectangle(Color.Red, _healthBarRed);
            renderer.FillRectangle(Color.Green, _healthBarGreen);
        }
    }
}