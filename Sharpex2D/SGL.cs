using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Storage;
using Sharpex2D.Framework.Debug;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Game.Services.Achievements;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Implementation;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Media;
using Sharpex2D.Framework.Media.Sound;
using Sharpex2D.Framework.Media.Video;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.Scene;
using Sharpex2D.Framework.UI;

namespace Sharpex2D
{
    public static class SGL
    {
        /// <summary>
        /// Current Game Instance.
        /// </summary>
        private static Game _gameInstance;

        /// <summary>
        /// Current GraphicsDevice.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// The Current Renderer.
        /// </summary>
        internal static IRenderer CurrentRenderer { set; get; }

        /// <summary>
        /// Gets the Version of SGL.
        /// </summary>
        public static string Version
        {
            get { return "0.2.200"; }
        }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public static SGLState State { private set; get; }

        /// <summary>
        /// ComponentManager Instance.
        /// </summary>
        public static ComponentManager Components { private set; get; }

        /// <summary>
        /// ImplementationManager Instance.
        /// </summary>
        public static ImplementationManager Implementations { private set; get; }

        /// <summary>
        /// Initializes a new SGL class.
        /// </summary>
        static SGL()
        {
            State = SGLState.NotInitialized;
        }

        /// <summary>
        /// Initializes SGL.
        /// </summary>
        /// <param name="initializer">The Initializer.</param>
        public static void Initialize(SGLInitializer initializer)
        {
            if (State != SGLState.NotInitialized)
            {
                return;
            }
            State = SGLState.Initializing;
            Components = new ComponentManager();
            Implementations = new ImplementationManager();
            _gameInstance = initializer.GameInstance;
            Components.AddComponent(initializer.RenderTarget);
            Components.AddComponent(new EventManager());
            initializer.RenderTarget.SurfaceControl.SetSize(initializer.Width, initializer.Height);
            initializer.GameInstance.Input = new InputManager(initializer.RenderTarget.Handle);
            GraphicsDevice = new GraphicsDevice(initializer.RenderTarget)
            {
                DisplayMode = new DisplayMode(initializer.Width, initializer.Height)
            };
            initializer.GameInstance.Content = new ContentManager();
            initializer.GameInstance.SceneManager = new SceneManager();
            initializer.GameLoop.TargetFramesPerSecond = initializer.TargetFramesPerSecond;
            Components.AddComponent(initializer.GameLoop);
            Components.AddComponent(Implementations);
            Components.AddComponent(initializer.GameInstance.Content);
            Components.AddComponent(GraphicsDevice);
            Components.AddComponent(initializer.GameInstance);
            Components.AddComponent(initializer.GameInstance.SceneManager);
            Components.AddComponent(initializer.GameInstance.Input);
            Components.Get<IGameLoop>().Subscribe(_gameInstance);

            //prepare game services

            Components.AddComponent(new AchievementProvider());
            Components.AddComponent(new ExceptionHandler());
            Components.AddComponent(new ContentStorage());

            State = SGLState.Initialized;
        }

        /// <summary>
        /// Runs SGL based on the specific initialized options.
        /// </summary>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        /// <param name="mediaInitializer">The MediaInitializer.</param>
        public static void Run(IRenderer graphicRenderer, MediaInitializer mediaInitializer)
        {
            if (State != SGLState.Initialized)
                throw new InvalidOperationException("SGL must be initialized in the first place.");

            if (State == SGLState.Running) return;

            CurrentRenderer = graphicRenderer;
            CurrentRenderer.GraphicsDevice = GraphicsDevice;
            _gameInstance.SoundManager = new SoundManager(mediaInitializer.SoundInitializer);
            _gameInstance.VideoManager = new VideoManager(mediaInitializer.VideoInitializer);
            Components.AddComponent(graphicRenderer);
            Components.AddComponent(_gameInstance.SoundManager);
            Components.AddComponent(_gameInstance.VideoManager);
            Components.Construct();
            _gameInstance.OnInitialize();
            _gameInstance.OnLoadContent();
            Components.Get<IGameLoop>().Start();

            Log.Next("SGL is sucessfully running.", LogLevel.Info, LogMode.StandardOut);

            State = SGLState.Running;
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            Components.Get<IGameLoop>().Stop();
            _gameInstance.OnUnload();
            _gameInstance.OnClose();
            GC.Collect();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
