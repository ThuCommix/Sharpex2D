using System;
using SharpexGL.Framework.Components;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering.Font;
using SharpexGL.Framework.Rendering.Geometry;

namespace SharpexGL.Framework.Rendering
{
    public interface IRenderer : IGeometryRenderer, IConstructable, IDisposable
    {
        /// <summary>
        /// Sets or gets the GraphicsDevice.
        /// </summary>
        GraphicsDevice GraphicsDevice
        {
            get;
            set;
        }
        /// <summary>
        /// Sets or gets the Frames per second.
        /// </summary>
        int FramesPerSecond
        {
            get;
        }
        /// <summary>
        /// Sets or gets whether the renderer is disposed.
        /// </summary>
        bool IsDisposed
        {
            get;
            set;
        }
        /// <summary>
        /// A value indicating whether VSync is enabled.
        /// </summary>
        bool VSync { set; get; }
        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        void Begin();
        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        void Close();
        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(Texture texture, Rectangle rectangle, Color color);
        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(Texture texture, Vector2 position, Color color);
        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, IFont font, Vector2 position, Color color);
    }
}
