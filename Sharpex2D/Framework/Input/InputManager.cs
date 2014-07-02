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
using System.Collections;
using System.Collections.Generic;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Input.Devices;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class InputManager : IConstructable, IEnumerable<IDevice>
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("EA75A88F-C5C3-48B4-ACA1-3366B579CA57"); }
        }

        #endregion

        #region IConstructable Implementation

        /// <summary>
        ///     Constructs the Component.
        /// </summary>
        public void Construct()
        {
            Keyboard.Construct();
            Mouse.Construct();
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        ///     Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<IDevice> GetEnumerator()
        {
            return _devices.GetEnumerator();
        }

        /// <summary>
        ///     Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private readonly List<IDevice> _devices;

        /// <summary>
        ///     Initializes a new InputManager Instance.
        /// </summary>
        /// <param name="handle">The GameWindowHandle.</param>
        public InputManager(IntPtr handle)
        {
            Mouse = new Mouse(handle);
            var fluentKeyboard = new Keyboard(handle) {IsEnabled = true};
            Keyboard = fluentKeyboard;
            _devices = new List<IDevice> {fluentKeyboard, Mouse};
        }

        /// <summary>
        ///     Gets the KeyboardListener.
        /// </summary>
        public IKeyboard Keyboard { get; private set; }

        /// <summary>
        ///     Gets the MouseListener.
        /// </summary>
        public IMouse Mouse { get; private set; }

        /// <summary>
        ///     Sets the standard keyboard.
        /// </summary>
        /// <param name="keyboard">The Keyboard.</param>
        public void SetStandardKeyboard(IKeyboard keyboard)
        {
            Keyboard = keyboard;
            Keyboard.Construct();
        }

        /// <summary>
        ///     Sets the standard mouse.
        /// </summary>
        /// <param name="mouse">The Mouse.</param>
        public void SetStandardMouse(IMouse mouse)
        {
            Mouse = mouse;
            Mouse.Construct();
        }

        /// <summary>
        ///     Gets all Devices.
        /// </summary>
        /// <returns>IDevice Array</returns>
        public IDevice[] GetDevices()
        {
            return _devices.ToArray();
        }

        /// <summary>
        ///     Adds a new device to the InputManager.
        /// </summary>
        /// <param name="device">The Device.</param>
        public void Add(IDevice device)
        {
            device.InitializeDevice();
            _devices.Add(device);
        }

        /// <summary>
        ///     Removes a device from the InputManager.
        /// </summary>
        /// <param name="device">The Device.</param>
        public void Remove(IDevice device)
        {
            if (_devices.Contains(device))
            {
                _devices.Remove(device);
            }
        }

        /// <summary>
        ///     Gets a special device.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Device</returns>
        public T Get<T>() where T : IDevice
        {
            for (int i = 0; i <= _devices.Count - 1; i++)
            {
                if (_devices[i].GetType() == typeof (T))
                {
                    return (T) _devices[i];
                }
            }

            throw new InvalidOperationException("Device not found (" + typeof (T).FullName + ").");
        }
    }
}