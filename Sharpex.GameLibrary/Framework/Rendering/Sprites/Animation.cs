using SharpexGL.Framework.Content;
using SharpexGL.Framework.Content.Factory;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Sprites
{
    public class Animation : IContent
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
            Rect = rectangle;
            Texture = sprite.GetSprite(0, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }

        private readonly SpriteSheet _sprite;
        private readonly float _duration;
        private float _timeElapsed;
        internal Rectangle Rect;
        private int _currentFrame;
        /// <summary>
        /// Sets or gets the Factory.
        /// </summary>
        public static AnimationFactory Factory { private set; get; }
        /// <summary>
        /// Gets the duration of a single keyframe.
        /// </summary>
        public float Duration
        {
            get { return _duration; }
        }
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
                Texture = _sprite.GetSprite(_currentFrame * (int)Rect.Width, (int) Rect.Y, (int) Rect.Width, (int) Rect.Height);
                //Reset time
                _timeElapsed = 0;
            }
        }

        static Animation()
        {
            Factory = new AnimationFactory();
        }
    }
}
