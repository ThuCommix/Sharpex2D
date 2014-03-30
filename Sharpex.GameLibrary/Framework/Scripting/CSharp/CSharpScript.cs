using System;
using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Scripting.CSharp
{
    public class CSharpScript : IScript, IContent
    {
        #region IScript Implementation
        /// <summary>
        /// Sets or gets the ScriptContent.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// A value indicating whether the script is active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the guid of the script.
        /// </summary>
        public Guid Guid { get; set; }
        #endregion
    }
}
