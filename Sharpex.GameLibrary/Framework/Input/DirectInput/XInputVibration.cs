using System.Runtime.InteropServices;

namespace SharpexGL.Framework.Input.DirectInput
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
