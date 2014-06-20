using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IMouse : IDevice, IUpdateable, IConstructable
    {
        /// <summary>
        ///     Gets the current MousePosition.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        ///     Determines, if a specific button is released.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <returns>Boolean</returns>
        bool IsButtonReleased(MouseButtons button);

        /// <summary>
        ///     Determines, if a specific button is pressed.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <returns>Boolean</returns>
        bool IsButtonPressed(MouseButtons button);
    }
}