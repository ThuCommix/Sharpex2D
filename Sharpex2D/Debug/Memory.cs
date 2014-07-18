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

namespace Sharpex2D.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public struct Memory
    {
        /// <summary>
        /// Gets the Size.
        /// </summary>
        public long Size { private set; get; }

        /// <summary>
        /// Gets the Size.
        /// </summary>
        public float SizeF { private set; get; }

        /// <summary>
        /// Gets the Unit.
        /// </summary>
        public MemoryUnit Unit { private set; get; }

        private readonly long _rawSize;
        private readonly MemoryUnit _rawUnit;

        /// <summary>
        /// Initializes a new Memory struct.
        /// </summary>
        /// <param name="size">The Size.</param>
        /// <param name="unit">The MemoryUnit.</param>
        public Memory(long size, MemoryUnit unit) : this()
        {
            _rawSize = size;
            _rawUnit = unit;
            Unit = _rawUnit;
            Size = _rawSize;
            SizeF = _rawSize;
        }

        /// <summary>
        /// Converts the Size.
        /// </summary>
        /// <param name="targetUnit">The MemoryUnit.</param>
        public void Convert(MemoryUnit targetUnit)
        {
            Unit = targetUnit;
            if (targetUnit > _rawUnit)
            {
                Size = _rawSize/((long) targetUnit/(long) _rawUnit);
                SizeF = _rawSize / ((float)targetUnit / (float)_rawUnit);
            }
            else
            {
                Size = _rawSize * ((long)targetUnit * (long)_rawUnit);
                SizeF = _rawSize * ((float)targetUnit * (float)_rawUnit);
            }
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        /// <param name="m1">The first Memory.</param>
        /// <param name="m2">The second Memory.</param>
        /// <returns>Memory.</returns>
        public static Memory operator +(Memory m1, Memory m2)
        {
            var m1C = new Memory(m1._rawSize, m1._rawUnit);
            var m2C = new Memory(m2._rawSize, m2._rawUnit);
            m1C.Convert(MemoryUnit.Byte);
            m2C.Convert(MemoryUnit.Byte);

            return new Memory(m1C.Size + m2C.Size, MemoryUnit.Byte);
        }

        /// <summary>
        /// Substract operator.
        /// </summary>
        /// <param name="m1">The first Memory.</param>
        /// <param name="m2">The second Memory.</param>
        /// <returns>Memory.</returns>
        public static Memory operator -(Memory m1, Memory m2)
        {
            var m1C = new Memory(m1._rawSize, m1._rawUnit);
            var m2C = new Memory(m2._rawSize, m2._rawUnit);
            m1C.Convert(MemoryUnit.Byte);
            m2C.Convert(MemoryUnit.Byte);

            return new Memory(m1C.Size - m2C.Size, MemoryUnit.Byte);
        }

        /// <summary>
        /// Multiply operator.
        /// </summary>
        /// <param name="m1">The first Memory.</param>
        /// <param name="m2">The second Memory.</param>
        /// <returns>Memory.</returns>
        public static Memory operator *(Memory m1, Memory m2)
        {
            var m1C = new Memory(m1._rawSize, m1._rawUnit);
            var m2C = new Memory(m2._rawSize, m2._rawUnit);
            m1C.Convert(MemoryUnit.Byte);
            m2C.Convert(MemoryUnit.Byte);

            return new Memory(m1C.Size * m2C.Size, MemoryUnit.Byte);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        /// <param name="m1">The first Memory.</param>
        /// <param name="m2">The second Memory.</param>
        /// <returns>Memory.</returns>
        public static Memory operator /(Memory m1, Memory m2)
        {
            var m1C = new Memory(m1._rawSize, m1._rawUnit);
            var m2C = new Memory(m2._rawSize, m2._rawUnit);
            m1C.Convert(MemoryUnit.Byte);
            m2C.Convert(MemoryUnit.Byte);

            return new Memory(m1C.Size - m2C.Size, MemoryUnit.Byte);
        }
    }
}
