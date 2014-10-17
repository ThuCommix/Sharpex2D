using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class FadeableText : IDrawable, IUpdateable
    {
        private float _currentAlpha;
        private bool _flag;

        /// <summary>
        /// Initializes a new FadeableText.
        /// </summary>
        public FadeableText()
        {
            Text = "Fadeable Text1";
            Font = new Font("Segoe UI", 9, TypefaceStyle.Regular);
            Position = new Vector2(0, 0);
            FadeInVelocity = 1;
            FadeOutVelocity = 2;
        }

        /// <summary>
        /// Gets or sets the Font.
        /// </summary>
        public Font Font { set; get; }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// Gets or sets the FadeInVelocity.
        /// </summary>
        public float FadeInVelocity { set; get; }

        /// <summary>
        /// Gets or sets the FadeOutVelocity.
        /// </summary>
        public float FadeOutVelocity { set; get; }

        /// <summary>
        /// A value indicating whether the animation is complete.
        /// </summary>
        public bool AnimationComplete { private set; get; }

        /// <summary>
        /// A value indicating whether the fadeable text is fading out.
        /// </summary>
        public bool FadeOut
        {
            get { return !_flag; }
        }

        /// <summary>
        /// Draws the FadeableText.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (AnimationComplete) return;

            spriteBatch.DrawString(Text, Font, Position, Color.FromArgb((int) _currentAlpha, 255, 255, 255));
        }

        /// <summary>
        /// Updates the FadeableText.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (AnimationComplete) return;

            if (!_flag)
            {
                _currentAlpha += FadeInVelocity;
                if (_currentAlpha >= 255)
                {
                    _currentAlpha = 255;
                    _flag = true;
                }
            }
            else
            {
                _currentAlpha -= FadeOutVelocity;
                if (_currentAlpha <= 0)
                {
                    _currentAlpha = 0;
                    AnimationComplete = true;
                }
            }
        }
    }
}