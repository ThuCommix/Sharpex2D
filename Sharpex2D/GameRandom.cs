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
    public class GameRandom
    {
        private readonly Random _random;

        /// <summary>
        /// Initializes a new GameRandom.
        /// </summary>
        public GameRandom()
        {
            _random = new Random();
        }

        /// <summary>
        /// Returns Int32 between Int32 min - and max value.
        /// </summary>
        /// <returns>Int</returns>
        public int Next()
        {
            return _random.Next(-2147483648, 2147483647);
        }

        /// <summary>
        /// Returns Int32 between 0 and max.
        /// </summary>
        /// <param name="max">The Maximum.</param>
        /// <returns>Int</returns>
        public int Next(int max)
        {
            return _random.Next(0, max);
        }

        /// <summary>
        /// Returns Int32 between min and max.
        /// </summary>
        /// <param name="min">The Minimum.</param>
        /// <param name="max">The Maximum.</param>
        /// <returns>Int</returns>
        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Returns a float between 0 and 1.
        /// </summary>
        /// <returns>Float</returns>
        public float NextFloat()
        {
            return (float) _random.NextDouble();
        }

        /// <summary>
        /// Returns a double between 0 and 1.
        /// </summary>
        /// <returns>Double</returns>
        public double NextDouble()
        {
            return _random.NextDouble();
        }

        /// <summary>
        /// Returns true or false.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool NextBoolean()
        {
            return NextBoolean(0.5);
        }

        /// <summary>
        /// Returns true or false based on the probability.
        /// </summary>
        /// <param name="probability">The Probability.</param>
        /// <returns>Boolean</returns>
        public bool NextBoolean(double probability)
        {
            return _random.NextDouble() <= probability;
        }
    }
}
