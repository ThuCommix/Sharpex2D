
namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IDevice
    {
        /// <summary>
        ///     A value indicating whether the device is enabled.
        /// </summary>
        bool IsEnabled { set; get; }

        /// <summary>
        ///     Gets the device description.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        void InitializeDevice();
    }
}