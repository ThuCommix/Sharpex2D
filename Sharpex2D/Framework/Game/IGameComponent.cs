namespace Sharpex2D.Framework.Game
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IGameComponent : IUpdateable, IDrawable
    {
        /// <summary>
        ///     Gets the Order.
        /// </summary>
        int Order { get; }
    }
}