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

namespace Sharpex2D.Framework.UI
{
    public class InputState
    {
        /// <summary>
        /// Gets the state
        /// </summary>
        public object State { get; }

        /// <summary>
        /// Initializes a new InputState class
        /// </summary>
        /// <param name="state">The state</param>
        public InputState(object state)
        {
            State = state;
        }

        /// <summary>
        /// Converts the state as the specified type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>T</returns>
        public T As<T>() where T : class 
        {
            return State as T;
        }

        /// <summary>
        /// Gets a value indicating whether the specified type is assignable from state
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>True when convertable</returns>
        public bool Is<T>()
        {
            return State is T;
        }
    }
}
