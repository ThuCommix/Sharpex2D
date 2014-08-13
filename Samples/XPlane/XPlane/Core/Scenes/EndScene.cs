using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Input;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using XPlane.Core.Miscellaneous;

namespace XPlane.Core.Scenes
{
    public class EndScene : Scene
    {
        private Texture2D _background;
        private Vector2 _backgroundPosition;
        private BlackBlend _blackBlend;
        private Font _font;
        private Font _font2;

        /// <summary>
        /// Gets or sets the reached Score.
        /// </summary>
        public int Score { set; get; }

        /// <summary>
        /// Gets or sets the InputManager.
        /// </summary>
        private InputManager Input { set; get; }

        /// <summary>
        /// Updates the scene.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _blackBlend.Update(gameTime);

            if (Input.Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SGL.QueryComponents<SceneManager>().ActiveScene = SGL.QueryComponents<SceneManager>().Get<MenuScene>();
            }
        }

        /// <summary>
        /// Renders the Scene.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawTexture(_background, _backgroundPosition);
            Vector2 dim = renderer.MeasureString(string.Format("score: {0}", Score), _font);
            Vector2 dim2 = renderer.MeasureString("Press {Enter} to continue", _font2);
            renderer.DrawString(string.Format("score: {0}", Score), _font, new Vector2(400 - dim.X/2, 300), Color.White);
            renderer.DrawString("Press {Enter} to continue", _font2, new Vector2(400 - dim2.X/2, 420), Color.White);
            _blackBlend.Render(renderer, gameTime);
        }

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public override void Initialize()
        {
            _backgroundPosition = new Vector2(0);
            _font = new Font("Segoe UI", 30, TypefaceStyle.Bold);
            _font2 = new Font("Segoe UI", 13, TypefaceStyle.Regular);
            Input = SGL.QueryComponents<InputManager>();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("endMenu.png");
        }

        /// <summary>
        /// Called if the scene is activated.
        /// </summary>
        public override void OnSceneActivated()
        {
            _blackBlend = new BlackBlend {FadeIn = false, IsEnabled = true};
        }
    }
}