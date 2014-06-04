using System;
using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Scripting.VB
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    [Content("Visual Basic Script File")]
    public class VBScript : IScript
    {
        #region IScript Implementation

        /// <summary>
        ///     Sets or gets the ScriptContent.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     A value indicating whether the script is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the guid of the script.
        /// </summary>
        public Guid Guid { get; set; }

        #endregion
    }
}