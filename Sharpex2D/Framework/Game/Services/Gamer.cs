using System;

namespace Sharpex2D.Framework.Game.Services
{
    [Serializable]
    public class Gamer
    {
        /// <summary>
        /// Gets or sets the DisplayName of the gamer.
        /// </summary>
        public string DisplayName { set; get; }
        /// <summary>
        /// Gets or sets the Guid-Identifer of the gamer.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Initializes a new Gamer class.
        /// </summary>
        public Gamer()
        {
            Guid = Guid.NewGuid();
        }
    }
}
