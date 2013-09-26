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
        public SGLInitializer(Game gameInstance)
        {
            Width = 640;
            Height = 480;
            GameInstance = gameInstance;
            TargetFramesPerSecond = 60;
        }
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public SGLInitializer(int width, int height)
        {
            Width = width;
            Height = height;
            TargetFramesPerSecond = 60;
        }
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="framesPerSecond">The TargetFrameRate.</param>
        public SGLInitializer(int width, int height, int framesPerSecond)
        {
            Width = width;
            Height = height;
            TargetFramesPerSecond = framesPerSecond;
        }
        /// <summary>
        /// Initialize SGLInitializer.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="framesPerSecond">The TargetFrameRate.</param>
        /// <param name="gameInstance">The Game.</param>
        public SGLInitializer(int width, int height, int framesPerSecond , Game gameInstance)
        {
            Width = width;
            Height = height;
            GameInstance = gameInstance;
            TargetFramesPerSecond = framesPerSecond;
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
    }
}
