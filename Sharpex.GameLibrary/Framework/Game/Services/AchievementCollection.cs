using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Game.Services
{
    public class AchievementCollection
    {
        /// <summary>
        /// Initializes a new AchievementCollection class.
        /// </summary>
        public AchievementCollection()
        {
            _achievements = new Dictionary<Guid, IAchievement>();
        } 

        private readonly Dictionary<Guid, IAchievement> _achievements;

        /// <summary>
        /// Adds a new Achievement to the collection.
        /// </summary>
        /// <param name="achievement"></param>
        public void Add(IAchievement achievement)
        {
            _achievements.Add(achievement.Guid, achievement);
        }

        /// <summary>
        /// Removes an Achievement from the collection
        /// </summary>
        /// <param name="achievementGuid">The AchievementGuid.</param>
        public void Remove(Guid achievementGuid)
        {
            if (_achievements.ContainsKey(achievementGuid))
            {
                _achievements.Remove(achievementGuid);
                return;
            }

            throw new ArgumentException("The achievement does not exist.");
        }

        /// <summary>
        /// Returns a specified achievement.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>T</returns>
        public T Get<T>() where T : IAchievement
        {
            foreach (var achievement in _achievements.Values)
            {
                if (achievement.GetType() == typeof (T))
                {
                    return (T)achievement;
                }
            }

            throw new ArgumentException(typeof (T).Name + " could not be resolved.");
        }

        /// <summary>
        /// Returns a specified achievement.
        /// </summary>
        /// <param name="achievementGuid">The AchievementGuid.</param>
        /// <returns>IAchievement</returns>
        public IAchievement Get(Guid achievementGuid)
        {
            if (_achievements.ContainsKey(achievementGuid))
            {
                return _achievements[achievementGuid];
            }

            throw new ArgumentException("The achievement does not exist.");
        }

        /// <summary>
        /// Returns all achievements.
        /// </summary>
        /// <returns>IAchievement Array.</returns>
        public IAchievement[] GetAll()
        {
            var array = new IAchievement[_achievements.Values.Count];
            _achievements.Values.CopyTo(array, 0);
            return array;
        }
    }
}
