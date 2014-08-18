using System;
using System.Collections.Generic;

namespace XPlane.Core.Miscellaneous
{
    public class AchievementManager
    {

        /// <summary>
        /// Gets the Achievements.
        /// </summary>
        public List<Achievement> Achievements { private set; get; } 

        /// <summary>
        /// Initializes a new AchievementManager class.
        /// </summary>
        public AchievementManager()
        {
            Achievements = new List<Achievement>
            {
                new EnemyDestroyedAchievement(),
                new ScoreAchievement(),
                new SustainAchievement(),
                new LasterTimeAchievement(),
            };
        }

        /// <summary>
        /// Gets the Achievement.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>TAchievement.</returns>
        public T Get<T>() where T : Achievement
        {
            foreach (var achievement in Achievements)
            {
                if (achievement.GetType() == typeof (T))
                {
                    return (T) achievement;
                }
            }

            throw new InvalidOperationException("Achievement not found.");
        }
    }
}
