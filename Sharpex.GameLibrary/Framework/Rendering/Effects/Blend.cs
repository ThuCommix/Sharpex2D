using System;
using System.Drawing;
using System.Threading;
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
            if (!_issubscribed)
            {
                SGL.Components.Get<GameLoop>().Subscribe(this);
                _finished = false;
                Completed = false;
                _issubscribed = true;
            }
        }
        /// <summary>
        /// A value indicating whether the Effect is completed.
        /// </summary>
        public bool Completed { private set; get; }
        /// <summary>
        /// Gets the Callback action.
        /// </summary>
        public Action Callback { private set; get; }

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
            if (_finished)
            {
                SGL.Components.Get<GameLoop>().Unsubscribe(this);
                _issubscribed = false;
                Completed = true;
                if (Callback != null)
                {
                    // Invoke callback
                    new Thread(() => Callback.Invoke()).Start();
                }
            }
            else
            {
                var scalingPerSecond = 255/Duration * elapsed;
                if (_blendMode == BlendMode.FadeIn)
                {
                    if (_alpha - scalingPerSecond > 0)
                    {
                        _alpha -= scalingPerSecond;
                    }
                    else
                    {
                        _alpha = 0;
                        _finished = true;
                    }

                    _color.A = (byte) _alpha;
                }
                else
                {
                    if (_alpha + scalingPerSecond < 255)
                    {
                        _alpha += scalingPerSecond;
                    }
                    else
                    {
                        _alpha = 255;
                        _finished = true;
                    }
                    _color.A = (byte)_alpha;
                }
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
                new Math.Rectangle(-10, -10, SGL.GraphicsDevice.DisplayMode.Width + 60, SGL.GraphicsDevice.DisplayMode.Height + 60),
                _color);
        }

        #endregion

        /// <summary>
        /// Initializes a new Blend class.
        /// </summary>
        /// <param name="callback">The Callback Action.</param>
        /// <param name="blendMode">The BlendMode.</param>
        public Blend(Action callback, BlendMode blendMode)
        {
            var bmp = new Bitmap(100, 100);
            Graphics.FromImage(bmp).FillRectangle(new SolidBrush(Color.Transparent.ToWin32Color()), new Rectangle(0, 0, 100, 100));
            _overlay = new Texture {Texture2D = bmp};
            _color = Color.Black;
            _color.A = blendMode == BlendMode.FadeIn ? (byte)255 : (byte)0;
            _alpha = blendMode == BlendMode.FadeIn ? 255 : 0;
            _finished = false;
            Callback = callback;
            _blendMode = blendMode;
        }

        private readonly Texture _overlay;
        private bool _finished;
        private bool _issubscribed;
        private Color _color;
        private readonly BlendMode _blendMode;
        private float _alpha;
    }
}
