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

using System;

namespace Sharpex2D.Framework
{
    public static class MathHelper
    {
        #region Constants

        /// <summary>
        /// Natural base for logarithms.
        /// </summary>
        public const float E = (float) Math.E;

        /// <summary>
        /// Natural base for logarithms.
        /// </summary>
        public const float Log10E = 0.4342945f;

        /// <summary>
        /// Natural base for logarithms.
        /// </summary>
        public const float Log2E = 1.442695f;

        /// <summary>
        /// π.
        /// </summary>
        public const float Pi = (float) Math.PI;

        /// <summary>
        /// ε.
        /// </summary>
        public const float Epsilon = 5.960464E-8f;

        /// <summary>
        /// π divided by 2.
        /// </summary>
        public const float PiOver2 = (float) (Math.PI*0.5);

        /// <summary>
        /// π divided by 4.
        /// </summary>
        public const float PiOver4 = (float) (Math.PI*0.25);

        /// <summary>
        /// Two times π.
        /// </summary>
        public const float TwoPi = 2*(float) Math.PI;

        /// <summary>
        /// 180 divided by π.
        /// </summary>
        public const double OneEightyOverPi = 180.0/Math.PI;

        /// <summary>
        /// π divided by 180.
        /// </summary>
        public const double PiOverOneEighty = Math.PI/180.0;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the sinus for the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float Sin(float value)
        {
            return (float) Math.Sin(value);
        }

        /// <summary>
        /// Returns the cosinus for the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float Cos(float value)
        {
            return (float) Math.Cos(value);
        }

        /// <summary>
        /// Returns the square root for the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static float Sqrt(float value)
        {
            return (float) Math.Sqrt(value);
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static float Round(float value)
        {
            return (float) Math.Round(value);
        }

        /// <summary>
        /// Calculates an angle out of given coordinates.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static float ToAngle(float x, float y)
        {
            return (float) Math.Atan2(y, x) + Pi;
        }

        /// <summary>
        /// Calculates an angle out of given coordinates.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static float ToAngle(Vector2 vector)
        {
            return ToAngle(vector.X, vector.Y);
        }

        /// <summary>
        /// Converts an angle to a direction vector.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public static Vector2 ToVector(float angle)
        {
            return new Vector2(Cos(angle), Sin(angle));
        }

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized
        /// barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="value3">The value3.</param>
        /// <param name="amount1">The amount1.</param>
        /// <param name="amount2">The amount2.</param>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1)*amount1 + (value3 - value1)*amount2;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="value3">The value3.</param>
        /// <param name="value4">The value4.</param>
        /// <param name="amount">The amount.</param>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/

            double amountSquared = amount*amount;
            double amountCubed = amountSquared*amount;

            return (float) (0.5*(2.0*value2 + (value3 - value1)*amount +
                                 (2.0*value1 - 5.0*value2 + 4.0*value3 - value4)*amountSquared +
                                 (3.0*value2 - value1 - 3.0*value3 + value4)*amountCubed));
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public static float Clamp(float value, float min, float max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;

            return value;
        }

        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="tangent1">The tangent1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="tangent2">The tangent2.</param>
        /// <param name="amount">The amount.</param>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float a2 = amount*amount;
            float asqr3 = amount*a2;
            float a3 = a2 + a2 + a2;

            return (value1*(((asqr3 + asqr3) - a3) + 1f)) +
                   (value2*((-2f*asqr3) + a3)) +
                   (tangent1*((asqr3 - (a2 + a2)) + amount)) +
                   (tangent2*(asqr3 - a2));
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="amount">The amount.</param>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1)*amount;
        }

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public static float Max(float value1, float value2)
        {
            return Math.Max(value1, value2);
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public static float Min(float value1, float value2)
        {
            return Math.Min(value1, value2);
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="amount">The amount.</param>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            float result = Clamp(amount, 0.0f, 1.0f);
            result = Hermite(value1, 0.0f, value2, 0.0f, result);

            return result;
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The radians.</param>
        public static float ToDegrees(float radians)
        {
            return (float) (radians*OneEightyOverPi);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        public static float ToRadians(float degrees)
        {
            return (float) (degrees*PiOverOneEighty);
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public static float WrapAngle(float angle)
        {
            return (float) Math.IEEERemainder(angle, 6.2831854820251465);
        }

        /// <summary>
        /// Calculates x raised to the power of y.
        /// </summary>
        /// <param name="basis">The Basis.</param>
        /// <param name="exponent">The Exponent.</param>
        /// <returns></returns>
        public static float Pow(float basis, float exponent)
        {
            return (float) Math.Pow(basis, exponent);
        }

        /// <summary>
        /// Calculates the arctangent.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <returns>Float.</returns>
        public static float Atan2(float x, float y)
        {
            return (float) Math.Atan2(x, y);
        }

        #endregion
    }
}
