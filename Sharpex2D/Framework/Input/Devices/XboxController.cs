using System;
using System.Globalization;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Input.Events;
using Sharpex2D.Framework.Input.XInput;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Input.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class XboxController : IDevice, IUpdateable
    {
        #region IDevice Implementation

        /// <summary>
        ///     A value indicating whether the device is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CB9F0F16-F1A0-4022-B50E-82BA1C2D4D5E"); }
        }

        /// <summary>
        ///     Gets the Description.
        /// </summary>
        public string Description
        {
            get { return "XBOX360 Controller support via XInput"; }
        }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public void InitializeDevice()
        {
            if (!_isInitilized)
            {
                _isInitilized = true;
                SGL.Components.Get<IGameLoop>().Subscribe(this);
                _eventManager = SGL.Components.Get<EventManager>();
                UpdateState();
            }
        }

        #endregion

        #region IUpdateable Implementation

        /// <summary>
        ///     Processes a Tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Tick(GameTime gameTime)
        {
            if (IsEnabled)
            {
                UpdateState();
            }
        }

        /// <summary>
        ///     Constructs the component.
        /// </summary>
        public void Construct()
        {
        }

        #endregion

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
        private static readonly XboxController[] Controllers;

        private readonly int _playerIndex;
        private XInputBatteryInformation _batterInformationHeadset;
        private XInputBatteryInformation _batteryInformationGamepad;
        private EventManager _eventManager;
        private XInputState _gamepadStateCurrent;
        private XInputState _gamepadStatePrev = new XInputState();
        private bool _isInitilized;
        private DateTime _stopMotorTime;
        private bool _stopMotorTimerActive;

        /// <summary>
        ///     Initializes a new XboxController class.
        /// </summary>
        static XboxController()
        {
            Controllers = new XboxController[MaxControllerCount];
            for (int i = FirstControllerIndex; i <= LastControllerIndex; ++i)
            {
                Controllers[i] = new XboxController(i);
            }
        }

        /// <summary>
        ///     Initializes a new XboxController class.
        /// </summary>
        /// <param name="playerIndex">The Index.</param>
        private XboxController(int playerIndex)
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
        ///     A value indicating whether the D-Pad up is pressed.
        /// </summary>
        public bool IsDPadUpPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_UP); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad down is pressed.
        /// </summary>
        public bool IsDPadDownPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad left is pressed.
        /// </summary>
        public bool IsDPadLeftPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT); }
        }

        /// <summary>
        ///     A value indicating whether the D-Pad right is pressed.
        /// </summary>
        public bool IsDPadRightPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT); }
        }

        /// <summary>
        ///     A value indicating whether A is pressed.
        /// </summary>
        public bool IsAPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_A); }
        }

        /// <summary>
        ///     A value indicating whether B is pressed.
        /// </summary>
        public bool IsBPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_B); }
        }

        /// <summary>
        ///     A value indicating whether X is pressed.
        /// </summary>
        public bool IsXPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_X); }
        }

        /// <summary>
        ///     A value indicating whether Y is pressed.
        /// </summary>
        public bool IsYPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_Y); }
        }

        /// <summary>
        ///     A value indicating whether back is pressed.
        /// </summary>
        public bool IsBackPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_BACK); }
        }

        /// <summary>
        ///     A value indicating whether start is pressed.
        /// </summary>
        public bool IsStartPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_START); }
        }

        /// <summary>
        ///     A value indicating whether left-shoulder is pressed.
        /// </summary>
        public bool IsLeftShoulderPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER); }
        }

        /// <summary>
        ///     A value indicating whether right-shoulder is pressed.
        /// </summary>
        public bool IsRightShoulderPressed
        {
            get
            {
                return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER);
            }
        }

        /// <summary>
        ///     A value indicating whether left-stick is pressed.
        /// </summary>
        public bool IsLeftStickPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB); }
        }

        /// <summary>
        ///     A value indicating whether right-stick is pressed.
        /// </summary>
        public bool IsRightStickPressed
        {
            get { return _gamepadStateCurrent.Gamepad.IsButtonPressed((int) ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB); }
        }

        /// <summary>
        ///     A value indicating whether left-trigger is pressed.
        /// </summary>
        public int LeftTrigger
        {
            get { return _gamepadStateCurrent.Gamepad.bLeftTrigger; }
        }

        /// <summary>
        ///     A value indicating whether right-trigger is pressed.
        /// </summary>
        public int RightTrigger
        {
            get { return _gamepadStateCurrent.Gamepad.bRightTrigger; }
        }

        /// <summary>
        ///     Gets the LeftThumbStick.
        /// </summary>
        public Vector2 LeftThumbStick
        {
            get
            {
                var vec2 = new Vector2(_gamepadStateCurrent.Gamepad.sThumbLX, _gamepadStateCurrent.Gamepad.sThumbLY);
                return vec2;
            }
        }

        /// <summary>
        ///     Gets the RightThumbStick.
        /// </summary>
        public Vector2 RightThumbStick
        {
            get
            {
                var vec2 = new Vector2(_gamepadStateCurrent.Gamepad.sThumbRX, _gamepadStateCurrent.Gamepad.sThumbRY);
                return vec2;
            }
        }

        /// <summary>
        ///     A value indicating whether the Controller is Connected.
        /// </summary>
        public bool IsConnected { get; internal set; }

        /// <summary>
        ///     Retrieves the XBoxController.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>XboxController.</returns>
        public static XboxController Retrieve(int index)
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
            _eventManager.Publish(new XboxControllerStateChangedEventArgs
            {
                CurrentInputState = _gamepadStateCurrent,
                PreviousInputState = _gamepadStatePrev
            });
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
}