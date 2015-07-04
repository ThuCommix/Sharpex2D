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

using System.Collections.Generic;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class MouseState
    {
        private readonly Dictionary<MouseButtons, bool> _reference;

        /// <summary>
        /// Initializes a new MouseState class.
        /// </summary>
        /// <param name="reference">The Reference.</param>
        /// <param name="position">The Position.</param>
        /// <param name="delta">The WheelDelta.</param>
        internal MouseState(Dictionary<MouseButtons, bool> reference, Vector2 position, int delta)
        {
            Position = new Vector2(position.X, position.Y);
            _reference = new Dictionary<MouseButtons, bool>();
            WheelDelta = delta;

            foreach (KeyValuePair<MouseButtons, bool> pair in reference)
            {
                _reference.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Gets the Position.
        /// </summary>
        public Vector2 Position { private set; get; }

        /// <summary>
        /// Gets the wheel delta.
        /// </summary>
        public int WheelDelta { get; private set; }

        /// <summary>
        /// A value indicating whether the MouseButton is pressed.
        /// </summary>
        /// <param name="button">The MouseButton.</param>
        /// <returns>True if pressed.</returns>
        public bool IsPressed(MouseButtons button)
        {
            if (!_reference.ContainsKey(button))
            {
                return false;
            }

            return _reference[button];
        }
    }
}