using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputState
    {
        /// <summary>
        /// The PacketNumber.
        /// </summary>
        [FieldOffset(0)] public int PacketNumber;
        /// <summary>
        /// The Gamepad.
        /// </summary>
        [FieldOffset(4)] public XInputGamepad Gamepad;
        /// <summary>
        /// Copies the source.
        /// </summary>
        /// <param name="source">The Source.</param>
        public void Copy(XInputState source)
        {
            PacketNumber = source.PacketNumber;
            Gamepad.Copy(source.Gamepad);
        }
    }
}
