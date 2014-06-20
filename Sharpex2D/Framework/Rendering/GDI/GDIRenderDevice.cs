using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Content.Pipeline.Processor;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Devices;
using Sharpex2D.Framework.Rendering.GDI.Fonts;
using Font = Sharpex2D.Framework.Rendering.Fonts.Font;
using Matrix = System.Drawing.Drawing2D.Matrix;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Device("Graphics Device Interface")]
    public class GDIRenderDevice : RenderDevice
    {
        private readonly IContentProcessor[] _contentProcessors;
        private readonly InterpolationMode _interpolationMode;
        private readonly SmoothingMode _smoothingMode;
        private Bitmap _buffer;
        private Graphics _buffergraphics;
        private bool _isBegin;

        /// <summary>
        ///     Initializes a new GDIRenderDevice class.
        /// </summary>
        /// <param name="interpolationMode">The InterpolationMode.</param>
        /// <param name="smoothingMode">The SmoothingMode.</param>
        public GDIRenderDevice(InterpolationMode interpolationMode, SmoothingMode smoothingMode)
            : base(new GDIResourceManager(), new Guid("A18634EE-B48C-4705-866F-ADFA5F4630ED"))
        {
            _contentProcessors = new IContentProcessor[]
            {new GDIFontContentProcessor(), new GDIPenContentProcessor(), new GDITextureContentProcessor()};
            _interpolationMode = interpolationMode;
            _smoothingMode = smoothingMode;
        }

        /// <summary>
        ///     Initializes a new GDIRenderDevice class.
        /// </summary>
        public GDIRenderDevice() : base(new GDIResourceManager(), new Guid("A18634EE-B48C-4705-866F-ADFA5F4630ED"))
        {
            _contentProcessors = new IContentProcessor[]
            {new GDIFontContentProcessor(), new GDIPenContentProcessor(), new GDITextureContentProcessor()};

            _interpolationMode = InterpolationMode.Linear;
            _smoothingMode = SmoothingMode.AntiAlias;
        }

        /// <summary>
        ///     Gets the required PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(5, 1); }
        }

        /// <summary>
        ///     Gets the ContentProcessors.
        /// </summary>
        public override IContentProcessor[] ContentProcessors
        {
            get { return _contentProcessors; }
        }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public override void InitializeDevice()
        {
            _buffer = new Bitmap(GraphicsDevice.BackBuffer.Width, GraphicsDevice.BackBuffer.Height);
            _buffergraphics = Graphics.FromImage(_buffer);

            _buffergraphics.InterpolationMode = _interpolationMode == InterpolationMode.Linear
                ? System.Drawing.Drawing2D.InterpolationMode.Bilinear
                : System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            _buffergraphics.SmoothingMode = _smoothingMode == SmoothingMode.AntiAlias
                ? System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                : System.Drawing.Drawing2D.SmoothingMode.None;

            _buffergraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            _buffergraphics.CompositingMode = CompositingMode.SourceOver;
            _buffergraphics.CompositingQuality = CompositingQuality.HighQuality;
            _buffergraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _buffergraphics.PageUnit = GraphicsUnit.Pixel;

            GraphicsDevice.ClearColor = Color.CornflowerBlue;

            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
        }

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        public override void Begin()
        {
            if (_isBegin)
            {
                throw new RenderDeviceException("Begin is already called.");
            }
            _isBegin = true;
            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
        }

        /// <summary>
        ///     Ends the draw operation.
        /// </summary>
        public override void End()
        {
            if (!_isBegin)
            {
                throw new RenderDeviceException("Begin was not called.");
            }

            _isBegin = false;
            Present();
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public override void DrawString(string text, Font font, Rectangle rectangle, Color color)
        {
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new RenderDeviceException("GdiRenderer needs a GdiFont resource.");
            }
            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(color.ToWin32Color()),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public override void DrawString(string text, Font font, Vector2 position, Color color)
        {
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new RenderDeviceException("GdiRenderer needs a GdiFont resource.");
            }

            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(color.ToWin32Color()),
                position.ToPointF());
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public override void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1)
        {
            var gdiTexture = texture as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);

                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, tempBmp.Width, tempBmp.Height), 0,
                    0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, gdiTexture.Bmp.Width,
                        gdiTexture.Bmp.Height), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public override void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1)
        {
            var gdiTexture = texture as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                        (int) rectangle.Height), 0, 0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                        (int) rectangle.Height), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public override Vector2 MeasureString(string text, Font font)
        {
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new InvalidOperationException("GdiRenderer needs a GdiFont resource.");
            }

            SizeF result = _buffergraphics.MeasureString(text, gdifont.GetFont());

            return new Vector2(result.Width, result.Height);
        }

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public override void SetTransform(Matrix2x3 matrix)
        {
            _buffergraphics.Transform = new Matrix(matrix[0, 0], matrix[1, 0], matrix[0, 1],
                matrix[1, 1], matrix.OffsetX, matrix.OffsetY);
        }

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        public override void ResetTransform()
        {
            _buffergraphics.ResetTransform();
        }

        /// <summary>
        ///     Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public override void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawRectangle(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public override void DrawLine(Pen pen, Vector2 start, Vector2 target)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawLine(gdiPen.GetPen(), start.X, start.Y, target.X, target.Y);
        }

        /// <summary>
        ///     Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public override void DrawEllipse(Pen pen, Ellipse ellipse)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawEllipse(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) ellipse.Position.X, (int) ellipse.Position.Y, (int) ellipse.RadiusX*2,
                    (int) ellipse.RadiusY*2));
        }

        /// <summary>
        ///     Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public override void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawArc(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height), startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public override void DrawPolygon(Pen pen, Polygon polygon)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawPolygon(gdiPen.GetPen(), polygon.Points.ToPoints());
        }

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public override void FillRectangle(Color color, Rectangle rectangle)
        {
            _buffergraphics.FillRectangle(new SolidBrush(color.ToWin32Color()),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public override void FillEllipse(Color color, Ellipse ellipse)
        {
            _buffergraphics.FillEllipse(new SolidBrush(color.ToWin32Color()),
                new System.Drawing.Rectangle((int) ellipse.Position.X, (int) ellipse.Position.Y, (int) ellipse.RadiusX*2,
                    (int) ellipse.RadiusY*2));
        }

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public override void FillPolygon(Color color, Polygon polygon)
        {
            _buffergraphics.FillPolygon(new SolidBrush(color.ToWin32Color()), polygon.Points.ToPoints());
        }

        /// <summary>
        ///     Presents the Frame.
        /// </summary>
        private void Present()
        {
            Control control = Control.FromHandle(GraphicsDevice.RenderTarget.Handle);
            if (control != null)
            {
                int width = control.Width;
                int height = control.Height;
                if (!GraphicsDevice.BackBuffer.Scaling)
                {
                    width = GraphicsDevice.BackBuffer.Width;
                    height = GraphicsDevice.BackBuffer.Height;
                }
                Graphics graphics = control.CreateGraphics();
                IntPtr hdc = graphics.GetHdc();
                IntPtr intPtr = NativeMethods.CreateCompatibleDC(hdc);
                IntPtr hbitmap = _buffer.GetHbitmap();
                NativeMethods.SelectObject(intPtr, hbitmap);
                NativeMethods.StretchBlt(hdc, 0, 0, width, height, intPtr, 0, 0, GraphicsDevice.BackBuffer.Width,
                    GraphicsDevice.BackBuffer.Height, NativeMethods.GdiRasterOperations.SRCCOPY);
                NativeMethods.DeleteObject(hbitmap);
                NativeMethods.DeleteDC(intPtr);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
        }

        /// <summary>
        ///     Tints the Bitmap.
        /// </summary>
        /// <param name="sourceBitmap">The SourceBitmap.</param>
        /// <param name="blueTint">The BlueTint.</param>
        /// <param name="greenTint">The GreenTint.</param>
        /// <param name="redTint">The RedTint.</param>
        /// <returns>The Bitmap.</returns>
        private Bitmap ColorTint(Bitmap sourceBitmap, float blueTint, float greenTint, float redTint)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            var pixelBuffer = new byte[sourceData.Stride*sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                float blue = pixelBuffer[k] + (255 - pixelBuffer[k])*blueTint;
                float green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1])*greenTint;
                float red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2])*redTint;


                if (blue > 255)
                {
                    blue = 255;
                }


                if (green > 255)
                {
                    green = 255;
                }


                if (red > 255)
                {
                    red = 255;
                }


                pixelBuffer[k] = (byte) blue;
                pixelBuffer[k + 1] = (byte) green;
                pixelBuffer[k + 2] = (byte) red;
            }


            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }
    }
}