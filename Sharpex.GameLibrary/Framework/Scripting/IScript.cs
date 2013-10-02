
namespace SharpexGL.Framework.Scripting
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
        /// Gets or sets the name of the script.
        /// </summary>
        string Name { get; set; }
    }
}
