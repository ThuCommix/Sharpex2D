using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.UI;
using XPlane.Core.Miscellaneous;

namespace XPlane.Core.UI
{
    public class AchievementControl : UIControl
    {
        /// <summary>
        /// Gets or sets the AchievementManager.
        /// </summary>
        public AchievementManager AchievementManager { set; get; }

        private Font _description;
        private Font _header;
        private readonly Rectangle _display;
        private readonly Pen _progressPen;
        private int _offset;

        /// <summary>
        /// Initializes a new AchievementControl class.
        /// </summary>
        /// <param name="assignedUIManager">The AssignedUIManager.</param>
        public AchievementControl(UIManager assignedUIManager) : base(assignedUIManager)
        {
            _description = new Font("Segoe UI", 12, TypefaceStyle.Regular);
            _header = new Font("Segoe UI", 15, TypefaceStyle.Bold);
            _display = new Rectangle(0, 0, 800, 480);
            _progressPen = new Pen(Color.FromArgb(180, 0, 0, 0), 1);
        }

        /// <summary>
        /// Draws the AchievementControl.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        public override void OnDraw(SpriteBatch spriteBatch)
        {
            _offset = 20;
            spriteBatch.FillRectangle(Color.FromArgb(220, 0, 0, 0), _display);

            foreach (var achievement in AchievementManager.Achievements)
            {
                spriteBatch.DrawString(achievement.GetAchievementString(), _header, new Vector2(20, _offset), Color.White);
                spriteBatch.DrawString(achievement.Description, _description, new Vector2(20, _offset + 20), Color.White);

                var progress = new Rectangle(20, _offset + 65, 200, 15);
                spriteBatch.DrawRectangle(_progressPen, progress);

                var progressC = 198*achievement.Amount/achievement.NextAchievementAt;

                spriteBatch.FillRectangle(Color.Green, new Rectangle(21, _offset + 66, progressC, 13));


                _offset += 100;
            }
        }
    }
}
