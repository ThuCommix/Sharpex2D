using System;
using System.Collections.Generic;
using System.Linq;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Game.Services.Achievements
{
    public class AchievementProvider : IComponent
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("EE55BA76-5C25-4EEA-859C-470082438DAA"); }
        }
        #endregion

        /// <summary>
        /// Initializes a new AchievementProvider class.
        /// </summary>
        public AchievementProvider()
        {
            _achievements = new Dictionary<Guid, Achievement>();
        }

        private readonly Dictionary<Guid, Achievement> _achievements;

        /// <summary>
        /// Adds a new Achievement.
        /// </summary>
        /// <param name="achievement">The Achievement.</param>
        public void Add(Achievement achievement)
        {
            if (_achievements.ContainsKey(achievement.Guid))
            {
                throw new InvalidOperationException("The guid is already existing.");
            }

            _achievements.Add(achievement.Guid, achievement);
        }
        /// <summary>
        /// Removes a Achievement.
        /// </summary>
        /// <param name="achievement">The Achievement.</param>
        public void Remove(Achievement achievement)
        {
            if (!_achievements.ContainsValue(achievement))
            {
                throw new InvalidOperationException("The achievement was not found.");
            }
                
            _achievements.Remove(achievement.Guid);
        }
        /// <summary>
        /// Removes a Achievement.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        public void Remove(Guid guid)
        {
            if (!_achievements.ContainsKey(guid))
            {
                throw new InvalidOperationException("The guid was not found.");
            }

            _achievements.Remove(guid);
        }
        /// <summary>
        /// Gets a Achievement.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>Achievement</returns>
        public Achievement Get(Guid guid)
        {
            if (!_achievements.ContainsKey(guid))
            {
                throw new InvalidOperationException("The guid was not found.");
            }

            return _achievements[guid];
        }
        /// <summary>
        /// Gets the solved Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetSolved()
        {
            return (from achievement in _achievements where achievement.Value.IsSolved select achievement.Value).ToArray();
        }

        /// <summary>
        /// Gets the unsolved Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetUnSolved()
        {
            return (from achievement in _achievements where !achievement.Value.IsSolved select achievement.Value).ToArray();
        }
        /// <summary>
        /// Gets all Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetAll()
        {
            return _achievements.Values.ToArray();
        }
    }
}
