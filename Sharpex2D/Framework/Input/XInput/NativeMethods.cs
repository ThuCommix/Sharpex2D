using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Input.XInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class NativeMethods
    {
        /// <summary>
        ///     Gets the XInput state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pState">The InputState.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputGetState
            (
            int dwUserIndex,
            ref XInputState pState
            );

        /// <summary>
        ///     Sets the Input state.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="pVibration">The Vibration.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputSetState
            (
            int dwUserIndex,
            ref XInputVibration pVibration
            );

        /// <summary>
        ///     Gets the Capabilities.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="dwFlags">The dwFlags.</param>
        /// <param name="pCapabilities">The Capabilities.</param>
        /// <returns></returns>
        [DllImport("xinput9_1_0.dll")]
        internal static extern int XInputGetCapabilities
            (
            int dwUserIndex,
            int dwFlags,
            ref XInputCapabilities pCapabilities
            );

        /// <summary>
        ///     Gets the Battery information.
        /// </summary>
        /// <param name="dwUserIndex">The Index.</param>
        /// <param name="devType">The DevType.</param>
        /// <param name="pBatteryInformation">The BatteryInformation.</param>
        /// <returns></returns>
        internal static int XInputGetBatteryInformation
            (
            int dwUserIndex,
            byte devType,
            ref XInputBatteryInformation pBatteryInformation
            )
        {
            return 0;
        }
    }
}