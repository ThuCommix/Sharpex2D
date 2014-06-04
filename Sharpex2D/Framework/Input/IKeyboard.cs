using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Game;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IKeyboard : IDevice, IConstructable, IUpdateable
    {
        /// <summary>
        ///     Determines, if a specific key is pressed.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyPressed(Keys key);

        /// <summary>
        ///     Determines, if a specific key is pushed up.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyUp(Keys key);

        /// <summary>
        ///     Determines, if a specific key is pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyDown(Keys key);
    }
}