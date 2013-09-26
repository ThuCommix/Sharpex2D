namespace SharpexGL.Framework.Components
{
    public interface IConstructable : IComponent
    {
        /// <summary>
        /// Constructs the Component
        /// </summary>
        void Construct();
    }
}
