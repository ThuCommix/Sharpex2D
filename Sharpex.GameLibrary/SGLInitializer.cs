using System;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Surface;

namespace SharpexGL
{
    public class SGLInitializer
    {
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        /// <param name="gameInstance">The Game.</param>
        /// <param name="renderTarget">The RenderTarget.</param>
        public SGLInitializer(Game gameInstance, RenderTarget renderTarget)
        {
            Width = 640;
            Height = 480;
            GameInstance = gameInstance;
            RenderTarget = renderTarget;
            TargetFramesPerSecond = 60;
            GameLoop = new DualThreadGameLoop();
        }
        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// Gets or sets the TargetFramesPerSecond.
        /// </summary>
        public int TargetFramesPerSecond { set; get; }
        /// <summary>
        /// Gets or sets the Game.
        /// </summary>
        public Game GameInstance { set; get; }
        /// <summary>
        /// Sets or gets the TargetHandle.
        /// </summary>
        public RenderTarget RenderTarget { get; set; }
        /// <summary>
        /// Sets or gets the GameLoop.
        /// </summary>
        public IGameLoop GameLoop { set; get; }
    }
}
