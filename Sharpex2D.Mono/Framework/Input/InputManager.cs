using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Input.Devices;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class InputManager : IConstructable
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