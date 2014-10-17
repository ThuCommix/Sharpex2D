using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.UI;

namespace XPlane.Core.UI
{
    public class MenuButton : UIControl
    {
        private readonly Font _font;

        /// <summary>
        /// Initializes a new MenuButton class.
        /// </summary>
        /// <param name="assignedUIManager">The AssignedUIManager.</param>
        public MenuButton(UIManager assignedUIManager) : base(assignedUIManager)
        {
            Size = new UISize(300, 30);
            _font = new Font("Segoe UI", 16, TypefaceStyle.Bold);
            Text = "MenuButton1";
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// Draws the MenuButton.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        public override void OnDraw(SpriteBatch spriteBatch)
        {
            Vector2 dim = spriteBatch.MeasureString(Text, _font);
            spriteBatch.DrawString(Text, _font,
                new Vector2(Position.X - (dim.X/2) + (Size.Width/2), Position.Y - (dim.Y/2) + (Size.Height/2)),
                IsMouseHoverState ? Color.DarkOrange : Color.White);
        }
    }
}