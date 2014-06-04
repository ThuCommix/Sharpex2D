using System;

namespace Sharpex2D.Framework.Common.Randomization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameRandom
    {
        private readonly Random _random;

        /// <summary>
        ///     Initializes a new GameRandom.
        /// </summary>
        public GameRandom()
        {
            _random = new Random();
        }

        /// <summary>
        ///     Returns Int32 between Int32 min - and max value.
        /// </summary>
        /// <returns>Int</returns>
        public int Next()
        {
            return _random.Next(-2147483648, 2147483647);
        }

        /// <summary>
        ///     Returns Int32 between 0 and max.
        /// </summary>
        /// <param name="max">The Maximum.</param>
        /// <returns>Int</returns>
        public int Next(int max)
        {
            return _random.Next(0, max);
        }

        /// <summary>
        ///     Returns Int32 between min and max.
        /// </summary>
        /// <param name="min">The Minimum.</param>
        /// <param name="max">The Maximum.</param>
        /// <returns>Int</returns>
        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        ///     Returns a float between 0 and 1.
        /// </summary>
        /// <returns>Float</returns>
        public float NextFloat()
        {
            return (float) _random.NextDouble();
        }

        /// <summary>
        ///     Returns a double between 0 and 1.
        /// </summary>
        /// <returns>Double</returns>
        public double NextDouble()
        {
            return _random.NextDouble();
        }

        /// <summary>
        ///     Returns true or false.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool NextBoolean()
        {
            return NextBoolean(0.5);
        }

        /// <summary>
        ///     Returns true or false based on the probability.
        /// </summary>
        /// <param name="probability">The Probability.</param>
        /// <returns>Boolean</returns>
        public bool NextBoolean(double probability)
        {
            return _random.NextDouble() <= probability;
        }
    }
}