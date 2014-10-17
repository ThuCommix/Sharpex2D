using Sharpex2D;
using Sharpex2D.Entities;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Entities
{
    public class Explosion : Entity
    {
        private bool _isVisible;

        /// <summary>
        /// Initializes a new Explosion class.
        /// </summary>
        /// <param name="explosionTexture">The ExplosionTexture.</param>
        public Explosion(Texture2D explosionTexture)
        {
            Sprite = new AnimatedSpriteSheet(explosionTexture) {AutoUpdate = true};

            Sprite.Add(new Keyframe(new Rectangle(0, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(133.5f, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(267, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(400.5f, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(534, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(667.5f, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(801, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(934.5f, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(1068, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(1201.5f, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(1335, 0, 133.5f, 134), 20));
            Sprite.Add(new Keyframe(new Rectangle(1468.5f, 0, 133.5f, 134), 20));

            Position = new Vector2(0, 0);
            _isVisible = true;
            RemainingLifeTime = 300;
        }

        /// <summary>
        /// Get the SpriteSheet.
        /// </summary>
        public AnimatedSpriteSheet Sprite { private set; get; }

        /// <summary>
        /// Gets the RemainingLifeTime.
        /// </summary>
        public float RemainingLifeTime { private set; get; }

        /// <summary>
        /// Updates the Player.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            if (!_isVisible) return;

            Sprite.Update(gameTime);
            RemainingLifeTime -= gameTime.ElapsedGameTime;
            if (RemainingLifeTime < 0)
            {
                RemainingLifeTime = 0;
                _isVisible = false;
            }
        }

        /// <summary>
        /// Draws the Player.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_isVisible) return;

            spriteBatch.DrawTexture(Sprite, Position);
        }
    }
}