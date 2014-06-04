using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputCapabilities
    {
        /// <summary>
        ///     The Type.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(0)] private readonly byte Type;

        /// <summary>
        ///     The SubType.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(1)] public byte SubType;

        /// <summary>
        ///     The Flags.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(2)] public short Flags;

        /// <summary>
        ///     The Gamepad.
        /// </summary>
        [FieldOffset(4)] public XInputGamepad Gamepad;

        /// <summary>
        ///     The Vibration.
        /// </summary>
        [FieldOffset(16)] public XInputVibration Vibration;
    }
}