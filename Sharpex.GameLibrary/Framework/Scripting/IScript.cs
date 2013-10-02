
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
        bool IsActive { get; }
        /// <summary>
        /// Gets the name of the script.
        /// </summary>
        string Name { get; }
    }
}
