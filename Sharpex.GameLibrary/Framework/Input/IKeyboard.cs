using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Input
{
    public interface IKeyboard : IDevice, IConstructable
    {
        /// <summary>
        /// Determines, if a specific key is pressed.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyPressed(Keys key);

        /// <summary>
        /// Determines, if a specific key is pushed up.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyUp(Keys key);

        /// <summary>
        /// Determines, if a specific key is pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        bool IsKeyDown(Keys key);

        /// <summary>
        /// Called if the component should get updated.
        /// </summary>
        /// <param name="elapsed">The Elapsed</param>
        void Tick(float elapsed);
    }
}
