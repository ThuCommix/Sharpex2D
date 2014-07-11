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

using System.Collections.Generic;
using Sharpex2D.Math;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class MouseState : IInputState
    {
        private readonly Dictionary<MouseButtons, bool> _reference;

        /// <summary>
        ///     Initializes a new MouseState class.
        /// </summary>
        /// <param name="reference">The Reference.</param>
        /// <param name="position">The Position.</param>
        internal MouseState(Dictionary<MouseButtons, bool> reference, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            _reference = reference;
        }

        /// <summary>
        ///     Gets the Position.
        /// </summary>
        public Vector2 Position { private set; get; }

        /// <summary>
        ///     A value indicating whether the MouseButton is pressed.
        /// </summary>
        /// <param name="button">The MouseButton.</param>
        /// <returns>True if pressed.</returns>
        public bool IsMouseButtonDown(MouseButtons button)
        {
            if (!_reference.ContainsKey(button))
            {
                return false;
            }

            return _reference[button];
        }

        /// <summary>
        ///     A value indicating whether the MouseButton is released.
        /// </summary>
        /// <param name="button">The MouseButton.</param>
        /// <returns>True if released.</returns>
        public bool IsMouseButtonUp(MouseButtons button)
        {
            if (!_reference.ContainsKey(button))
            {
                return false;
            }

            return !_reference[button];
        }
    }
}