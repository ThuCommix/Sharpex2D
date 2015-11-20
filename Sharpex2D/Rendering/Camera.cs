// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Sharpex2D.Framework.Rendering
{
    public class Camera
    {
        private Rectangle _bounds;

        /// <summary>
        /// Gets or sets the bounds
        /// </summary>
        public Rectangle Bounds
        {
            set { _bounds = value; }
            get { return _bounds; }
        }

        /// <summary>
        /// Gets or sets the position
        /// </summary>
        public Vector2 Position
        {
            set
            {
                _bounds.X = value.X;
                _bounds.Y = value.Y;
            }
            get { return new Vector2(Bounds.X, Bounds.Y); }
        }

        /// <summary>
        /// Gets or sets the center
        /// </summary>
        public Vector2 Center
        {
            get { return _bounds.Center; }
            set
            {
                _bounds.X = value.X - (_bounds.Width/2f);
                _bounds.Y = value.Y - (_bounds.Height/2f);
            }
        }

        /// <summary>
        /// Gets or sets the velocity
        /// </summary>
        public Vector2 Velocity { set; get; }

        /// <summary>
        /// A value indicating whether the camera has invalid bounds
        /// </summary>
        public bool Invalid => Bounds.Width < 1 && Bounds.Height < 1;

        /// <summary>
        /// Gets or sets the zoom
        /// </summary>
        public float Zoom { set; get; }

        /// <summary>
        /// Gets or sets the rotation
        /// <remarks>Values in radians</remarks>
        /// </summary>
        public float Rotation { set; get; }

        /// <summary>
        /// Gets the transformation
        /// </summary>
        public Matrix Transformation
            => Matrix.Translation(Vector2.Negate(Position))*Matrix.Rotation(Rotation)*Matrix.Scaling(Zoom);

        /// <summary>
        /// Initializes a new Camera class
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public Camera(int width, int height) : this(new Rectangle(0, 0, width, height))
        {
            
        }

        /// <summary>
        /// Initializes a new Camera class
        /// </summary>
        /// <param name="position">The position</param>
        public Camera(Vector2 position) : this(new Rectangle(position, Vector2.Zero))
        {
            
        }

        /// <summary>
        /// Initializes a new Camera class
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public Camera(Vector2 position, int width, int height)
            : this(new Rectangle(position, new Vector2(width, height)))
        {

        }

        /// <summary>
        /// Initializes a new Camera class
        /// </summary>
        /// <param name="bounds">The bounds</param>
        public Camera(Rectangle bounds)
        {
            _bounds = bounds;
            Zoom = 1f;
            Rotation = 0f;
            Velocity = Vector2.Zero;
        }

        /// <summary>
        /// Looks at the specified position
        /// </summary>
        /// <param name="position">The position</param>
        public void LookAt(Vector2 position)
        {
            Center = position;
        }

        /// <summary>
        /// Looks at the specified position
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="rotation">The rotation</param>
        /// <param name="zoom">The zoom</param>
        public void LookAt(Vector2 position, float rotation, float zoom)
        {
            Center = position;
            Rotation = rotation;
            Zoom = zoom;
        }

        /// <summary>
        /// Creates a new camera view based on the specified arguments
        /// </summary>
        /// <param name="bounds">The bounds</param>
        /// <param name="rotation">The rotation</param>
        /// <param name="zoom">The zoom</param>
        /// <returns>Returns a camera</returns>
        public static Camera CreateLookAt(Rectangle bounds, float rotation, float zoom)
        {
            return new Camera(bounds) {Center = new Vector2(bounds.X, bounds.Y), Rotation = rotation, Zoom = zoom};
        }
    }
}
