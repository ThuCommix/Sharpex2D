using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Content;
using Sharpex2D.Input;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using XPlane.Core.Miscellaneous;
using XPlane.Core.UI;

namespace XPlane.Core.Scenes
{
    public class GameScene : Scene
    {
        private const float InputDelay = 100;
        private BlackBlend _blackBlend;
        private float _currentDelay;
        private DebugDisplay _debugDisplay;
        private EntityComposer _entityComposer;
        private Minimap _minimap;
        private Scoreboard _scoreBoard;
        private Skybox _skyBox;
        private bool _achievementsOpen;
        private AchievementControl _achievementControl;

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
            if (_entityComposer.GameOver)
            {
                _blackBlend.IsEnabled = true;
                if (_blackBlend.IsCompleted)
                {
                    var endScene = SGL.QueryComponents<SceneManager>().Get<EndScene>();
                    endScene.Score = _entityComposer.Score;
                    endScene.AchievementManager = _entityComposer.AchievementManager;
                    SGL.QueryComponents<SceneManager>().ActiveScene = endScene;
                }
            }
            else
            {
                HandleInput(gameTime);
                if (!_achievementsOpen)
                {
                    _skyBox.Update(gameTime);
                    _entityComposer.Update(gameTime);
                    _scoreBoard.CurrentHealth = _entityComposer.Player.Health;
                    _scoreBoard.CurrentScore = _entityComposer.Score;
                    _minimap.Update(gameTime);
                    _debugDisplay.Update(gameTime);
                }
            }

            _blackBlend.Update(gameTime);
            _achievementControl.Visible = _achievementsOpen;
            UIManager.Update(gameTime);
        }

        /// <summary>
        /// Renders the scene.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            _skyBox.Render(renderer, gameTime);
            _entityComposer.Render(renderer, gameTime);
            if (!_debugDisplay.Visible)
            {
                _scoreBoard.Render(renderer, gameTime);
            }
            _minimap.Render(renderer, gameTime);
            _debugDisplay.Render(renderer, gameTime);
            _blackBlend.Render(renderer, gameTime);
            UIManager.Render(renderer, gameTime);
        }

        /// <summary>
        /// Initializes the Scene.
        /// </summary>
        public override void Initialize()
        {
            Input = SGL.QueryComponents<InputManager>();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The Content.</param>
        public override void LoadContent(ContentManager content)
        {
            _skyBox =
                new Skybox(new[]
                {
                    content.Load<Texture2D>("mainbackground.png"), content.Load<Texture2D>("bgLayer1.png"),
                    content.Load<Texture2D>("bgLayer2.png")
                });
        }

        /// <summary>
        /// Called if the scene is activated.
        /// </summary>
        public override void OnSceneActivated()
        {
            _entityComposer = new EntityComposer();
            _scoreBoard = new Scoreboard();
            _debugDisplay = new DebugDisplay(_entityComposer) {Visible = false};
            _minimap = new Minimap(_entityComposer);
            _blackBlend = new BlackBlend {FadeIn = true};
            _achievementControl = new AchievementControl(UIManager);
            _achievementsOpen = false;
            _achievementControl.AchievementManager = _entityComposer.AchievementManager;

#if AUDIO_ENABLED
    //AudioManager.Instance.Sound.Play(SGL.QueryResource<Sound>("gameMusic.mp3"), PlayMode.Loop);
#endif
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void HandleInput(GameTime gameTime)
        {
            if (_currentDelay > 0)
            {
                _currentDelay -= gameTime.ElapsedGameTime;
            }

            if (_currentDelay <= 0)
            {
                if (Input.Keyboard.GetState().IsKeyDown(Keys.F1))
                {
                    _debugDisplay.Visible = !_debugDisplay.Visible;
                    _currentDelay = InputDelay;
                }

                if (Input.Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    _minimap.Visible = !_minimap.Visible;
                    _currentDelay = InputDelay;
                }

                if (Input.Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    _entityComposer.EnableHPBars = !_entityComposer.EnableHPBars;
                    _currentDelay = InputDelay;
                }

                if (Input.Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    _achievementsOpen = !_achievementsOpen;
                    _currentDelay = InputDelay;
                }
            }
        }
    }
}