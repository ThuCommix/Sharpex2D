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
using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Debug.Logging;
using Sharpex2D.Input.Implementation.Touch;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class InputManager : IUpdateable
    {
        private readonly List<INativeInput> _inputs;
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new InputManager Instance.
        /// </summary>
        public InputManager()
        {
            _inputs = new List<INativeInput>();
            _logger = LogManager.GetClassLogger();
        }

        /// <summary>
        /// Updates all inputs.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            foreach (INativeInput input in _inputs)
            {
                input.Update(gameTime);
            }
        }

        /// <summary>
        /// Initializes the InputManager.
        /// </summary>
        public void Initialize()
        {
            _inputs.Add(Implementation.XInput.Gamepad.Retrieve(0));
            _inputs.Add(Implementation.XInput.Gamepad.Retrieve(1));
            _inputs.Add(Implementation.XInput.Gamepad.Retrieve(2));
            _inputs.Add(Implementation.XInput.Gamepad.Retrieve(3));

            _inputs.Add(new Implementation.Mouse());
            _inputs.Add(new Implementation.Keyboard());

            _inputs.Add(new Implementation.JoystickApi.Joystick());
            _inputs.Add(new TouchDevice());

            foreach (INativeInput input in _inputs)
            {
                try
                {
                    input.Initialize();
                }
                catch
                {
                    _logger.Warn("Unable to initialize {0}.", input.GetType().Name);
                }
            }
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>INativeInput.</returns>
        public T GetInput<T>() where T : INativeInput
        {
            foreach (INativeInput input in _inputs.Where(input => input.GetType().BaseType == typeof (T)))
            {
                return (T) input;
            }

            throw new InvalidOperationException("The input was not found.");
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>INativeInput.</returns>
        public T[] GetInputs<T>() where T : INativeInput
        {
            var inputs = new List<T>();
            bool flag = false;

            foreach (INativeInput input in _inputs.Where(input => input.GetType().BaseType == typeof (T)))
            {
                inputs.Add((T) input);
                flag = true;
            }

            if (flag)
            {
                return inputs.ToArray();
            }

            throw new InvalidOperationException("The input was not found.");
        }
    }
}