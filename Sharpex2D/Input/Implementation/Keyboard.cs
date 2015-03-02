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
using Sharpex2D.Surface;

namespace Sharpex2D.Input.Implementation
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class Keyboard : NativeInput<KeyboardState>
    {
        private readonly Dictionary<Keys, bool> _currentKeyState;
        private readonly GameWindow _gameWindow;

        /// <summary>
        /// Initializes a new Keyboard class.
        /// </summary>
        public Keyboard()
        {
            _currentKeyState = new Dictionary<Keys, bool>();
            _gameWindow = SGL.QueryComponents<RenderTarget>().Window;
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            _currentKeyState.Clear();

            if (!_gameWindow.IsActive) return;

            for (int i = 1; i < 255; i++)
            {
                _currentKeyState.Add((Keys) i, GetKeyState((Keys) i));
            }
        }

        /// <summary>
        /// Gets the KeyboardState.
        /// </summary>
        /// <returns>KeyboardState.</returns>
        public override KeyboardState GetState()
        {
            return new KeyboardState(_currentKeyState);
        }

        /// <summary>
        /// Gets the key state.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed.</returns>
        private bool GetKeyState(Keys key)
        {
            return 0 != (NativeMethods.GetAsyncKeyState(key) & 0x8000);
        }
    }
#endif
}