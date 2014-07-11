// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.GameService
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AchievementProvider : IComponent, IGameService
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("EE55BA76-5C25-4EEA-859C-470082438DAA"); }
        }

        #endregion

        private readonly Dictionary<Guid, Achievement> _achievements;

        /// <summary>
        ///     Initializes a new AchievementProvider class.
        /// </summary>
        public AchievementProvider()
        {
            _achievements = new Dictionary<Guid, Achievement>();
        }

        /// <summary>
        ///     Adds a new Achievement.
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
        ///     Removes a Achievement.
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
        ///     Removes a Achievement.
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
        ///     Gets a Achievement.
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
        ///     Gets the solved Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetSolved()
        {
            return
                (from achievement in _achievements where achievement.Value.IsSolved select achievement.Value).ToArray();
        }

        /// <summary>
        ///     Gets the unsolved Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetUnSolved()
        {
            return
                (from achievement in _achievements where !achievement.Value.IsSolved select achievement.Value).ToArray();
        }

        /// <summary>
        ///     Gets all Achievements.
        /// </summary>
        /// <returns>Array of Achievement</returns>
        public Achievement[] GetAll()
        {
            return _achievements.Values.ToArray();
        }
    }
}