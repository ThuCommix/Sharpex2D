// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System;

namespace Sharpex2D.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public unsafe struct Matrix2x3
    {
        /// <summary>
        /// The identity matrix.
        /// </summary>
        public static readonly Matrix2x3 Identity;

        private fixed float _values [6];

        /// <summary>
        /// Initializes a new Matrix2x3 class.
        /// </summary>
        static Matrix2x3()
        {
            Matrix2x3 m = default(Matrix2x3);
            m._values[0] = 1.0f;
            m._values[4] = 1.0f;
            Identity = m;
        }

        /// <summary>
        /// Returns or sets the matrix element at the given Position.
        /// </summary>
        /// <returns>Float.</returns>
        public float this[int x, int y]
        {
            get
            {
                fixed (float* values = _values)
                    return values[x*3 + y];
            }
            set
            {
                if (x < 0 || x > 1 || y < 0 || y > 2)
                    throw new IndexOutOfRangeException();

                fixed (float* values = _values)
                    values[x*3 + y] = value;
            }
        }

        /// <summary>
        /// Sets or gets the OffsetX.
        /// </summary>
        public float OffsetX
        {
            get { fixed (float* values = _values) return values[2]; }
            set { fixed (float* values = _values) values[2] = value; }
        }

        /// <summary>
        /// Sets or gets the OffsetY.
        /// </summary>
        public float OffsetY
        {
            get { fixed (float* values = _values) return values[5]; }
            set { fixed (float* values = _values) values[5] = value; }
        }

        /// <summary>
        /// Gets the Determinant.
        /// </summary>
        public float Determinant
        {
            get
            {
                fixed (float* values = _values)
                    return values[0]*values[4] - values[1]*values[3];
            }
        }

        /// <summary>
        /// A value indicating whether this matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                fixed (float* values = _values)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int rem;
                        int div = System.Math.DivRem(i, 3, out rem);
                        if (System.Math.Abs(values[i] - (div == rem ? 1 : 0)) > MathHelper.Epsilon)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Applies this matrix to a point.
        /// </summary>
        /// <returns>Vector2.</returns>
        public Vector2 ApplyTo(Vector2 point)
        {
            fixed (float* values = _values)
            {
                var result = new Vector2(0, 0);
                result.X = point.X*values[0] + point.Y*values[1] + values[2];
                result.Y = point.X*values[3] + point.Y*values[4] + values[5];
                return result;
            }
        }

        /// <summary>
        /// Applies this matrix to an array of vectors.
        /// </summary>
        /// <returns>Vector2 Array.</returns>
        public Vector2[] ApplyTo(Vector2[] points)
        {
            var result = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++)
                result[i] = ApplyTo(points[i]);
            return result;
        }

        /// <summary>
        /// Multiplies the rows.
        /// </summary>
        /// <param name="row">The Row.</param>
        /// <param name="factor">The Factor.</param>
        private void MultiplyRow(int row, float factor)
        {
            int startIndex = row*3;
            int endIndex = startIndex + 3;
            fixed (float* values = _values)
            {
                for (int i = startIndex; i < endIndex; i++)
                    values[i] *= factor;
            }
        }

        /// <summary>
        /// Swaps the rows.
        /// </summary>
        private void SwapRows()
        {
            fixed (float* values = _values)
            {
                for (int i = 0; i < 3; i++)
                {
                    float temp = values[i];
                    values[i] = values[i + 3];
                    values[i + 3] = temp;
                }
            }
        }

        /// <summary>
        /// Subtracts the rows.
        /// </summary>
        /// <param name="row1">The first Row.</param>
        /// <param name="row2">The second Row.</param>
        /// <param name="factor">The Factor.</param>
        private void SubtractRows(int row1, int row2, float factor)
        {
            int row1Start = row1*3;
            int row2Start = row2*3;
            fixed (float* values = _values)
            {
                for (int i = 0; i < 3; i++)
                    values[row1Start + i] -= values[row2Start + i]*factor;
            }
        }

        /// <summary>
        /// Returns the inverse matrix of this matrix.
        /// </summary>
        /// <returns>Matrix2x3.</returns>
        public Matrix2x3 Invert()
        {
            Matrix2x3 m1 = this;
            Matrix2x3 m2 = Identity;

            if (m1._values[0] == 0 || m1._values[4] == 0)
            {
                if (m1._values[1] == 0 || m1._values[3] == 0)
                    throw new InvalidOperationException("This matrix is singular.");

                m1.SwapRows();
                m2.SwapRows();
            }

            if (m1._values[3] != 0)
            {
                float factor = m1._values[0]/m1._values[3];
                m1.SubtractRows(1, 0, factor);
                m2.SubtractRows(1, 0, factor);
            }
            if (m1._values[1] != 0)
            {
                float factor = m1._values[4]/m1._values[1];
                m1.SubtractRows(0, 1, factor);
                m2.SubtractRows(0, 1, factor);
            }

            float f1 = 1/m1._values[0];
            m1.MultiplyRow(0, f1);
            m2.MultiplyRow(0, f1);
            float f2 = 1/m1._values[4];
            m1.MultiplyRow(1, f2);
            m2.MultiplyRow(1, f2);
            m2.OffsetX = -m1.OffsetX;
            m2.OffsetY = -m1.OffsetY;

            return m2;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="factorX">The X Factor.</param>
        /// <param name="factorY">The Y Factor.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Scaling(float factorX, float factorY)
        {
            Matrix2x3 m = default(Matrix2x3);
            m._values[0] = factorX;
            m._values[4] = factorY;
            return m;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="factor">The Factor.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Scaling(float factor)
        {
            return Scaling(factor, factor);
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Translation(float x, float y)
        {
            Matrix2x3 m = Identity;
            m._values[2] = x;
            m._values[5] = y;
            return m;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="vector">The Vector.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Translation(Vector2 vector)
        {
            Matrix2x3 m = Identity;
            m._values[2] = vector.X;
            m._values[5] = vector.Y;
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix.
        /// </summary>
        /// <param name="angle">The Angle.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Rotation(float angle)
        {
            float sin = MathHelper.Sin(angle);
            float cos = MathHelper.Cos(angle);

            Matrix2x3 m = default(Matrix2x3);
            m._values[0] = cos;
            m._values[1] = -sin;
            m._values[3] = sin;
            m._values[4] = cos;
            return m;
        }

        /// <summary>
        /// Creates a shearing matrix.
        /// </summary>
        /// <param name="value">The Value.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 ShearingX(float value)
        {
            Matrix2x3 m = Identity;
            m._values[1] = value;
            return m;
        }

        /// <summary>
        /// Creates a shearing matrix.
        /// </summary>
        /// <param name="value">The Value.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 ShearingY(float value)
        {
            Matrix2x3 m = Identity;
            m._values[3] = value;
            return m;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 Multiply(Matrix2x3 left, Matrix2x3 right)
        {
            Matrix2x3 m = default(Matrix2x3);
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    m[x, y] = left[x, 0]*right[0, y] + left[x, 1]*right[1, y] + (y == 2 ? left[x, 2] : 0);
                }
            }
            return m;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>Matrix2x3.</returns>
        public static Matrix2x3 operator *(Matrix2x3 left, Matrix2x3 right)
        {
            return Multiply(left, right);
        }
    }
}