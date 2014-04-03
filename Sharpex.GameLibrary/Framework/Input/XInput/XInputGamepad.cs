using System.Runtime.InteropServices;

namespace SharpexGL.Framework.Input.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputGamepad
    {
        /// <summary>
        /// The Buttons.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(0)] public short wButtons;
        /// <summary>
        /// The back left trigger.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(2)] public byte bLeftTrigger;
        /// <summary>
        /// The back right trigger.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(3)] public byte bRightTrigger;
        /// <summary>
        /// The thumb left X.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(4)] public short sThumbLX;
        /// <summary>
        /// The thumb left Y.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(6)] public short sThumbLY;
        /// <summary>
        /// The thumb right X.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(8)] public short sThumbRX;
        /// <summary>
        /// The thumb right Y.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(10)] public short sThumbRY;

        /// <summary>
        /// A value indicating whether the button was pressed.
        /// </summary>
        /// <param name="buttonFlags">The ButtonFlags.</param>
        /// <returns>True if the button was pressed.</returns>
        public bool IsButtonPressed(int buttonFlags)
        {
            return (wButtons & buttonFlags) == buttonFlags;
        }

        /// <summary>
        /// A value indicating whether the button is accessable on the gamepad.
        /// </summary>
        /// <param name="buttonFlags">The ButtonFlags.</param>
        /// <returns>True if accessable.</returns>
        public bool IsButtonPresent(int buttonFlags)
        {
            return (wButtons & buttonFlags) == buttonFlags;
        }

        /// <summary>
        /// Copies the source.
        /// </summary>
        /// <param name="source">The Source.</param>
        public void Copy(XInputGamepad source)
        {
            sThumbLX = source.sThumbLX;
            sThumbLY = source.sThumbLY;
            sThumbRX = source.sThumbRX;
            sThumbRY = source.sThumbRY;
            bLeftTrigger = source.bLeftTrigger;
            bRightTrigger = source.bRightTrigger;
            wButtons = source.wButtons;
        }
    }
}
