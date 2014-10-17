using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XPlane.Core.Miscellaneous
{
    [Serializable]
    public class ScoreAchievement : Achievement
    {
        private int _currentLv = 1;

        /// <summary>
        /// Initializes a new ScoreAchievement class.
        /// </summary>
        public ScoreAchievement()
        {
            NextAchievementAt = 1000;
            Description = string.Format("Higher score means a larger internet penis (0/1000)");
        }

        /// <summary>
        /// Gets the AchievementString.
        /// </summary>
        /// <returns>String.</returns>
        public override string GetAchievementString()
        {
            return string.Format("Score is my life (Lv.:{0})", _currentLv);
        }

        /// <summary>
        /// Adds an amount.
        /// </summary>
        /// <param name="amount">The Amount.</param>
        public override void Add(float amount)
        {
            if (_currentLv == 10) return;
            Amount = amount;

            if (Amount >= NextAchievementAt)
            {
                _currentLv++;
                GameMessage.Instance.QueueMessage(string.Format("Achievement: {0}", GetAchievementString()));
                if (_currentLv < 10)
                {
                    NextAchievementAt *= 2;
                }
            }
            Description = string.Format("Higher score means a larger internet penis ({0}/{1})", Amount, NextAchievementAt);
        }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        [XmlElement("Stage")]
        public override int CurrentLevel
        {
            get { return _currentLv; }
            set { _currentLv = value; }
        }
    }
}
