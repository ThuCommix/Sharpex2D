using System.Collections.Generic;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    internal static class DirectXHelper
    {
        /// <summary>
        ///     Gets the Direct3D9Device.
        /// </summary>
        public static Device Direct3D9 { set; get; }

        /// <summary>
        ///     Converts the Color into a DXColor.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>DXColor.</returns>
        public static Color4 ConvertColor(Color color)
        {
            return new Color4(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
        }

        /// <summary>
        ///     Converts the Vector into a texture based rectangle.
        /// </summary>
        /// <param name="position">The Position.</param>
        /// <param name="texture">The Texture.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle ConvertVectorToTextureRectangle(Vector2 position,
            Texture2D texture)
        {
            return new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        /// <summary>
        ///     Converts a Rectangle into a WinRectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle ConvertToWinRectangle(Math.Rectangle rectangle)
        {
            return new Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                (int) rectangle.Height);
        }

        /// <summary>
        ///     Converts the Vector2 into DXVector3.
        /// </summary>
        /// <param name="vector2">The Vector2.</param>
        /// <returns>Vector3.</returns>
        public static Vector3 ConvertVector2(Vector2 vector2)
        {
            return new Vector3(vector2.X, vector2.Y, 0);
        }

        /// <summary>
        ///     Converts the vertex into DXVertex3.
        /// </summary>
        /// <param name="vertex">The Vertex.</param>
        /// <returns>Vector2 Array.</returns>
        public static SlimDX.Vector2[] ConvertToVertex(params Vector2[] vertex)
        {
            var vertexList = new List<SlimDX.Vector2>();

            foreach (Vector2 vector in vertex)
            {
                vertexList.Add(new SlimDX.Vector2(vector.X, vector.Y));
            }

            return vertexList.ToArray();
        }
    }
}