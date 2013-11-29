﻿using System;
using System.Collections.Generic;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Input.Devices;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Input
{
    public class InputManager : IGameHandler
    {

        #region IGameHandler Implementation
        /// <summary>
        /// Constructs the Component.
        /// </summary>
        public void Construct()
        {
            Keyboard.Construct();
        }

        /// <summary>
        /// Called if the component should get updated.
        /// </summary>
        /// <param name="elapsed">The Elapsed</param>
        public void Tick(float elapsed)
        {
            Keyboard.Tick(elapsed);
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {

        }
        #endregion

        /// <summary>
        /// Gets the KeyboardListener.
        /// </summary>
        public IKeyboard Keyboard { get; private set; }
        /// <summary>
        /// Gets the MouseListener.
        /// </summary>
        public IMouse Mouse { get; private set; }

        /// <summary>
        /// Initializes a new InputManager Instance.
        /// </summary>
        /// <param name="handle">The GameWindowHandle.</param>
        public InputManager(IntPtr handle)
        {
            Mouse = new Mouse(handle);
            var fluentKeyboard = new Keyboard(handle){IsEnabled=true};
            Keyboard = fluentKeyboard;
            _devices = new List<IDevice> {fluentKeyboard, Mouse};
        }

        private readonly List<IDevice> _devices;

        /// <summary>
        /// Sets the standard keyboard.
        /// </summary>
        /// <param name="keyboard">The Keyboard.</param>
        public void SetStandardKeyboard(IKeyboard keyboard)
        {
            Keyboard = keyboard;
            Keyboard.Construct();
        }

        /// <summary>
        /// Gets all Devices.
        /// </summary>
        /// <returns>IDevice Array</returns>
        public IDevice[] GetDevices()
        {
            return _devices.ToArray();
        }

        /// <summary>
        /// Adds a new device to the InputManager.
        /// </summary>
        /// <param name="device">The Device.</param>
        public void Add(IDevice device)
        {
            _devices.Add(device);
        }

        /// <summary>
        /// Removes a device from the InputManager.
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
        /// Gets a special device.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Device</returns>
        public T Get<T>() where T : IDevice
        {
            for (var i = 0; i <= _devices.Count - 1; i++)
            {
                if (_devices[i].GetType() == typeof (T))
                {
                    return (T)_devices[i];
                }
            }

            throw new InvalidOperationException("Device not found (" + typeof(T).FullName + ").");
        }
    }
}
