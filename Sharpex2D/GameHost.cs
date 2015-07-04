// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Debug;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.Scene;

namespace Sharpex2D.Framework
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class GameHost
    {
        /// <summary>
        /// Current Game Instance.
        /// </summary>
        internal static Game GameInstance;

        /// <summary>
        /// Gets the current InputManager.
        /// </summary>
        internal static InputManager InputManager;

        private static readonly Logger Logger;

        /// <summary>
        /// Initializes a new GameHost class.
        /// </summary>
        static GameHost()
        {
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
        /// ComponentManager Instance.
        /// </summary>
        internal static ComponentManager Components { private set; get; }

        /// <summary>
        /// Initializes GameHost.
        /// </summary>
        /// <param name="game">The Game.</param>
        internal static void Initialize(Game game)
        {
            Components = new ComponentManager();
            GameWindow gameWindow;

            try
            {
                gameWindow = GameWindow.Default;
            }
            catch (InvalidOperationException)
            {
                gameWindow = GameWindow.CreateNew();
            }

            Components.Add(gameWindow);
            InputManager = new InputManager();
            GameInstance = game;

            var gameLoop = new GameLoop();
            Components.Add(gameLoop);

            GameInstance.Content = new ContentManager();

            var launchParameters = new LaunchParameters();
            GameInstance.OnInitialize(launchParameters);

            GraphicsManager = GameInstance.GraphicsManager;

            GraphicsDevice = new GraphicsDevice(gameWindow, GraphicsManager)
            {
                ClearColor = Color.CornflowerBlue
            };

            InputManager.Initialize();
            gameWindow.ClientSize = new Vector2(GraphicsManager.PreferredBackBufferWidth,
                GraphicsManager.PreferredBackBufferHeight);
            GameInstance.SceneManager = new SceneManager(GameInstance);
            Components.Add(GameInstance.Content);
            Components.Add(GraphicsDevice);
            Components.Add(GameInstance);
            Components.Add(GameInstance.SceneManager);
            Components.Get<GameLoop>().Subscribe((IDrawable) GameInstance);
            Components.Get<GameLoop>().Subscribe((IUpdateable) GameInstance);
            Components.Get<GameLoop>().Subscribe(InputManager);
            Components.Add(new ExceptionHandler());

            Run();
        }

        /// <summary>
        /// Runs GameHost based on the specific initialized options.
        /// </summary>
        private static void Run()
        {
            GraphicsManager = GameInstance.GraphicsManager;
            GameInstance.MediaPlayer = new MediaPlayer(GameInstance.SoundManager);
            Components.Add(GameInstance.MediaPlayer);
            Components.Construct();
            Components.Get<GameLoop>().Start();

            Logger.Engine("Sharpex2D ({0}) is sucessfully running.", typeof (GameHost).Assembly.GetName().Version);
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            Components.Get<GameLoop>().Stop();
            GameInstance.OnUnload();
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
        /// Queries the ComponentManager.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>T.</returns>
        internal static T Get<T>() where T : IComponent
        {
            return Components.Get<T>();
        }
    }
}