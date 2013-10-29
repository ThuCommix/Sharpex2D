using System;

namespace SharpexGL.Framework.Game.Services
{
    public interface IAchievement
    {
        /// <summary>
        /// Gets the title of the achievement.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Gets the description of the achievement.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Gets the Guid-Identifer of the achievement.
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// A value indicating whether the achievement is solved.
        /// </summary>
        bool IsSolved { set; get; }
    }
}
