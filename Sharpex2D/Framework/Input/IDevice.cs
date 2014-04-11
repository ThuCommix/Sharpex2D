using System;

namespace Sharpex2D.Framework.Input
{
    public interface IDevice
    {
        /// <summary>
        /// A value indicating whether the device is enabled.
        /// </summary>
        bool IsEnabled { set; get; }
        /// <summary>
        /// Gets the Guid-Identifer of the device.
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// Gets the device description.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Initializes the Device.
        /// </summary>
        void InitializeDevice();
    }
}
