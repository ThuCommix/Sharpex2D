using Sharpex2D;
using Sharpex2D.Entities;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Entities
{
    public class Projectile : Entity, IDynamicHitbox
    {
        /// <summary>
        /// Gets the Velocity.
        /// </summary>
        public const float Velocity = 0.3f;

        /// <summary>
        /// Gets the AttackDamage.
        /// </summary>
        public const int AttackDamage = 4;

        /// <summary>
        /// Initializes a new Projectile class.
        /// </summary>
        /// <param name="projectileTexture">The ProjectileTexture.</param>
        public Projectile(Texture2D projectileTexture)
        {
            Texture = projectileTexture;
            Position = new Vector2(0);
            Bounds = new Rectangle(Position, new Vector2(46, 16));
        }

        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public Texture2D Texture { private set; get; }

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
        /// Updates the projectile.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + (Velocity*gameTime.ElapsedGameTime), Position.Y);
            Bounds = new Rectangle(Position, new Vector2(46, 16));
        }

        /// <summary>
        /// Renders the projectile.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawTexture(Texture, Position);
        }
    }
}