using System;
using System.Text;

namespace Sharpex2D.Framework.Math
{
    public class Matrix : ICloneable
    {
        #region Matrix
        /// <summary>
        /// Initializes a new Matrix class.
        /// </summary>
        /// <param name="columns">The amount of Columns.</param>
        /// <param name="rows">The amount of Rows.</param>
        public Matrix(int columns, int rows)
        {
            _fields = new double[columns, rows];
            for (var x = 0; x <= columns - 1; x++)
            {
                for (var y = 0; y <= rows - 1; y++)
                {
                    _fields[x, y] = 0;
                }
            }
            _columns = columns;
            _rows = rows;
        }

        private readonly double[,] _fields;
        private readonly int _columns;
        private readonly int _rows;

        /// <summary>
        /// Gets the amount of columns.
        /// </summary>
        public int Columns
        {
            get { return _columns; }
        }
        /// <summary>
        /// Gets the amount of rows.
        /// </summary>
        public int Rows
        {
            get
            {
                return _rows;
            }
        }


        /// <summary>
        /// Sets a value of the element.
        /// </summary>
        /// <param name="column">The Column.</param>
        /// <param name="row">The Row.</param>
        /// <param name="value">The Value.</param>
        public void Set(int column, int row, double value)
        {
            if (column  > _columns || column  < 0)
            {
                throw new ArgumentOutOfRangeException("column");
            }
            if (row  > _rows || row  < 0)
            {
                throw new ArgumentOutOfRangeException("row");
            }

            _fields[column, row] = value;
        }

        /// <summary>
        /// Gets the value of the element.
        /// </summary>
        /// <param name="column">The Column.</param>
        /// <param name="row">The Row.</param>
        /// <returns>The value of the element</returns>
        public double Get(int column, int row)
        {
            if (column  > _columns || column  < 0)
            {
                throw new ArgumentOutOfRangeException("column");
            }
            if (row  > _rows || row < 0)
            {
                throw new ArgumentOutOfRangeException("row");
            }

            return _fields[column, row];
        }

        #region Operator

        /// <summary>
        /// Addition of two matrices.
        /// </summary>
        /// <param name="a">The first Matrix.</param>
        /// <param name="b">The second Matrix.</param>
        /// <returns>Matrix</returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            if (a.Columns != b.Columns || a.Rows != b.Rows)
                throw new InvalidOperationException("The two matrices needs to have the same size.");

            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) + b.Get(x, y));
                }
            }

            return result;
        }
        /// <summary>
        /// Substraction of two matrices.
        /// </summary>
        /// <param name="a">The first Matrix.</param>
        /// <param name="b">The second Matrix.</param>
        /// <returns>Matrix</returns>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            if (a.Columns != b.Columns || a.Rows != b.Rows)
                throw new InvalidOperationException("The two matrices needs to have the same size.");

            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) - b.Get(x, y));
                }
            }

            return result;
        }
        /// <summary>
        /// Multiplys two matrices.
        /// </summary>
        /// <param name="a">The first Matrix.</param>
        /// <param name="b">The second Matrix.</param>
        /// <returns>Matrix</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            if (a.Columns != b.Columns || a.Rows != b.Rows)
                throw new InvalidOperationException("The two matrices needs to have the same size.");

            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) * b.Get(x, y));
                }
            }

            return result;
        }
        /// <summary>
        /// Scalarmultiply with a matrix.
        /// </summary>
        /// <param name="a">The Matrix.</param>
        /// <param name="scalar">The Scalar</param>
        /// <returns>Matrix</returns>
        public static Matrix operator *(Matrix a, double scalar)
        {
            CheckForNull(a);
            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) * scalar);
                }
            }

            return result;
        }
        /// <summary>
        /// Scalarmultiply with a matrix.
        /// </summary>
        /// <param name="a">The Matrix.</param>
        /// <param name="scalar">The Scalar</param>
        /// <returns>Matrix</returns>
        public static Matrix operator *(double scalar, Matrix a)
        {
            CheckForNull(a);
            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) * scalar);
                }
            }

            return result;
        }
        /// <summary>
        /// Divide two matrices.
        /// </summary>
        /// <param name="a">The first Matrix.</param>
        /// <param name="b">The second Matrix.</param>
        /// <returns>Matrix</returns>
        public static Matrix operator /(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            if (a.Columns != b.Columns || a.Rows != b.Rows)
                throw new InvalidOperationException("The two matrices needs to have the same size.");

            var result = new Matrix(a.Columns, a.Rows);

            for (var y = 0; y <= a.Rows - 1; y++)
            {
                for (var x = 0; x <= a.Columns - 1; x++)
                {
                    result.Set(x, y, a.Get(x, y) / b.Get(x, y));
                }
            }

            return result;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            return a.Equals(b);
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            CheckForNull(a, b);
            return !a.Equals(b);
        }

        /// <summary>
        /// Addition with another matrix.
        /// </summary>
        /// <param name="other">The Matrix.</param>
        /// <returns>Matrix</returns>
        public Matrix Addition(Matrix other)
        {
            return this + other;
        }
        /// <summary>
        /// Substract with another matrix.
        /// </summary>
        /// <param name="other">The Matrix.</param>
        /// <returns>Matrix</returns>
        public Matrix Subtract(Matrix other)
        {
            return this - other;
        }
        /// <summary>
        /// Multiply with another matrix.
        /// </summary>
        /// <param name="other">The Matrix.</param>
        /// <returns>Matrix</returns>
        public Matrix Multiply(Matrix other)
        {
            return this * other;
        }
        /// <summary>
        /// Multiply with a scalar.
        /// </summary>
        /// <param name="scalar">The Scalar.</param>
        /// <returns>Matrix</returns>
        public Matrix Multiply(double scalar)
        {
            return this * scalar;
        }
        /// <summary>
        /// Divide with another matrix.
        /// </summary>
        /// <param name="other">The Matrix.</param>
        /// <returns>Matrix</returns>
        public Matrix Divide(Matrix other)
        {
            return this / other;
        }
        /// <summary>
        /// Pow the matrix.
        /// </summary>
        /// <param name="exponent">The Exponent.</param>
        /// <returns>Matrix</returns>
        public Matrix Pow(double exponent)
        {
            var result = new Matrix(Columns, Rows);
            for (var y = 0; y <= Rows - 1; y++)
            {
                for (var x = 0; x <= Columns - 1; x++)
                {
                    result.Set(x, y, MathHelper.Pow((float) Get(x, y), (float) exponent));
                }
            }
            return result;
        }
        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <returns>Matrix</returns>
        public Matrix Transpose()
        {
            var result = new Matrix(Rows, Columns);
            for (var y = 0; y <= Rows - 1; y++)
            {
                for (var x = 0; x <= Columns - 1; x++)
                {
                    result.Set(y, x, Get(x, y));
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Converts the Matrix in to a string.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = 0; y <= Rows - 1; y++)
            {
                for (var x = 0; x <= Columns - 1; x++)
                {
                    if (x == Columns - 1)
                    {
                        sb.Append(Get(x, y));
                    }
                    else
                    {
                        sb.Append(Get(x, y) + ", ");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check if another matrix is equal to the current matrix.
        /// </summary>
        /// <param name="other">The Matrix.</param>
        /// <returns></returns>
        protected bool Equals(Matrix other)
        {
            return Equals(_fields, other._fields) && _columns == other._columns && _rows == other._rows;
        }

        /// <summary>
        /// Check if another matrix is equal to the current matrix.
        /// </summary>
        /// <param name="obj">The Matrix.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Matrix) obj);
        }

        /// <summary>
        /// Clones the Matrix.
        /// </summary>
        /// <returns>Object</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Gets the HashCode.
        /// </summary>
        /// <returns>Int32</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_fields != null ? _fields.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _columns;
                hashCode = (hashCode * 397) ^ _rows;
                return hashCode;
            }
        }

        /// <summary>
        /// Checks for Null.
        /// </summary>
        /// <param name="matrices">The Matrices.</param>
        private static void CheckForNull(params Matrix[] matrices)
        {
            foreach (var matrix in matrices)
            {
                if (matrix == null)
                {
                    throw new ArgumentNullException("The matrix can not be null.");
                }
            }
        }

        /// <summary>
        /// Copys the current Matrix to another matrix.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void CopyTo(Matrix matrix)
        {
            if (Columns == matrix.Columns && Rows == matrix.Rows)
            {
                for(var y =0;y<= Rows -1;y++)
                {
                    for (var x = 0;x <= Columns - 1;x++)
                    {
                        matrix.Set(x, y, Get(x, y));
                    }
                }
            }
            else
            {
                throw new ArgumentException("The matrices needs to have the same size.");
            }
        }

        /// <summary>
        /// Resizes the matrix.
        /// </summary>
        /// <param name="columns">The Columns.</param>
        /// <param name="rows">The Rows.</param>
        /// <returns>Matrix</returns>
        public Matrix Resize(int columns, int rows)
        {
            var result = new Matrix(columns, rows);

            for (var y = 0; y <= rows - 1; y++)
            {
                for (var x = 0; x <= columns - 1; x++)
                {
                    if (x <= Columns - 1 && y <= Rows - 1)
                    {
                        result.Set(x, y, Get(x, y));
                    }
                    else
                    {
                        result.Set(x, y, 0);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
