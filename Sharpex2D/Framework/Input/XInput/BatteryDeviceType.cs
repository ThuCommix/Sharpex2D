namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum BatteryDeviceType : byte
    {
        /// <summary>
        ///     Gamepad.
        /// </summary>
        BATTERY_DEVTYPE_GAMEPAD = 0x00,

        /// <summary>
        ///     Headset.
        /// </summary>
        BATTERY_DEVTYPE_HEADSET = 0x01,
    }
}