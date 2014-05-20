using System;
using System.Drawing;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Surface
{
    public interface ISurfaceControl : IDisposable
    {
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
        void SetWindowStyle(SurfaceStyle style);
        /// <summary>
        /// Gets the Soue of the GameWindow.
        /// </summary>
        /// <returns>Size</returns>
        Vector2 GetSize();
        /// <summary>
        /// Sets the ControlLayout.
        /// </summary>
        /// <param name="surfaceLayout">The SurfaceLayout.</param>
        void SetControlLayout(SurfaceLayout surfaceLayout);
        /// <summary>
        /// Sets the Cursor visibility.
        /// </summary>
        /// <param name="state">The State.</param>
        void SetCursorVisibility(bool state);
        /// <summary>
        /// Sets the Cursor icon.
        /// </summary>
        /// <param name="iconPath">The IconPath.</param>
        void SetCursorIcon(string iconPath);
        /// <summary>
        /// Indicating whether the surface is running in fullscreen.
        /// </summary>
        /// <returns>True if fullscreen is activated</returns>
        bool IsFullscreen();
    }
}
