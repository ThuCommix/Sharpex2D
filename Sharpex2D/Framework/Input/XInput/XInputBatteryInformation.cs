using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputBatteryInformation
    {
        /// <summary>
        ///     The BatteryType.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(0)] public byte BatteryType;

        /// <summary>
        ///     The BatteryLevel.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(1)] public byte BatteryLevel;

        /// <summary>
        ///     Converts the object to string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", (BatteryType) BatteryType, (BatteryLevel) BatteryLevel);
        }
    }
}