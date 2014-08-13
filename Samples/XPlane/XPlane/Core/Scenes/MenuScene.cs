using System.Windows.Forms;
using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using XPlane.Core.Audio;
using XPlane.Core.Miscellaneous;
using XPlane.Core.UI;
using MouseButtons = Sharpex2D.Input.MouseButtons;

namespace XPlane.Core.Scenes
{
    public class MenuScene : Scene
    {
        private BlackBlend _blackBlend;
        private Texture2D _menuBackground;

        private MenuButton _menuButton1;
        private MenuButton _menuButton3;
        private Vector2 _menuPosition;

        /// <summary>
        /// Updates the scene.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _blackBlend.Update(gameTime);
            UIManager.Update(gameTime);

            if (_menuButton1.IsMouseDown(MouseButtons.Left))
            {
                SGL.QueryComponents<SceneManager>().ActiveScene = SGL.QueryComponents<SceneManager>().Get<GameScene>();
            }

            if (_menuButton3.IsMouseDown(MouseButtons.Left))
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Renders the Scene.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawTexture(_menuBackground, _menuPosition);
            UIManager.Render(renderer, gameTime);
            _blackBlend.Render(renderer, gameTime);
        }

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public override void Initialize()
        {
            _menuPosition = new Vector2(0, 0);

            _menuButton1 = new MenuButton(UIManager)
            {
                Text = "Start new game",
                Position = new Vector2(250, 200),
            };
            _menuButton3 = new MenuButton(UIManager)
            {
                Text = "Exit",
                Position = new Vector2(250, 240),
            };
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public override void LoadContent(ContentManager content)
        {
            _menuBackground = content.Load<Texture2D>("alternativeMenu.png");
        }

        /// <summary>
        /// Called if the scene is activated.
        /// </summary>
        public override void OnSceneActivated()
        {
            _blackBlend = new BlackBlend {IsEnabled = true, FadeIn = false};

#if AUDIO_ENABLED
            AudioManager.Instance.Sound.Play(SGL.QueryResource<Sound>("menuMusic.mp3"), PlayMode.Loop);
#endif
        }
    }
}