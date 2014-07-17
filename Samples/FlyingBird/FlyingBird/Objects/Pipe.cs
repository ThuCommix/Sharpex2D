using Sharpex2D.Math;

namespace FlyingBird.Objects
{
    public class Pipe
    {
        /// <summary>
        ///     Initializes a new Pipe class.
        /// </summary>
        public Pipe(int height, int gateway)
        {
            TopPipeHeight = height - 22;
            BottomPipeHeight = 480 - gateway - height;

            BottomPipeY = height + gateway + 22;

            GatewaySize = gateway;
            Position = new Vector2(200, 0);
        }

        /// <summary>
        ///     Gets the TopPipe height.
        /// </summary>
        public float TopPipeHeight { get; private set; }

        /// <summary>
        ///     Gets the BottomPipe height.
        /// </summary>
        public float BottomPipeHeight { get; private set; }

        /// <summary>
        ///     Gets the BottomPipeY
        /// </summary>
        public float BottomPipeY { get; private set; }

        /// <summary>
        ///     Sets or gets the GatewaySize.
        /// </summary>
        public float GatewaySize { private set; get; }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        ///     A value indicating whether the pipe is scored.
        /// </summary>
        public bool Scored { set; get; }
    }
}