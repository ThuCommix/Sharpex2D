// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Diagnostics;
using Sharpex2D.Audio;
using Sharpex2D.Content;
using Sharpex2D.Debug;
using Sharpex2D.Debug.Logging;
using Sharpex2D.GameService;
using Sharpex2D.Input;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using Sharpex2D.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class SGL
    {
        /// <summary>
        /// Current Game Instance.
        /// </summary>
        internal static Game GameInstance;

        private static readonly Logger Logger;

        /// <summary>
        /// Initializes a new SGL class.
        /// </summary>
        static SGL()
        {
            State = EngineState.NotInitialized;
            Logger = LogManager.GetClassLogger();
        }

        /// <summary>
        /// Current GraphicsDevice.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// The current SpriteBatch.
        /// </summary>
        internal static SpriteBatch SpriteBatch { set; get; }

        /// <summary>
        /// Gets or sets the GraphicsManager.
        /// </summary>
        internal static GraphicsManager GraphicsManager { set; get; }

        /// <summary>
        /// Gets the Version of SGL.
        /// </summary>
        public static Version Version
        {
            get { return new Version(1, 3, 0); }
        }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public static EngineState State { private set; get; }

        /// <summary>
        /// ComponentManager Instance.
        /// </summary>
        public static ComponentManager Components { private set; get; }

        /// <summary>
        /// Initializes SGL.
        /// </summary>
        public static void Initialize()
        {
            Game gameInstance = InitializeHelper.GetGameClass();
            RenderTarget renderTarget;

            try
            {
                renderTarget = RenderTarget.Default;
            }
            catch (InvalidOperationException)
            {
                renderTarget = RenderTarget.Create();
            }

            var sglInitializer = new Configurator(gameInstance, renderTarget);

            Initialize(sglInitializer);
        }

        /// <summary>
        /// Initializes SGL.
        /// </summary>
        /// <param name="configurator">The Configurator.</param>
        public static void Initialize(IConfigurator configurator)
        {
            if (State != EngineState.NotInitialized)
            {
                return;
            }

            State = EngineState.Initializing;
            Components = new ComponentManager();
            GameInstance = configurator.GameInstance;
            Components.Add(configurator.RenderTarget);
            GraphicsDevice = new GraphicsDevice(configurator.RenderTarget)
            {
                BackBuffer = configurator.BackBuffer,
                ClearColor = Color.CornflowerBlue
            };
            configurator.RenderTarget.Window.Size = new Vector2(configurator.BackBuffer.Width,
                configurator.BackBuffer.Height);
            var gameLoop = new GameLoop {TargetTime = 1000/(float) configurator.TargetFrameRate};
            Components.Add(gameLoop);
            GameInstance.Input = new InputManager();
            GameInstance.Content = new ContentManager();
            GameInstance.SceneManager = new SceneManager();
            GraphicsDevice.RefreshRate = configurator.TargetFrameRate;
            Components.Add(GameInstance.Content);
            Components.Add(GraphicsDevice);
            Components.Add(GameInstance);
            Components.Add(GameInstance.SceneManager);
            Components.Add(GameInstance.Input);
            Components.Get<GameLoop>().Subscribe((IDrawable) GameInstance);
            Components.Get<GameLoop>().Subscribe((IUpdateable) GameInstance);
            Components.Get<GameLoop>().Subscribe(GameInstance.Input);

            //prepare game services
            var gameServices = new GameServiceContainer();

            gameServices.Add(new AchievementProvider());
            gameServices.Add(new Gamer());
            gameServices.Add(new LaunchParameters());

            GameInstance.GameServices = gameServices;

            Components.Add(new ExceptionHandler());

            EngineConfiguration engineConfiguration =
                GameInstance.OnInitialize(GameInstance.GameServices.GetService<LaunchParameters>());

            State = EngineState.Initialized;
            Run(engineConfiguration);
        }

        /// <summary>
        /// Runs SGL based on the specific initialized options.
        /// </summary>
        /// <param name="engineConfiguration">The EngineConfiguration.</param>
        private static void Run(EngineConfiguration engineConfiguration)
        {
            Components.Add(engineConfiguration);
            GraphicsManager = engineConfiguration.GraphicsManager;
            GameInstance.AudioManager = AudioManager.Instance;
            Components.Add(GameInstance.AudioManager);
            Components.Construct();
            Components.Get<GameLoop>().Start();

            Logger.Engine("SGL ({0}) is sucessfully running.", Version);

            State = EngineState.Running;
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            State = EngineState.NotInitialized;

            Components.Get<GameLoop>().Stop();
            GameInstance.OnUnload();
            GC.Collect();
            //Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        internal static void Restart(string parameters)
        {
            var gameProcess = new Process
            {
                StartInfo =
                {
                    FileName = Environment.GetCommandLineArgs()[0],
                    Arguments = parameters,
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory
                }
            };

            try
            {
                gameProcess.Start();
                Shutdown();
            }
            catch (Exception)
            {
                Logger.Engine("Failed to restart the process.");
            }
        }

        /// <summary>
        /// Queries a resource from the ContentManager content cache.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="assetname">The Asset (Path loaded with the ContentManager).</param>
        /// <returns>T.</returns>
        public static T QueryResource<T>(string assetname) where T : IContent
        {
            if (State != EngineState.Initialized && State != EngineState.Running)
            {
                throw new InvalidOperationException(
                    string.Format("SGL must be initialized in order to query any resource. Current state {0}", State));
            }

            T data;
            if (QueryComponents<ContentManager>().QueryCache<T>(assetname, out data))
            {
                return data;
            }

            throw new InvalidOperationException("The specified resource was not found.");
        }

        /// <summary>
        /// Queries the ComponentManager.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>T.</returns>
        public static T QueryComponents<T>() where T : IComponent
        {
            return Components.Get<T>();
        }
    }
}