using System;

namespace SharpexGL.Framework.Input.XInput
{

    [Flags]
    public enum Subtypes
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        XINPUT_DEVSUBTYPE_UNKNOWN = 0x00,
        /// <summary>
        /// Wheel.
        /// </summary>
        XINPUT_DEVSUBTYPE_WHEEL = 0x02,
        /// <summary>
        /// Arcade stick.
        /// </summary>
        XINPUT_DEVSUBTYPE_ARCADE_STICK = 0x03,
        /// <summary>
        /// Flight stick.
        /// </summary>
        XINPUT_DEVSUBTYPE_FLIGHT_STICK = 0x04,
        /// <summary>
        /// Dance pad.
        /// </summary>
        XINPUT_DEVSUBTYPE_DANCE_PAD = 0x05,
        /// <summary>
        /// Guitar.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR = 0x06,
        /// <summary>
        /// Guitar alternate.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = 0x07,
        /// <summary>
        /// Drum kit.
        /// </summary>
        XINPUT_DEVSUBTYPE_DRUM_KIT = 0x08,
        /// <summary>
        /// Guitar bass.
        /// </summary>
        XINPUT_DEVSUBTYPE_GUITAR_BASS = 0x0B,
        /// <summary>
        /// Arcade pad.
        /// </summary>
        XINPUT_DEVSUBTYPE_ARCADE_PAD = 0x13
    };
}
