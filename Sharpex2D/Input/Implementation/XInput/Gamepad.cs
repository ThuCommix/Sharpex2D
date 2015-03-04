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

using System;
using System.Globalization;
using Sharpex2D.Math;

namespace Sharpex2D.Input.Implementation.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class Gamepad : IGamepad
    {
        /// <summary>
        /// Maximum Controller input.
        /// </summary>
        internal const int MaxControllerCount = 4;

        /// <summary>
        /// StartIndex.
        /// </summary>
        internal const int FirstControllerIndex = 0;

        /// <summary>
        /// Gets the Available Controllers.
        /// </summary>
        private static readonly Gamepad[] Controllers;

        private readonly int _playerIndex;
        private XInputState _gamepadStateCurrent;
        private XInputState _gamepadStatePrev = new XInputState();
        private bool _isInitilized;
        private bool _vibrationStopped = true;
        private float _vibrationTime;

        /// <summary>
        /// Initializes a new Gamepad class.
        /// </summary>
        static Gamepad()
        {
            Controllers = new Gamepad[MaxControllerCount];
            for (int i = FirstControllerIndex; i < MaxControllerCount; i++)
            {
                Controllers[i] = new Gamepad(i);
            }
        }

        /// <summary>
        /// Initializes a new Gamepad class.
        /// </summary>
        /// <param name="playerIndex">The Index.</param>
        private Gamepad(int playerIndex)
        {
            _playerIndex = playerIndex;
            _gamepadStatePrev.Copy(_gamepadStateCurrent);
        }

        /// <summary>
        /// Gets the Gamepad BatteryInformation.
        /// </summary>
        public XInputBatteryInformation BatteryInformationGamepad { get; internal set; }

        /// <summary>
        /// Gets the Headset BatteryInformation.
        /// </summary>
        public XInputBatteryInformation BatteryInformationHeadset { get; internal set; }

        /// <summary>
        /// A value indicating whether the Controller is available.
        /// </summary>
        public bool IsAvailable { get; internal set; }

        /// <summary>
        /// Gets the BatteryLevel.
        /// </summary>
        public BatteryLevel BatteryLevel
        {
            get
            {
                var xinputBatteryType = (XInputBatteryType) BatteryInformationGamepad.BatteryType;
                var xinputBatteryLevel = (XInputBatteryLevel) BatteryInformationGamepad.BatteryLevel;
                System.Diagnostics.Debug.WriteLine(xinputBatteryType);
                if (xinputBatteryType == XInputBatteryType.Wired)
                {
                    return BatteryLevel.Wired;
                }
                var level = BatteryLevel.Full;
                switch (xinputBatteryLevel)
                {
                    case XInputBatteryLevel.Empty:
                        level = BatteryLevel.Empty;
                        break;
                    case XInputBatteryLevel.Full:
                        level = BatteryLevel.Full;
                        break;
                    case XInputBatteryLevel.Low:
                        level = BatteryLevel.Low;
                        break;
                    case XInputBatteryLevel.Medium:
                        level = BatteryLevel.Medium;
                        break;
                }

                return level;
            }
        }

        /// <summary>
        /// Gets the State.
        /// </summary>
        /// <returns>GamepadState.</returns>
        public GamepadState GetState()
        {
            return
                new GamepadState(
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_UP),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_A),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_B),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_Y),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_X),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_BACK),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_START),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB),
                    _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GUIDE),
                    _gamepadStateCurrent.Gamepad.bLeftTrigger/255f, _gamepadStateCurrent.Gamepad.bRightTrigger/255f,
                    new Vector2(_gamepadStateCurrent.Gamepad.sThumbLX/32767f,
                        _gamepadStateCurrent.Gamepad.sThumbLY/32767f),
                    new Vector2(_gamepadStateCurrent.Gamepad.sThumbRX/32767f,
                        _gamepadStateCurrent.Gamepad.sThumbRY/32767f));
        }

        /// <summary>
        /// Vibrates the controller.
        /// </summary>
        /// <param name="leftMotor">The LeftMotor.</param>
        /// <param name="rightMotor">The RightMotor.</param>
        /// <param name="length">The Length.</param>
        public void Vibrate(float leftMotor, float rightMotor, float length)
        {
            leftMotor = (float) System.Math.Max(0d, System.Math.Min(1d, leftMotor));
            rightMotor = (float) System.Math.Max(0d, System.Math.Min(1d, rightMotor));

            var vibration = new XInputVibration
            {
                LeftMotorSpeed = (ushort) (65535d*leftMotor),
                RightMotorSpeed = (ushort) (65535d*rightMotor)
            };
            Vibrate(vibration, length);
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            int result = XInputInterops.XInputGetState(_playerIndex, ref _gamepadStateCurrent);
            IsAvailable = (result == 0);
            if (!IsAvailable) return;

            UpdateBatteryState();
            _gamepadStatePrev.Copy(_gamepadStateCurrent);

            if (_vibrationTime > 0)
            {
                _vibrationTime -= gameTime.ElapsedGameTime;
            }

            if (_vibrationTime <= 0 && !_vibrationStopped)
            {
                var stopStrength = new XInputVibration {LeftMotorSpeed = 0, RightMotorSpeed = 0};
                XInputInterops.XInputSetState(_playerIndex, ref stopStrength);
                _vibrationStopped = true;
            }
        }

        /// <summary>
        /// Initializes the Device.
        /// </summary>
        public void Initialize()
        {
            if (!_isInitilized)
            {
                _isInitilized = true;
                Update(new GameTime
                {
                    ElapsedGameTime = 0,
                    IsRunningSlowly = false,
                    TotalGameTime = TimeSpan.FromSeconds(0)
                });
            }
        }

        /// <summary>
        /// Retrieves the XBoxController.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>XboxController.</returns>
        public static Gamepad Retrieve(int index)
        {
            return Controllers[index];
        }

        /// <summary>
        /// Updates the BatteryState.
        /// </summary>
        internal void UpdateBatteryState()
        {
            XInputBatteryInformation headset = new XInputBatteryInformation(),
                gamepad = new XInputBatteryInformation();

            XInputInterops.XInputGetBatteryInformation(_playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD,
                ref gamepad);
            XInputInterops.XInputGetBatteryInformation(_playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_HEADSET,
                ref headset);

            BatteryInformationHeadset = headset;
            BatteryInformationGamepad = gamepad;
        }

        /// <summary>
        /// Gets the Capabilities.
        /// </summary>
        /// <returns></returns>
        public XInputCapabilities GetCapabilities()
        {
            var capabilities = new XInputCapabilities();
            XInputInterops.XInputGetCapabilities(_playerIndex, XInputConstants.XINPUT_FLAG_GAMEPAD, ref capabilities);
            return capabilities;
        }

        /// <summary>
        /// Vibrates the controller.
        /// </summary>
        /// <param name="strength">The Strength.</param>
        /// <param name="length">The Length.</param>
        internal void Vibrate(XInputVibration strength, float length)
        {
            XInputInterops.XInputSetState(_playerIndex, ref strength);
            _vibrationStopped = false;
            _vibrationTime = length;
        }

        /// <summary>
        /// Converts the object to string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return _playerIndex.ToString(CultureInfo.InvariantCulture);
        }
    }
}