using Sharpex2D;
using Sharpex2D.Entities;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Entities
{
    public class Player : Entity, IDynamicHitbox
    {
        /// <summary>
        /// Gets the Velocity.
        /// </summary>
        public const float Velocity = 0.2f;

        /// <summary>
        /// Initializes a new Player class.
        /// </summary>
        /// <param name="playerTexture">The PlayerTexture.</param>
        public Player(Texture2D playerTexture)
        {
            Sprite = new AnimatedSpriteSheet(playerTexture) {AutoUpdate = true};

            Sprite.Add(new Keyframe(new Rectangle(0, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(115, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(230, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(345, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(460, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(575, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(690, 0, 115, 69), 50));
            Sprite.Add(new Keyframe(new Rectangle(805, 0, 115, 69), 50));

            Position = new Vector2(0, 190);
            Health = 100;
        }

        /// <summary>
        /// Gets the Health.
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Get the SpriteSheet.
        /// </summary>
        public AnimatedSpriteSheet Sprite { private set; get; }

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
            if (Health - value < 0)
            {
                Health = 0;
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
            Sprite.Update(gameTime);
            Bounds = new Rectangle(Position, new Vector2(116, 69));
        }

        /// <summary>
        /// Renders the Player.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawTexture(Sprite, Position);
        }
    }
}