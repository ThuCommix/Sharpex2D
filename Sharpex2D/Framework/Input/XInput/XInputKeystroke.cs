using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputKeystroke
    {
        /// <summary>
        /// The VirtualKey.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(0)] public short VirtualKey;
        /// <summary>
        /// The Unicode char.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(2)] public char Unicode;
        /// <summary>
        /// The Flags.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(4)] public short Flags;
        /// <summary>
        /// The UserIndex.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(5)] public byte UserIndex;
        /// <summary>
        /// The HIDCode.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(6)] public byte HidCode;
    }
}
