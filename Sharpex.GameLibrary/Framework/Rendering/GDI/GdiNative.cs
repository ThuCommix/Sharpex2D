using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.GDI
{
    public static class GdiNative
    {
        public enum GdiRasterOperations
        {
            SRCCOPY = 13369376,
            SRCPAINT = 15597702,
            SRCAND = 8913094,
            SRCINVERT = 6684742,
            SRCERASE = 4457256,
            NOTSRCCOPY = 3342344,
            NOTSRCERASE = 1114278,
            MERGECOPY = 12583114,
            MERGEPAINT = 12255782,
            PATCOPY = 15728673,
            PATPAINT = 16452105,
            PATINVERT = 5898313,
            DSTINVERT = 5570569,
            BLACKNESS = 66,
            WHITENESS = 16711778
        }
        #region GDI Methods
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, GdiRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, GdiRasterOperations dwRop);
        #endregion

        #region Methods
        /// <summary>
        /// Converts the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public static Color Convert(Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        /// <summary>
        /// Converts the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static Math.Rectangle Convert(Math.Rectangle rectangle)
        {
            return new Math.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public static Point[] Convert(Vector2[] vertices)
        {
            Point[] points = new Point[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                points[i] = new Point((int)vertices[i].X, (int)vertices[i].Y);
            }

            return points;
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="offset">The offset.</param>
        public static Point[] Convert(Vector2[] vertices, Vector2 offset)
        {
            return GdiNative.Convert(vertices.Select(v => v + offset).ToArray());
        }
        #endregion
    }
}
