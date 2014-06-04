using System;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class SGLInitializer
    {
        /// <summary>
        ///     Initialize SGLInitializer.
        /// </summary>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public SGLInitializer(Game gameInstance, RenderTarget renderTarget)
        {
            Width = 640;
            Height = 480;
            GameInstance = gameInstance;
            RenderTarget = renderTarget;
            TargetFramesPerSecond = 60;
            GameLoop = new GameLoop();

            if (!renderTarget.IsValid)
            {
                throw new InvalidOperationException("RenderTarget is not valid.");
            }
        }

        /// <summary>
        ///     Gets or sets the Width.
        /// </summary>
        public int Width { set; get; }

        /// <summary>
        ///     Gets or sets the Height.
        /// </summary>
        public int Height { set; get; }

        /// <summary>
        ///     Gets or sets the TargetFramesPerSecond.
        /// </summary>
        public int TargetFramesPerSecond { set; get; }

        /// <summary>
        ///     Gets or sets the Game.
        /// </summary>
        public Game GameInstance { set; get; }

        /// <summary>
        ///     Sets or gets the TargetHandle.
        /// </summary>
        public RenderTarget RenderTarget { get; set; }

        /// <summary>
        ///     Sets or gets the GameLoop.
        /// </summary>
        public IGameLoop GameLoop { set; get; }

        /// <summary>
        ///     Gets the default initializer.
        /// </summary>
        /// <param name="gameInstance">The GameInstance.</param>
        /// <returns>SGLInitializer</returns>
        public static SGLInitializer Default(Game gameInstance)
        {
            return new SGLInitializer(gameInstance, RenderTarget.GetDefault());
        }
    }
}