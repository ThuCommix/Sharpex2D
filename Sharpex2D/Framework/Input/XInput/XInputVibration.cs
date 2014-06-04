using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Sequential)]
    public struct XInputVibration
    {
        /// <summary>
        ///     The LeftMotorSpeed.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort LeftMotorSpeed;

        /// <summary>
        ///     The RightMotorSpeed.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort RightMotorSpeed;
    }
}