using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace FlyingBird.Misc
{
    public class AnimatedBackground
    {
        private Vector2 _position1;
        private Vector2 _position2;
        private readonly Texture2D _texture;

        /// <summary>
        ///     Initializes the AnimatedBackground class.
        /// </summary>
        /// <param name="bgTexture">The BackgroundTexture.</param>
        public AnimatedBackground(Texture2D bgTexture)
        {
            _texture = bgTexture;
            _position1 = new Vector2(0, 0);
            _position2 = new Vector2(638, 0);
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            _position1 = new Vector2(_position1.X - 0.5f, _position1.Y);
            _position2 = new Vector2(_position2.X - 0.5f, _position2.Y);

            if (_position1.X <= -640)
            {
                _position1.X = 638;
            }

            if (_position2.X <= -640)
            {
                _position2.X = 638;
            }
        }

        /// <summary>
        ///     Renders the object.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public void Render(RenderDevice renderer)
        {
            renderer.DrawTexture(_texture, _position1);
            renderer.DrawTexture(_texture, _position2);
        }
    }
}