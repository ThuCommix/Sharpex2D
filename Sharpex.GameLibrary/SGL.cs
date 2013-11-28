using SharpexGL.Framework.Components;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Exceptions;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Implementation;
using SharpexGL.Framework.Input;
using SharpexGL.Framework.Media.Sound;
using SharpexGL.Framework.Rendering;
using SharpexGL.Framework.Rendering.Scene;
using SharpexGL.Framework.Surface;

namespace SharpexGL
{
    public static class SGL
    {
        #region Private Fields
        /// <summary>
        /// Current Game Instance.
        /// </summary>
        private static Game _gameInstance;
        /// <summary>
        /// Current GraphicsDevice.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice
        {
            get;
            set;
        }
        /// <summary>
        /// A value indicating whether SGL is running.
        /// </summary>
        private static bool _isRunning;
        /// <summary>
        /// The Current Renderer.
        /// </summary>
        internal static IRenderer CurrentRenderer { set; get; }
        #endregion

        #region Public Fields
        /// <summary>
        /// Gets the Version of SGL.
        /// </summary>
        public static string Version { get { return "0.1.537"; } }
        /// <summary>
        /// Determines, if SGL is initialized.
        /// </summary>
        public static bool IsInitialized { set; get; }
        /// <summary>
        /// ComponentManager Instance.
        /// </summary>
        public static ComponentManager Components { private set; get; }
        /// <summary>
        /// ImplementationManager Instance.
        /// </summary>
        public static ImplementationManager Implementations { private set; get; }
        #endregion

        #region ctor
        /// <summary>
        /// Initializes SGL.
        /// </summary>
        /// <param name="initializer">The Initializer.</param>
        public static void Initialize(SGLInitializer initializer)
        {
            if (IsInitialized) return;
            IsInitialized = true;
            Components = new ComponentManager();
            Implementations = new ImplementationManager();
            _gameInstance = initializer.GameInstance;
            var renderTarget = new RenderTarget(initializer.TargetHandle);
            Components.AddComponent(renderTarget);
            renderTarget.SurfaceControl.SetSize(initializer.Width, initializer.Height);
            initializer.GameInstance.Input = new InputManager(renderTarget.Handle);
            GraphicsDevice = new GraphicsDevice(renderTarget)
            {
                DisplayMode = new DisplayMode(initializer.Width, initializer.Height)
            };
            initializer.GameInstance.Content = new ContentManager();
            initializer.GameInstance.SceneManager = new SceneManager();
            Components.AddComponent(Implementations);
            Components.AddComponent(initializer.GameInstance);
            Components.AddComponent(GraphicsDevice);
            Components.AddComponent(initializer.GameInstance.SceneManager);
            Components.AddComponent(initializer.GameInstance.Input);
            Components.AddComponent(initializer.GameInstance.Content);
            Components.AddComponent(new EventManager());
            Components.AddComponent(new GameLoop
                {
                    TargetFramesPerSecond = initializer.TargetFramesPerSecond
                });
            Components.Get<GameLoop>().Subscribe(_gameInstance);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs SGL based on the specific initialized options.
        /// </summary>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        /// <param name="soundInitializer">The SoundInitializer</param>
        public static void Run(IRenderer graphicRenderer, ISoundInitializer soundInitializer)
        {
            if (!IsInitialized)
                throw new SGLNotInitializedException("SGL must be initialized in the first place.");

            if (_isRunning) return;

            _isRunning = true;
            CurrentRenderer = graphicRenderer;
            CurrentRenderer.GraphicsDevice = GraphicsDevice;
            _gameInstance.SoundManager = new SoundManager(soundInitializer);
            Components.AddComponent(graphicRenderer);
            Components.AddComponent(_gameInstance.SoundManager);
            Components.Construct();
            _gameInstance.OnInitialize();
            _gameInstance.OnLoadContent();
            Components.Get<GameLoop>().Start();
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        public static void Shutdown()
        {
            Components.Get<GameLoop>().Stop();
            _gameInstance.OnUnload();
            _gameInstance.OnClose();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        #endregion
    }
}
