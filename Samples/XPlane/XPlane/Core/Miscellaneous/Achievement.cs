

using System;
using System.Xml.Serialization;

namespace XPlane.Core.Miscellaneous
{
    [Serializable]
    public abstract class Achievement
    {
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        [XmlElement("Description")]
        public string Description { set; get; }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        [XmlElement("Stage")]
        public abstract int CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the amount needed to get the next achievement.
        /// </summary>
        [XmlElement("NextAchievementAt")]
        public float NextAchievementAt { set; get; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        [XmlElement("Amount")]
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
