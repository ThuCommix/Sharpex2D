using System;

namespace SharpexGL.Framework.Input.XInput
{
    [Flags]
    public enum ButtonFlags : int
    {
        /// <summary>
        /// D-Pad Up.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_UP = 0x0001,
        /// <summary>
        /// D-Pad Down.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_DOWN = 0x0002,
        /// <summary>
        /// D-Pad Left.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_LEFT = 0x0004,
        /// <summary>
        /// D-Pad Right.
        /// </summary>
        XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008,
        /// <summary>
        /// Start.
        /// </summary>
        XINPUT_GAMEPAD_START = 0x0010,
        /// <summary>
        /// Back.
        /// </summary>
        XINPUT_GAMEPAD_BACK = 0x0020,
        /// <summary>
        /// Left Thumb.
        /// </summary>
        XINPUT_GAMEPAD_LEFT_THUMB = 0x0040,
        /// <summary>
        /// Right Thumb.
        /// </summary>
        XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080,
        /// <summary>
        /// Left Shoulder.
        /// </summary>
        XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100,
        /// <summary>
        /// Right Shoulder.
        /// </summary>
        XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200,
        /// <summary>
        /// A.
        /// </summary>
        XINPUT_GAMEPAD_A = 0x1000,
        /// <summary>
        /// B.
        /// </summary>
        XINPUT_GAMEPAD_B = 0x2000,
        /// <summary>
        /// X.
        /// </summary>
        XINPUT_GAMEPAD_X = 0x4000,
        /// <summary>
        /// Y.
        /// </summary>
        XINPUT_GAMEPAD_Y = 0x8000,
    };
}
