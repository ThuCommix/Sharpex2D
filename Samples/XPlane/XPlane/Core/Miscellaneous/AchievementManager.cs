using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XPlane.Core.Miscellaneous
{
    [XmlInclude(typeof(EnemyDestroyedAchievement))]
    [XmlInclude(typeof(LasterTimeAchievement))]
    [XmlInclude(typeof(ScoreAchievement))]
    [XmlInclude(typeof(SustainAchievement))]
    [Serializable]
    public class AchievementManager
    {
        /// <summary>
        /// Gets the Achievements.
        /// </summary>
        [XmlElement("Achievements")]
        public List<Achievement> Achievements { private set; get; } 

        /// <summary>
        /// Initializes a new AchievementManager class.
        /// </summary>
        public AchievementManager()
        {
            Achievements = new List<Achievement>();
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
