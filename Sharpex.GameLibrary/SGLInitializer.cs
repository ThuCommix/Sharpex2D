using System;
using SharpexGL.Framework.Game;

namespace SharpexGL
{
    public class SGLInitializer
    {
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        public SGLInitializer()
        {
            Width = 640;
            Height = 480;
            GameInstance = null;
            TargetFramesPerSecond = 60;
        }
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        /// <param name="gameInstance">The Game.</param>
        /// <param name="targetHandle">The SurfaceTargetHandle.</param>
        public SGLInitializer(Game gameInstance, IntPtr targetHandle)
        {
            Width = 640;
            Height = 480;
            GameInstance = gameInstance;
            TargetHandle = targetHandle;
            TargetFramesPerSecond = 60;
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
        public IntPtr TargetHandle { get; set; }
    }
}
