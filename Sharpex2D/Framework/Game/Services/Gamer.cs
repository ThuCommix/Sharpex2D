using System;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Game.Services
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Gamer Profile")]
    [Serializable]
    public class Gamer : IGameService, IContent
    {
        /// <summary>
        ///     Initializes a new Gamer class.
        /// </summary>
        public Gamer()
        {
            Guid = Guid.NewGuid();
        }

        /// <summary>
        ///     Gets or sets the DisplayName of the gamer.
        /// </summary>
        public string DisplayName { set; get; }

        /// <summary>
        ///     Gets or sets the Guid-Identifer of the gamer.
        /// </summary>
        public Guid Guid { get; set; }
    }
}