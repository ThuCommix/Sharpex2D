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
using System.Globalization;
using Sharpex2D.Math;

namespace Sharpex2D.Input.Windows.XInput
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Gamepad : NativeInput<GamepadState>
    {
        /// <summary>
        ///     Maximum Controller input.
        /// </summary>
        internal const int MaxControllerCount = 4;

        /// <summary>
        ///     StartIndex.
        /// </summary>
        internal const int FirstControllerIndex = 0;

        /// <summary>
        ///     LastIndex.
        /// </summary>
        internal const int LastControllerIndex = MaxControllerCount - 1;

        /// <summary>
        ///     Gets the Available Controllers.
        /// </summary>
        private static readonly Gamepad[] Controllers;

        private readonly int _playerIndex;
        private XInputBatteryInformation _batterInformationHeadset;
        private XInputBatteryInformation _batteryInformationGamepad;
        private XInputState _gamepadStateCurrent;
        private XInputState _gamepadStatePrev = new XInputState();
        private bool _isInitilized;
        private DateTime _stopMotorTime;
        private bool _stopMotorTimerActive;

        /// <summary>
        ///     Initializes a new XboxController class.
        /// </summary>
        static Gamepad()
        {
            Controllers = new Gamepad[MaxControllerCount];
            for (int i = FirstControllerIndex; i <= LastControllerIndex; ++i)
            {
                Controllers[i] = new Gamepad(i);
            }
        }

        /// <summary>
        ///     Initializes a new XboxController class.
        /// </summary>
        /// <param name="playerIndex">The Index.</param>
        private Gamepad(int playerIndex)
            : base(new Guid("CB9F0F16-F1A0-4022-B50E-82BA1C2D4D5E"))
        {
            _playerIndex = playerIndex;
            _gamepadStatePrev.Copy(_gamepadStateCurrent);
        }

        /// <summary>
        ///     Gets the Gamepad BatteryInformation.
        /// </summary>
        public XInputBatteryInformation BatteryInformationGamepad
        {
            get { return _batteryInformationGamepad; }
            internal set { _batteryInformationGamepad = value; }
        }

        /// <summary>
        ///     Gets the Headset BatteryInformation.
        /// </summary>
        public XInputBatteryInformation BatteryInformationHeadset
        {
            get { return _batterInformationHeadset; }
            internal set { _batterInformationHeadset = value; }
        }

        /// <summary>
        ///     A value indicating whether the Controller is Connected.
        /// </summary>
        public bool IsConnected { get; internal set; }

        /// <summary>
        ///     Gets the PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(5, 1); }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            UpdateState();
        }

        /// <summary>
        ///     Gets the State.
        /// </summary>
        /// <returns>GamepadState.</returns>
        public override GamepadState GetState()
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
                    _gamepadStateCurrent.Gamepad.bLeftTrigger, _gamepadStateCurrent.Gamepad.bRightTrigger,
                    new Vector2(_gamepadStateCurrent.Gamepad.sThumbLX, _gamepadStateCurrent.Gamepad.sThumbLY),
                    new Vector2(_gamepadStateCurrent.Gamepad.sThumbRX, _gamepadStateCurrent.Gamepad.sThumbRY));
        }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public override void InitializeDevice()
        {
            if (!_isInitilized)
            {
                _isInitilized = true;
                UpdateState();
            }
        }

        /// <summary>
        ///     Retrieves the XBoxController.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>XboxController.</returns>
        public static Gamepad Retrieve(int index)
        {
            return Controllers[index];
        }

        /// <summary>
        ///     Updates the BatteryState.
        /// </summary>
        internal void UpdateBatteryState()
        {
            XInputBatteryInformation headset = new XInputBatteryInformation(),
                gamepad = new XInputBatteryInformation();

            XInputAPI.XInputGetBatteryInformation(_playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD,
                ref gamepad);
            XInputAPI.XInputGetBatteryInformation(_playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_HEADSET,
                ref headset);

            BatteryInformationHeadset = headset;
            BatteryInformationGamepad = gamepad;
        }

        /// <summary>
        ///     Triggers the events.
        /// </summary>
        protected void OnStateChanged()
        {
        }

        /// <summary>
        ///     Gets the Capabilities.
        /// </summary>
        /// <returns></returns>
        public XInputCapabilities GetCapabilities()
        {
            var capabilities = new XInputCapabilities();
            XInputAPI.XInputGetCapabilities(_playerIndex, XInputConstants.XINPUT_FLAG_GAMEPAD, ref capabilities);
            return capabilities;
        }

        /// <summary>
        ///     Updates the Controller state.
        /// </summary>
        internal void UpdateState()
        {
            int result = XInputAPI.XInputGetState(_playerIndex, ref _gamepadStateCurrent);
            IsConnected = (result == 0);

            UpdateBatteryState();
            if (_gamepadStateCurrent.PacketNumber != _gamepadStatePrev.PacketNumber)
            {
                OnStateChanged();
            }
            _gamepadStatePrev.Copy(_gamepadStateCurrent);

            if (_stopMotorTimerActive && (DateTime.Now >= _stopMotorTime))
            {
                var stopStrength = new XInputVibration {LeftMotorSpeed = 0, RightMotorSpeed = 0};
                XInputAPI.XInputSetState(_playerIndex, ref stopStrength);
            }
        }

        /// <summary>
        ///     Vibrates the controller.
        /// </summary>
        /// <param name="leftMotor">The LeftMotor.</param>
        /// <param name="rightMotor">The RightMotor.</param>
        /// <param name="length">The Length.</param>
        public void Vibrate(double leftMotor, double rightMotor, float length)
        {
            leftMotor = System.Math.Max(0d, System.Math.Min(1d, leftMotor));
            rightMotor = System.Math.Max(0d, System.Math.Min(1d, rightMotor));

            var vibration = new XInputVibration
            {
                LeftMotorSpeed = (ushort) (65535d*leftMotor),
                RightMotorSpeed = (ushort) (65535d*rightMotor)
            };
            Vibrate(vibration, TimeSpan.FromMilliseconds(length));
        }

        /// <summary>
        ///     Vibrates the controller.
        /// </summary>
        /// <param name="strength">The Strength.</param>
        /// <param name="length">The Length.</param>
        internal void Vibrate(XInputVibration strength, TimeSpan length)
        {
            XInputAPI.XInputSetState(_playerIndex, ref strength);
            if (length != TimeSpan.MinValue)
            {
                _stopMotorTime = DateTime.Now.Add(length);
                _stopMotorTimerActive = true;
            }
        }

        /// <summary>
        ///     Converts the object to string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return _playerIndex.ToString(CultureInfo.InvariantCulture);
        }
    }

#endif
}