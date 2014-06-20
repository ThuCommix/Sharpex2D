namespace Sharpex2D.Framework.Components
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IConstructable : IComponent
    {
        /// <summary>
        ///     Constructs the Component
        /// </summary>
        void Construct();
    }
}