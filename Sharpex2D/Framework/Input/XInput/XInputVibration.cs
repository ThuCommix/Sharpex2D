using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [StructLayout(LayoutKind.Sequential)]
    public struct XInputVibration
    {
        /// <summary>
        /// The LeftMotorSpeed.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort LeftMotorSpeed;

        /// <summary>
        /// The RightMotorSpeed.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort RightMotorSpeed;
    }
}
