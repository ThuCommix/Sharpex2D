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
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework
{
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
            GameInstance.Setup(launchParameters);

            GraphicsManager = GameInstance.GraphicsManager;

            GraphicsDevice = new GraphicsDevice(gameWindow, GraphicsManager);

            InputManager.Initialize();
            gameWindow.ClientSize = new Vector2(GraphicsManager.PreferredBackBufferWidth,
                GraphicsManager.PreferredBackBufferHeight);
            GameInstance.Window = gameWindow;
            GameInstance.SceneManager = new SceneManager(GameInstance);
            Components.Add(GameInstance.Content);
            Components.Add(GraphicsDevice);
            Components.Add(GameInstance);
            Components.Add(GameInstance.SceneManager);
            Components.Get<GameLoop>().Subscribe((IDrawable) GameInstance);
            Components.Get<GameLoop>().Subscribe((IUpdateable) GameInstance);
            Components.Get<GameLoop>().Subscribe(InputManager);
            Components.Get<GameLoop>().Subscribe(gameWindow);

            Run();
        }

        /// <summary>
        /// Runs GameHost based on the specific initialized options.
        /// </summary>
        private static void Run()
        {
            GraphicsManager = GameInstance.GraphicsManager;
            GameInstance.SoundPlayer = new SoundPlayer(GameInstance.SoundManager);
            Components.Add(GameInstance.SoundPlayer);
            GameInstance.Initialize();

            Components.Get<GameLoop>().SuccessfullyInitialized += (o, e) =>
            {
                Logger.Instance.Engine(
                    $"Sharpex2D ({typeof (GameHost).Assembly.GetName().Version}) is sucessfully running.");
            };

            Components.Get<GameLoop>().Start();
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            Components.Get<GameLoop>().Stop();
            GameInstance.Unload();
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
                Logger.Instance.Engine("Failed to restart the process.");
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
