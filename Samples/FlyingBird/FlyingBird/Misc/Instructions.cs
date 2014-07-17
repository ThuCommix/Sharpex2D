using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace FlyingBird.Misc
{
    public class Instructions
    {
        private const string Instruction = "TAP WITH YOUR MOUSE TO START THE GAME";
        private const string Instruction2 = "TAP WITH YOUR MOUSE TO RESET.";
        private readonly Font _font;
        private readonly Vector2 _position;
        private readonly Texture2D _texture;
        private bool _flag;
        private float _opacity = 1f;

        /// <summary>
        ///     Initializes a new Instructions class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        public Instructions(Texture2D texture)
        {
            _texture = texture;
            _position = new Vector2(291, 215);
            _font = new Font("Segoe UI", 11, TypefaceStyle.Regular);
        }

        /// <summary>
        ///     A value indicating whether the Instructions is visible.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        ///     Gets or sets the InstructionFlag.
        /// </summary>
        public bool InstructionFlag { set; get; }

        /// <summary>
        ///     Renders the object.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public void Render(RenderDevice renderer)
        {
            if (!Visible) return;

            if (!InstructionFlag)
            {
                renderer.DrawTexture(_texture, _position, _opacity);
                Vector2 dim = renderer.MeasureString(Instruction, _font);
                renderer.DrawString(Instruction, _font, new Vector2(_position.X - dim.X/2 + 28, _position.Y - 30),
                    Color.White);
            }
            else
            {
                renderer.DrawTexture(_texture, _position, _opacity);
                Vector2 dim = renderer.MeasureString(Instruction2, _font);
                renderer.DrawString(Instruction2, _font, new Vector2(_position.X - dim.X/2 + 28, _position.Y - 30),
                    Color.White);
            }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!_flag)
            {
                _opacity -= 0.02f;
                if (_opacity <= 0)
                {
                    _flag = true;
                    _opacity = 0;
                }
            }
            else
            {
                _opacity += 0.02f;
                if (_opacity >= 1)
                {
                    _flag = false;
                    _opacity = 1;
                }
            }
        }
    }
}