using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Font;
using Sharpex2D.Framework.Rendering.Geometry;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IRenderer : IGeometryRenderer, IConstructable, IDisposable
    {
        /// <summary>
        ///     Sets or gets the GraphicsDevice.
        /// </summary>
        GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///     Sets or gets whether the renderer is disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///     A value indicating whether VSync is enabled.
        /// </summary>
        bool VSync { set; get; }

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        void Begin();

        /// <summary>
        ///     Flushes the buffer.
        /// </summary>
        void Close();

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, IFont font, Rectangle rectangle, Color color);

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, IFont font, Vector2 position, Color color);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        Vector2 MeasureString(string text, IFont font);

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        void SetTransform(Matrix2x3 matrix);

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        void ResetTransform();
    }
}