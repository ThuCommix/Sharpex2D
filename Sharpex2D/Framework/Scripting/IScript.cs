using System;

namespace Sharpex2D.Framework.Scripting
{
    public interface IScript
    {
        /// <summary>
        /// Sets or gets the ScriptContent.
        /// </summary>
        string Content { set; get; }
        /// <summary>
        /// A value indicating whether the script is active.
        /// </summary>
        bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the guid of the script.
        /// </summary>
        Guid Guid { get; set; }
    }
}
