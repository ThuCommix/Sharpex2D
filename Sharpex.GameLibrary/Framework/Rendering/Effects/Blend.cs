using System;
using System.Drawing;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;

namespace SharpexGL.Framework.Rendering.Effects
{
    public class Blend : IEffect, IGameHandler
    {
        #region IEffect Implementation

        /// <summary>
        /// Gets the Guid-Identifer.
        /// </summary>
        public Guid Guid { get { return new Guid("199266BF-94EA-4B1C-BF2B-117B3B13FCAB"); } }
        /// <summary>
        /// Sets or gets the Duration.
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// Starts the Effect.
        /// </summary>
        public void Start()
        {
            if (!issubscribed)
            {
                SGL.Components.Get<GameLoop>().Subscribe(this);
                issubscribed = true;
            }
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        /// Constructs the Component
        /// </summary>
        public void Construct()
        {

        }

        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            if (finished)
            {
                SGL.Components.Get<GameLoop>().Unsubscribe(this);
                issubscribed = false;
            }
            else
            {
                //logic here
            }
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            renderer.DrawTexture(_overlay,
                new Math.Rectangle(0, 0, SGL.GraphicsDevice.DisplayMode.Width, SGL.GraphicsDevice.DisplayMode.Height),
                _color);
        }

        #endregion

        /// <summary>
        /// Initializes a new Blend class.
        /// </summary>
        public Blend()
        {
            var bmp = new Bitmap(100, 100);
            Graphics.FromImage(bmp).FillRectangle(new SolidBrush(Color.Transparent.ToWin32Color()), new Rectangle(0, 0, 100, 100));
            _overlay = new Texture {Texture2D = bmp};
            _color = Color.Black;
            _color.A = 255;
            _totalElapsed = 0f;
        }

        private readonly Texture _overlay;
        private bool finished;
        private bool issubscribed;
        private Color _color;
        private float _totalElapsed;
    }
}
