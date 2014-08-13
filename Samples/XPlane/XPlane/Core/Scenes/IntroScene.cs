using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using XPlane.Core.Miscellaneous;

namespace XPlane.Core.Scenes
{
    public class IntroScene : Scene
    {
        private BlackBlend _blackBlend;
        private FadeableText _fadeableText1;
        private FadeableText _fadeableText2;
        private FadeableText _fadeableText3;
        private Font _header;
        private Skybox _skyBox;
        private Font _subHeader;

        /// <summary>
        /// Updates the scene.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _skyBox.Update(gameTime);
            _fadeableText1.Update(gameTime);
            if (_fadeableText1.AnimationComplete)
            {
                _fadeableText2.Update(gameTime);
                if (_fadeableText2.AnimationComplete)
                {
                    _fadeableText3.Update(gameTime);
                    if (_fadeableText3.AnimationComplete)
                    {
                        _blackBlend.Update(gameTime);
                        if (_blackBlend.IsCompleted)
                        {
                            SGL.QueryComponents<SceneManager>().ActiveScene =
                                SGL.QueryComponents<SceneManager>().Get<MenuScene>();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renders the Scene.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            _skyBox.Render(renderer, gameTime);
            _fadeableText1.Render(renderer, gameTime);
            if (_fadeableText1.AnimationComplete)
            {
                _fadeableText2.Render(renderer, gameTime);
                if (_fadeableText2.AnimationComplete)
                {
                    _fadeableText3.Render(renderer, gameTime);
                    if (_fadeableText3.AnimationComplete)
                    {
                        _blackBlend.Render(renderer, gameTime);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public override void Initialize()
        {
            _blackBlend = new BlackBlend {FadeIn = true, IsEnabled = true};
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public override void LoadContent(ContentManager content)
        {
            _skyBox =
                new Skybox(new[]
                {
                    content.Load<Texture2D>("mainbackground.png"), content.Load<Texture2D>("bgLayer1.png"),
                    content.Load<Texture2D>("bgLayer2.png")
                });
            _header = new Font("Segoe UI", 25, TypefaceStyle.Bold);
            _subHeader = new Font("Segoe UI", 40, TypefaceStyle.Bold);
            _fadeableText1 = new FadeableText
            {
                Font = _header,
                Text = "ThuCommix presents",
                Position = new Vector2(270, 200),
                FadeInVelocity = 2,
                FadeOutVelocity = 3
            };
            _fadeableText2 = new FadeableText
            {
                Font = _header,
                Text = "a game powered by Sharpex2D",
                Position = new Vector2(220, 200),
                FadeInVelocity = 2,
                FadeOutVelocity = 3
            };
            _fadeableText3 = new FadeableText
            {
                Font = _subHeader,
                Text = "XPlane",
                Position = new Vector2(330, 200),
                FadeInVelocity = 2,
                FadeOutVelocity = 2
            };
        }
    }
}