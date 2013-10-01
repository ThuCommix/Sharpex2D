using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Sprites
{
    public class Animation
    {
        /// <summary>
        /// Initializes a new Animation class.
        /// </summary>
        /// <param name="sprite">The SpriteSheet.</param>
        /// <param name="duration">The Duration.</param>
        /// <param name="rectangle">The Spritedimension.</param>
        /// <param name="keyframes">The amount of KeyFrames.</param>
        public Animation(float duration, int keyframes, SpriteSheet sprite, Rectangle rectangle)
        {
            _sprite = sprite;
            _duration = duration;
            _timeElapsed = 0;
            _currentFrame = 0;
            KeyFrames = keyframes;
            _rect = rectangle;
            Texture = sprite.GetSprite(0, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }

        private readonly SpriteSheet _sprite;
        private readonly float _duration;
        private float _timeElapsed;
        private Rectangle _rect;
        private int _currentFrame;

        /// <summary>
        /// Gets the amount of KeyFrames.
        /// </summary>
        public int KeyFrames { get; private set; }

        /// <summary>
        /// Gets the current Texture.
        /// </summary>
        public Texture Texture { private set; get; }

        /// <summary>
        /// Updates the Animation class.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void OnTick(float elapsed)
        {
            _timeElapsed += elapsed;
            if (_timeElapsed > _duration)
            {
                _currentFrame++;
                if (_currentFrame > KeyFrames)
                {
                    _currentFrame = 0;
                }
                //Set texture
                Texture = _sprite.GetSprite(_currentFrame * (int)_rect.Width, (int) _rect.Y, (int) _rect.Width, (int) _rect.Height);
                //Reset time
                _timeElapsed = 0;
            }
        }
    }
}
