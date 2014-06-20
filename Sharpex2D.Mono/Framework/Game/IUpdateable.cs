namespace Sharpex2D.Framework.Game
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    public interface IUpdateable
    {
        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        void Update(GameTime gameTime);
    }
}