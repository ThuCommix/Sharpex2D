
using System;

namespace XPlane.Core.Miscellaneous
{
    public class EnemyDestroyedAchievement : Achievement
    {
        private int _currentLv = 1;

        /// <summary>
        /// Initializes a new EnemyDestroyedAchievement class.
        /// </summary>
        public EnemyDestroyedAchievement()
        {
            NextAchievementAt = 5;
            Description = string.Format("We hate mines, you should feel the same. (0/5)");
        }

        /// <summary>
        /// Gets the AchievementString.
        /// </summary>
        /// <returns>String.</returns>
        public override string GetAchievementString()
        {
            return string.Format("Mine killer (Lv.:{0})", _currentLv);
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
                NextAchievementAt *= 2;
            }
            Description = string.Format("We hate mines, you should feel the same. ({0}/{1})", Amount, NextAchievementAt);
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
