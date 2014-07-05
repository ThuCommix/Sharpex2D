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
using Sharpex2D.Framework.Audio;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Debug;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Game.Services;
using Sharpex2D.Framework.Game.Services.Achievements;
using Sharpex2D.Framework.Game.Services.Availability;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.Devices;
using Sharpex2D.Framework.Rendering.Scene;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class SGL
    {
        /// <summary>
        ///     Current Game Instance.
        /// </summary>
        internal static Game GameInstance;

        private static readonly Logger Logger;

        /// <summary>
        ///     Initializes a new SGL class.
        /// </summary>
        static SGL()
        {
            State = EngineState.NotInitialized;
            Logger = LogManager.GetClassLogger();
        }

        /// <summary>
        ///     Current GraphicsDevice.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///     The Current Renderer.
        /// </summary>
        internal static RenderDevice RenderDevice { set; get; }

        /// <summary>
        ///     Gets the Version of SGL.
        /// </summary>
        public static string Version
        {
            get { return "0.6.200"; }
        }

        /// <summary>
        ///     Gets the current state.
        /// </summary>
        public static EngineState State { private set; get; }

        /// <summary>
        ///     ComponentManager Instance.
        /// </summary>
        public static ComponentManager Components { private set; get; }

        /// <summary>
        ///     Initializes SGL.
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
        ///     Initializes SGL.
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
            Components.AddComponent(configurator.RenderTarget);
            Components.AddComponent(new EventManager());
            GraphicsDevice = new GraphicsDevice(configurator.RenderTarget)
            {
                BackBuffer = configurator.BackBuffer
            };
            configurator.RenderTarget.Window.Size = new Vector2(configurator.BackBuffer.Width,
                configurator.BackBuffer.Height);
            GameInstance.Input = new InputManager(configurator.RenderTarget.Handle);
            GameInstance.Content = new ContentManager();
            GameInstance.SceneManager = new SceneManager();
            configurator.GameLoop.TargetFrameTime = 1000/(float) configurator.TargetFrameRate;
            configurator.GameLoop.TargetUpdateTime = 1000/(float) configurator.TargetFrameRate;
            Components.AddComponent(configurator.GameLoop);
            GraphicsDevice.RefreshRate = configurator.TargetFrameRate;
            Components.AddComponent(GameInstance.Content);
            Components.AddComponent(GraphicsDevice);
            Components.AddComponent(GameInstance);
            Components.AddComponent(GameInstance.SceneManager);
            Components.AddComponent(GameInstance.Input);
            Components.Get<IGameLoop>().Subscribe((IDrawable) GameInstance);
            Components.Get<IGameLoop>().Subscribe((IUpdateable) GameInstance);

            //prepare game services
            var gameServices = new GameServiceContainer();

            gameServices.Add(new AchievementProvider());
            gameServices.Add(new AvailabilityProvider());
            gameServices.Add(new Gamer());
            gameServices.Add(new LaunchParameters());

            GameInstance.GameServices = gameServices;

            Components.AddComponent(new ExceptionHandler());

            EngineConfiguration engineConfiguration =
                GameInstance.OnInitialize(GameInstance.GameServices.GetService<LaunchParameters>());

            State = EngineState.Initialized;

            Run(engineConfiguration.Renderer, engineConfiguration.SoundInitializer);
        }

        /// <summary>
        ///     Runs SGL based on the specific initialized options.
        /// </summary>
        /// <param name="renderDevice">The RenderDevice.</param>
        /// <param name="soundInitializer">The ISoundInitializer.</param>
        private static void Run(RenderDevice renderDevice, ISoundInitializer soundInitializer)
        {
            if (State != EngineState.Initialized)
            {
                throw new InvalidOperationException(string.Format("SGL must be initialized in order to run. Current state {0}", State));
            }

            if (State == EngineState.Running) return;

            RenderDevice = renderDevice;
            RenderDevice.GraphicsDevice = GraphicsDevice;
            RenderDevice.InitializeDevice();
            GameInstance.SoundManager = soundInitializer == null ? null : new SoundManager(soundInitializer);
            Components.AddComponent(renderDevice);
            Components.AddComponent(GameInstance.SoundManager);
            Components.Construct();
            GameInstance.OnLoadContent();
            Components.Get<IGameLoop>().Start();

            Logger.Engine("SGL ({0}) is sucessfully running.", Version);
            Logger.Engine("CLR: {0}.", Platform.IsMonoRuntime() ? "Mono" : "Windows");

            State = EngineState.Running;
        }

        /// <summary>
        ///     Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            State = EngineState.NotInitialized;

            Components.Get<IGameLoop>().Stop();
            GameInstance.OnUnload();
            GC.Collect();
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        ///     Restarts the game.
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
            if (State == EngineState.Running)
            {
                throw new InvalidOperationException(string.Format("SGL must be running in order to query any data. Current state {0}", State));
            }

            T data;
            if (Components.Get<ContentManager>().QueryCache<T>(assetname, out data))
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
            if (State == EngineState.Running)
            {
                throw new InvalidOperationException(string.Format("SGL must be running in order to query any data. Current state {0}", State));
            }

            return Components.Get<T>();
        }
    }
}