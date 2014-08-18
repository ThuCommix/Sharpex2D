

namespace XPlane.Core.Miscellaneous
{
    public abstract class Achievement
    {
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        public abstract int CurrentLevel { get; }

        /// <summary>
        /// Gets or sets the amount needed to get the next achievement.
        /// </summary>
        public float NextAchievementAt { set; get; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        public float Amount { set; get; }

        /// <summary>
        /// Gets the AchievementString.
        /// </summary>
        /// <returns>String.</returns>
        public abstract string GetAchievementString();

        /// <summary>
        /// Adds an amount.
        /// </summary>
        /// <param name="amount">The Amount.</param>
        public abstract void Add(float amount);
    }
}
