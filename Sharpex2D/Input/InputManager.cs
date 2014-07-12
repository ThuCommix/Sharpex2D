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
using System.Linq;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class InputManager : IComponent, IUpdateable, IEnumerable<IInputDevice>
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("EA75A88F-C5C3-48B4-ACA1-3366B579CA57"); }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _devices.Count; i++)
            {
                _devices[i].Update(gameTime);
            }
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        ///     Gets the Enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<IInputDevice> GetEnumerator()
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

        private readonly List<IInputDevice> _devices;
        private readonly Logger _logger;

        /// <summary>
        ///     Initializes a new InputManager Instance.
        /// </summary>
        public InputManager()
        {
            _devices = new List<IInputDevice>();
            _logger = LogManager.GetClassLogger();

            Mouse = new Mouse(new XPlatform.Mouse());
#if Windows
            Keyboard = new Keyboard(new Windows.RawInput.RawInputKeyboard());
            Gamepad = new Gamepad(Windows.XInput.Gamepad.Retrieve(1));
            Joystick = new Joystick(new Windows.JoystickApi.Joystick());

            var nativeTouch = new Windows.Touch.TouchDevice();
            if (nativeTouch.IsPlatformSupported)
            {
                TouchPanel = new TouchPanel(nativeTouch);
                Add(TouchPanel);
            }
            else
            {
                _logger.Info("Disabled touch input due incompatibility with the current platform.");
            }

            Add(Mouse);
            Add(Keyboard);
            Add(Gamepad);
            Add(Joystick);
#else
            Keyboard = new Keyboard(new XPlatform.Keyboard());
            Add(Mouse);
            Add(Keyboard);
#endif

        }

        /// <summary>
        ///     Gets the Devices.
        /// </summary>
        public IInputDevice[] Devices
        {
            get { return _devices.ToArray(); }
        }

        /// <summary>
        ///     Gets or sets the Keyboard device.
        /// </summary>
        public Keyboard Keyboard { get; set; }

        /// <summary>
        ///     Gets or sets the Mouse device.
        /// </summary>
        public Mouse Mouse { get; set; }

        /// <summary>
        ///     Gets or sets the Gamepad.
        /// </summary>
        public Gamepad Gamepad { get; set; }

        /// <summary>
        /// Gets or sets the Joystick.
        /// </summary>
        public Joystick Joystick { get; set; }

        /// <summary>
        /// Gets or sets the TouchPanel.
        /// </summary>
        public TouchPanel TouchPanel { get; set; }

        /// <summary>
        ///     Adds a new device to the InputManager.
        /// </summary>
        /// <param name="device">The Device.</param>
        public void Add(IInputDevice device)
        {
            device.InitializeDevice();
            _devices.Add(device);
        }

        /// <summary>
        ///     Removes a device from the InputManager.
        /// </summary>
        /// <param name="device">The Device.</param>
        public void Remove(IInputDevice device)
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
        /// <returns>InputDevice</returns>
        public T Get<T>() where T : IInputDevice
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

        /// <summary>
        ///     Gets any device which matches T.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Array of InputDevice.</returns>
        public T[] AnyDevice<T>() where T : IInputDevice
        {
            var deviceList = new List<T>();
            for (int i = 0; i <= _devices.Count - 1; i++)
            {
                if (_devices[i].GetType() == typeof (T))
                {
                    deviceList.Add((T) _devices[i]);
                }
            }

            if (deviceList.Count == 0)
            {
                throw new InvalidOperationException("Devices not found (" + typeof (T).FullName + ").");
            }

            return deviceList.ToArray();
        }

        /// <summary>
        ///     Gets the device if supported.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>InputDevice.</returns>
        public T GetSupportedDevice<T>() where T : IInputDevice
        {
            for (int i = 0; i <= _devices.Count - 1; i++)
            {
                if (_devices[i].GetType() == typeof (T) && _devices[i].IsPlatformSupported)
                {
                    return (T) _devices[i];
                }
            }

            throw new InvalidOperationException("Device not found (" + typeof (T).FullName + ").");
        }

        /// <summary>
        ///     Gets any supported device which matches T.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Array of InputDevice.</returns>
        public T[] AnySupportedDevice<T>() where T : IInputDevice
        {
            var deviceList = new List<T>();
            for (int i = 0; i <= _devices.Count - 1; i++)
            {
                if (_devices[i].GetType() == typeof (T) && _devices[i].IsPlatformSupported)
                {
                    deviceList.Add((T) _devices[i]);
                }
            }

            if (deviceList.Count == 0)
            {
                throw new InvalidOperationException("Devices not found (" + typeof (T).FullName + ").");
            }

            return deviceList.ToArray();
        }

        /// <summary>
        ///     Gets the device by guid.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>InputDevice.</returns>
        public IInputDevice GetDeviceByGuid(Guid guid)
        {
            foreach (IInputDevice device in _devices.Where(device => device.Guid == guid))
            {
                return device;
            }

            throw new InvalidOperationException("Device not found (" + guid + ").");
        }

        /// <summary>
        ///     Gets any device which matches the guid.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>Array of InputDevice.</returns>
        public IInputDevice[] AnyDeviceByGuid(Guid guid)
        {
            List<IInputDevice> deviceList = _devices.Where(device => device.Guid == guid).ToList();

            if (deviceList.Count == 0)
            {
                throw new InvalidOperationException("Devices not found (" + guid + ").");
            }

            return deviceList.ToArray();
        }
    }
}