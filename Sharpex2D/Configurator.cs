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
using Sharpex2D.Rendering;
using Sharpex2D.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Configurator : IConfigurator
    {
        /// <summary>
        ///     Initializes a new Configurator class.
        /// </summary>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public Configurator(Game gameInstance, RenderTarget renderTarget)
            : this(60, new BackBuffer(640, 480), new GameLoop(), gameInstance, renderTarget)
        {
        }

        /// <summary>
        ///     Initializes a new Configurator class.
        /// </summary>
        /// <param name="backbuffer">The BackBuffer.</param>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public Configurator(BackBuffer backbuffer, Game gameInstance, RenderTarget renderTarget)
            : this(60, backbuffer, new GameLoop(), gameInstance, renderTarget)
        {
        }

        /// <summary>
        ///     Initializes a new Configurator class.
        /// </summary>
        /// <param name="targetFrameRate">The TargetFrameRate.</param>
        /// <param name="backbuffer">The BackBuffer.</param>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public Configurator(int targetFrameRate, BackBuffer backbuffer, Game gameInstance, RenderTarget renderTarget)
            : this(targetFrameRate, backbuffer, new GameLoop(), gameInstance, renderTarget)
        {
        }

        /// <summary>
        ///     Initializes a new Configurator class.
        /// </summary>
        /// <param name="targetFrameRate">The TargetFrameRate.</param>
        /// <param name="backbuffer">The BackBuffer.</param>
        /// <param name="gameLoop">The GameLoop.</param>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public Configurator(int targetFrameRate, BackBuffer backbuffer, IGameLoop gameLoop, Game gameInstance,
            RenderTarget renderTarget)
        {
            if (!renderTarget.IsValid)
            {
                throw new InvalidOperationException("RenderTarget is not valid.");
            }

            TargetFrameRate = targetFrameRate;
            BackBuffer = backbuffer;
            GameLoop = gameLoop;
            GameInstance = gameInstance;
            RenderTarget = renderTarget;
        }

        /// <summary>
        ///     Gets or sets the BackBuffer.
        /// </summary>
        public BackBuffer BackBuffer { private set; get; }

        /// <summary>
        ///     Gets or sets the TargetFrameRate.
        /// </summary>
        public int TargetFrameRate { private set; get; }

        /// <summary>
        ///     Gets the Game.
        /// </summary>
        public Game GameInstance { private set; get; }

        /// <summary>
        ///     Gets the TargetHandle.
        /// </summary>
        public RenderTarget RenderTarget { get; private set; }

        /// <summary>
        ///     Sets or gets the GameLoop.
        /// </summary>
        public IGameLoop GameLoop { private set; get; }

        /// <summary>
        ///     Gets the default initializer.
        /// </summary>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <returns>SGLInitializer</returns>
        public static Configurator Default(Game gameInstance)
        {
            return new Configurator(gameInstance, RenderTarget.Default);
        }
    }
}