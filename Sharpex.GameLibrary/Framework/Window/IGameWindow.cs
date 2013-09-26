using System;
using System.Drawing;

namespace SharpexGL.Framework.Window
{
    public interface IGameWindow
    {
        /// <summary>
        /// Gets or sets the GameWindowHandle.
        /// </summary>
        IntPtr Handle { set; get; }
        /// <summary>
        /// Updates the GameWindow.
        /// </summary>
        void Update();
        /// <summary>
        /// Sets the Title of the GameWindow.
        /// </summary>
        /// <param name="value">The Value.</param>
        void SetTitle(string value);
        /// <summary>
        /// Sets the Icon of the GameWindow.
        /// </summary>
        /// <param name="icon">The Icon.</param>
        void SetIcon(Icon icon);
        /// <summary>
        /// Sets the Size of the GameWindow.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        void SetSize(int width, int height);
        /// <summary>
        /// Sets the Position of the GameWindow.
        /// </summary>
        /// <param name="x">The XKoord.</param>
        /// <param name="y">The YKoord.</param>
        void SetPosition(int x, int y);
        /// <summary>
        /// Sets the Style of the GameWindow.
        /// </summary>
        /// <param name="style">The WindowStyle.</param>
        void SetWindowStyle(WindowStyle style);
        /// <summary>
        /// Gets the Soue of the GameWindow.
        /// </summary>
        /// <returns>Size</returns>
        Size GetSize();
    }
}
