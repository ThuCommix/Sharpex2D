using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Fonts;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class RenderDevice : IComponent
    {
        /// <summary>
        ///     Initializes a new RenderDevice class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="resourceManager">The ResourceManager.</param>
        protected RenderDevice(ResourceManager resourceManager, Guid guid)
        {
            Guid = guid;
            ResourceManager = resourceManager;
        }

        /// <summary>
        ///     Gets the required PlatformVersion.
        /// </summary>
        public abstract Version PlatformVersion { get; }

        /// <summary>
        ///     A value indicating whether the current platform is supported.
        /// </summary>
        public bool IsPlatformSupported
        {
            get { return Environment.OSVersion.Version >= PlatformVersion; }
        }

        /// <summary>
        ///     Gets the ContentProcessors.
        /// </summary>
        public abstract IContentProcessor[] ContentProcessors { get; }

        /// <summary>
        ///     Gets or sets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { set; get; }

        /// <summary>
        ///     Gets the ResourceManager.
        /// </summary>
        public ResourceManager ResourceManager { private set; get; }

        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { private set; get; }

        #endregion

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public abstract void InitializeDevice();

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        public abstract void Begin();

        /// <summary>
        ///     Ends the draw operation.
        /// </summary>
        public abstract void End();

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public abstract void DrawString(string text, Font font, Rectangle rectangle, Color color);

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public abstract void DrawString(string text, Font font, Vector2 position, Color color);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public abstract void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public abstract void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public abstract Vector2 MeasureString(string text, Font font);

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public abstract void SetTransform(Matrix2x3 matrix);

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        public abstract void ResetTransform();

        /// <summary>
        ///     Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public abstract void DrawRectangle(Pen pen, Rectangle rectangle);

        /// <summary>
        ///     Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public abstract void DrawLine(Pen pen, Vector2 start, Vector2 target);

        /// <summary>
        ///     Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public abstract void DrawEllipse(Pen pen, Ellipse ellipse);

        /// <summary>
        ///     Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public abstract void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>
        ///     Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public abstract void DrawPolygon(Pen pen, Polygon polygon);

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public abstract void FillRectangle(Color color, Rectangle rectangle);

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public abstract void FillEllipse(Color color, Ellipse ellipse);

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public abstract void FillPolygon(Color color, Polygon polygon);
    }
}