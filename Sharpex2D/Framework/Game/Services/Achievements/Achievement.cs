using System;

namespace Sharpex2D.Framework.Game.Services.Achievements
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class Achievement
    {
        /// <summary>
        ///     Initializes a new Achievement class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        public Achievement(Guid guid)
        {
            Guid = guid;
        }

        /// <summary>
        ///     Sets or gets the Title.
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        ///     Sets or gets the Description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        ///     A value indicating whether the achievement is solved.
        /// </summary>
        public bool IsSolved { set; get; }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }
    }
}