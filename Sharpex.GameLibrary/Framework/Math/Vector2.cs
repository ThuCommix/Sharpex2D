using System;
using System.Drawing;

namespace SharpexGL.Framework.Math
{
    #region Constructors

    [Serializable]
    public class Vector2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2(float value)
        {
            _x = value;
            _y = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2(float x, float y)
        {
            _x = x;
            _y = y;
        }
        #endregion

        #region Fields
        private float _x;
        private float _y;
        #endregion

        #region Vectors
        /// <summary>
        /// Returns a null vector.
        /// </summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }
        /// <summary>
        /// Returns a vector that represents the x-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return new Vector2(1, 0); }
        }
        /// <summary>
        /// Returns a vector that represents the y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return new Vector2(0, 1); }
        }
        #endregion

        #region Coordinates
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the length.
        /// </summary>
        public float Length
        {
            get { return MathHelper.Sqrt(LengthSquared); }
        }
        /// <summary>
        /// Gets the length squared.
        /// </summary>
        public float LengthSquared
        {
            get { return _x * _x + _y * _y; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            if (LengthSquared > 0)
            {
                float divider = 1.0f / Length;

                _x *= divider;
                _y *= divider;
            }
        }
        /// <summary>
        /// Translates the vector.
        /// </summary>
        /// <param name="translation">The translation.</param>
        public void Translate(Vector2 translation)
        {
            _x += translation.X;
            _y += translation.Y;
        }
        #endregion

        #region Vector Methods
        /// <summary>
        /// Truncates the specified vector and removes all decimal places.
        /// </summary>
        /// <param name="a">The vector.</param>
        public static Vector2 Truncate(Vector2 a)
        {
            return new Vector2((int)a.X, (int)a.Y);
        }
        /// <summary>
        /// Normalizes the specified vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        public static Vector2 Normalize(Vector2 a)
        {
            if (a.LengthSquared == 0)
                return Vector2.Zero;

            float divider = 1.0f / a.Length;
            return new Vector2(
                a.X * divider,
                a.Y * divider);
        }
        /// <summary>
        /// Returns the absolute values of the specified vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        public static Vector2 Abs(Vector2 a)
        {
            return new Vector2(
                MathHelper.Abs(a.X),
                MathHelper.Abs(a.Y));
        }
        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(
                MathHelper.Clamp(value.X, min.X, max.X),
                MathHelper.Clamp(value.Y, min.Y, max.Y));
        }
        /// <summary>
        /// Linear interpolation between the two values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="amount">The amount.</param>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float amount)
        {
            Vector2 result = Zero;

            result.X = MathHelper.Lerp(a.X, b.X, amount);
            result.Y = MathHelper.Lerp(a.Y, b.Y, amount);

            return result;
        }
        /// <summary>
        /// Rotates the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angle">The angle.</param>
        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            float rx = vector.X * MathHelper.Cos(angle) - vector.Y * MathHelper.Sin(angle);
            float ry = vector.X * MathHelper.Sin(angle) + vector.Y * MathHelper.Cos(angle);

            return new Vector2(rx, ry);
        }
        /// <summary>
        /// Rounds the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static Vector2 Round(Vector2 vector)
        {
            vector.X = MathHelper.Round(vector.X);
            vector.Y = MathHelper.Round(vector.Y);

            return vector;
        }
        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="amount">The amount.</param>
        public static Vector2 SmoothStep(Vector2 a, Vector2 b, float amount)
        {
            Vector2 result = Zero;

            result.X = MathHelper.SmoothStep(a.X, b.X, amount);
            result.Y = MathHelper.SmoothStep(a.Y, b.Y, amount);

            return result;
        }
        /// <summary>
        /// Returns the lesser components of the two values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            return new Vector2(
                MathHelper.Min(a.X, b.X),
                MathHelper.Min(a.Y, b.Y));
        }
        /// <summary>
        /// Returns the greater components of the two values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(
                MathHelper.Max(a.X, b.X),
                MathHelper.Max(a.Y, b.Y));
        }
        /// <summary>
        /// Returns the dot product for the specified vectors.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns></returns>
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        /// <summary>
        /// Reflects the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="normal">The normal.</param>
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            var result = Zero;
            var sub = 2 * Dot(vector, normal);

            result.X = vector.X - sub * normal.X;
            result.Y = vector.Y - sub * normal.Y;

            return result;
        }
        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static Vector2 Negate(Vector2 vector)
        {
            return new Vector2(-vector.X, -vector.Y);
        }
        #endregion

        #region Object Member
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        public override bool Equals(object obj)
        {
            return this == (Vector2)obj;
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return "{" + X.ToString() + "," + Y.ToString() + "}";
        }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return (a.X != b.X) || (a.Y != b.Y);
        }
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The vector.</param>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="scale">The scale.</param>
        public static Vector2 operator *(Vector2 a, float scale)
        {
            return new Vector2(a.X * scale, a.Y * scale);
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="scale">The scale.</param>
        public static Vector2 operator /(Vector2 a, float scale)
        {
            return new Vector2(a.X / scale, a.Y / scale);
        }
        #endregion

        /// <summary>
        /// Returns point based on the vector position.
        /// </summary>
        /// <returns>Point</returns>
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
        /// <summary>
        /// Returns point based on the vector position.
        /// </summary>
        /// <returns>PointF</returns>
        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }
    }
}
