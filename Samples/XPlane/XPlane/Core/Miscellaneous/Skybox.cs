using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class Skybox : IGameComponent
    {
        private readonly Texture2D _background;
        private readonly Vector2 _backgroundPosition;
        private readonly Texture2D _layer1;
        private readonly Texture2D _layer2;

        private Vector2 _layer1Position1;

        private Vector2 _layer1Position2;

        private Vector2 _layer1Position3;
        private Vector2 _layer2Position1;
        private Vector2 _layer2Position2;
        private Vector2 _layer2Position3;

        /// <summary>
        /// Initializes a new Skybox class.
        /// </summary>
        /// <param name="skyTextures">The SkyTextures.</param>
        public Skybox(Texture2D[] skyTextures)
        {
            //Skybox supports 3 layers, background, layer 1, layer 2

            _background = skyTextures[0];
            _layer1 = skyTextures[1];
            _layer2 = skyTextures[2];

            _backgroundPosition = new Vector2(0, 0);
            _layer1Position1 = new Vector2(0, 0);
            _layer2Position1 = new Vector2(0, 0);

            _layer1Position2 = new Vector2(800, 0);
            _layer2Position2 = new Vector2(800, 0);

            _layer1Position3 = new Vector2(1600, 0);
            _layer2Position3 = new Vector2(1600, 0);

            IsActive = true;
        }

        public bool IsActive { set; get; }

        /// <summary>
        /// Renders the Skybox.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawTexture(_background, _backgroundPosition);

            renderer.DrawTexture(_layer1, _layer1Position1);
            renderer.DrawTexture(_layer2, _layer2Position1);

            renderer.DrawTexture(_layer1, _layer1Position2);
            renderer.DrawTexture(_layer2, _layer2Position2);

            //renderer.DrawTexture(_layer1, _layer1Position3);
            renderer.DrawTexture(_layer2, _layer2Position3);
        }

        /// <summary>
        /// Updates the Skybox.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            _layer1Position1 = new Vector2(_layer1Position1.X - 0.5f, 0);
            _layer1Position2 = new Vector2(_layer1Position2.X - 0.5f, 0);
            //_layer1Position3 = new Vector2(_layer1Position3.X - 0.5f, 0);

            _layer2Position1 = new Vector2(_layer2Position1.X - 1, 0);
            _layer2Position2 = new Vector2(_layer2Position2.X - 1, 0);
            _layer2Position3 = new Vector2(_layer2Position3.X - 1, 0);

            if (_layer1Position1.X < -800)
            {
                _layer1Position1 = new Vector2(799.5f, 0);
            }

            if (_layer1Position2.X < -800)
            {
                _layer1Position2 = new Vector2(799.5f, 0);
            }

            /*if (_layer1Position3.X < -800)
            {
                _layer1Position3 = new Vector2(799.5f, 0);
            }*/

            if (_layer2Position1.X < -800)
            {
                _layer2Position1 = new Vector2(799, 0);
            }

            if (_layer2Position2.X < -800)
            {
                _layer2Position2 = new Vector2(799, 0);
            }

            if (_layer2Position3.X < -800)
            {
                _layer2Position3 = new Vector2(799, 0);
            }
        }

        /// <summary>
        /// Gets the Order.
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}