using System;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IScript : IContent
    {
        /// <summary>
        ///     Sets or gets the ScriptContent.
        /// </summary>
        string Content { set; get; }

        /// <summary>
        ///     A value indicating whether the script is active.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the guid of the script.
        /// </summary>
        Guid Guid { get; set; }
    }
}