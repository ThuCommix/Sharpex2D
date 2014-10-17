using System.Collections.Generic;
using Sharpex2D;
using Sharpex2D.Common;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class GameMessage : Singleton<GameMessage>, IGameComponent
    {
        /// <summary>
        /// Gets the LifeTime in ms.
        /// </summary>
        public const float LifeTime = 5000;

        private readonly Font _font;
        private readonly int _y = 70;
        private readonly List<string> _queuedMessages;
        private float _clifeTime = LifeTime;
        private readonly Pen _pen;
        private readonly Pen _penBorder;
        
        /// <summary>
        /// Initializes a new GameMessage class.
        /// </summary>
        public GameMessage()
        {
            _font = new Font("Segoe UI", 15, TypefaceStyle.Bold);
            _queuedMessages = new List<string>();
            _pen = new Pen(Color.FromArgb(120, 0, 0, 0), 1);
            _penBorder = new Pen(Color.FromArgb(190, 0, 0, 0), 1);
        }

        /// <summary>
        /// Queues a new message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void QueueMessage(string message)
        {
            _queuedMessages.Add(message);
        }

        /// <summary>
        /// Updates the GameMessage.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (_queuedMessages.Count == 0) return;
            
            _clifeTime -= gameTime.ElapsedGameTime;
            if (_clifeTime <= 0)
            {
                _clifeTime = LifeTime;
                _queuedMessages.RemoveAt(0);
            }
        }

        /// <summary>
        /// Draws the GameMessage.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_queuedMessages.Count > 0)
            {
                var dim = spriteBatch.MeasureString(_queuedMessages[0], _font);
                var display = new Rectangle(800 - dim.X - 20, 1, dim.X + 20, dim.Y + 20);

                spriteBatch.FillRectangle(_pen.Color, display);
                spriteBatch.DrawRectangle(_penBorder, display);
                spriteBatch.DrawString(_queuedMessages[0], _font, new Vector2(790 - dim.X, 11), Color.White);
            }
        }

        /// <summary>
        /// Gets the Order.
        /// </summary>
        public int Order { get { return 10000; } }
    }
}
