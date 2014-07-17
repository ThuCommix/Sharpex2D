using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace FlyingBird
{
    public class Player
    {
        private readonly Texture2D _erased;
        private readonly Pen _pen;
        private readonly AnimatedSpriteSheet _spriteSheet;
        private Vector2 _position;

        /// <summary>
        ///     Initializes a new Player class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="texture2">The ErasedTexture.</param>
        public Player(Texture2D texture, Texture2D texture2)
        {
            _erased = texture2;
            Bounds = new Polygon(new Vector2(1, 6), new Vector2(5, 4), new Vector2(11, 0), new Vector2(22, 0), new Vector2(28, 6),
                new Vector2(28, 11), new Vector2(31, 15), new Vector2(28, 21), new Vector2(19, 21), new Vector2(18, 23),
                new Vector2(9, 23), new Vector2(0, 13), new Vector2(1, 7));
            _pen = new Pen(Color.White, 1);
            _spriteSheet = new AnimatedSpriteSheet(texture) {AutoUpdate = true};
            _spriteSheet.Add(new Keyframe(new Rectangle(0, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(32, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(64, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(96, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(128, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(160, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(192, 0, 32, 24), 100));

            _spriteSheet.Add(new Keyframe(new Rectangle(224, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(256, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(288, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(320, 0, 32, 24), 100));
            _spriteSheet.Add(new Keyframe(new Rectangle(352, 0, 32, 24), 100));

            Position = new Vector2(300, 230);
            Velocity = new Vector2(0, 0);
        }

        /// <summary>
        ///     Gets the Bounds.
        /// </summary>
        public Polygon Bounds { private set; get; }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public Vector2 Position
        {
            set
            {
                _position = value;
            }
            get { return _position; }
        }

        /// <summary>
        ///     Gets or sets the Velocity.
        /// </summary>
        public Vector2 Velocity { set; get; }

        /// <summary>
        ///     A value indicating whether the player is dead.
        /// </summary>
        public bool Dead { set; get; }

        /// <summary>
        ///     Renders the object.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public void Render(RenderDevice renderer)
        {
            if (!Dead)
            {
                renderer.DrawTexture(_spriteSheet, _position);
            }
            else
            {
                renderer.DrawTexture(_erased, _position);
            }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            float tSecond = gameTime.ElapsedGameTime/1000f;
            float gravity = 12f;

            float velocity = gravity*tSecond;

            Velocity = new Vector2(Velocity.X, Velocity.Y + velocity);

            Position += Velocity;
            _spriteSheet.Update(gameTime);
        }
    }
}