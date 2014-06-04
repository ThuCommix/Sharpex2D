namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum CapabilityFlags
    {
        /// <summary>
        ///     Voice supported.
        /// </summary>
        XINPUT_CAPS_VOICE_SUPPORTED = 0x0004,

        /// <summary>
        ///     FFB supported.
        /// </summary>
        XINPUT_CAPS_FFB_SUPPORTED = 0x0001,

        /// <summary>
        ///     Wireless.
        /// </summary>
        XINPUT_CAPS_WIRELESS = 0x0002,

        /// <summary>
        ///     PMD supported.
        /// </summary>
        XINPUT_CAPS_PMD_SUPPORTED = 0x0008,

        /// <summary>
        ///     No navigation.
        /// </summary>
        XINPUT_CAPS_NO_NAVIGATION = 0x0010,
    }
}