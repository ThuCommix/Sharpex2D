using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlane.Core.Miscellaneous
{
    public class SustainAchievement : Achievement
    {
        private int _currentLv = 1;

        /// <summary>
        /// Initializes a new SustainAchievement class.
        /// </summary>
        public SustainAchievement()
        {
            NextAchievementAt = 30000;
            Description = string.Format("Sustain as long as you can! (0s/30s)");
        }

        /// <summary>
        /// Gets the AchievementString.
        /// </summary>
        /// <returns>String.</returns>
        public override string GetAchievementString()
        {
            return string.Format("Sustain the mines (Lv.:{0})", _currentLv);
        }

        /// <summary>
        /// Adds an amount.
        /// </summary>
        /// <param name="amount">The Amount.</param>
        public override void Add(float amount)
        {
            Amount += amount;

            if (Amount >= NextAchievementAt)
            {
                _currentLv++;
                GameMessage.Instance.QueueMessage(string.Format("Achievement: {0}", GetAchievementString()));
                NextAchievementAt *= 2f;
            }
            Description = string.Format("Sustain as long as you can! ({0}s/{1}s)", (int)(Amount / 1000),
                (int) (NextAchievementAt/1000));
        }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        public override int CurrentLevel
        {
            get { return _currentLv; }
        }
    }
}
