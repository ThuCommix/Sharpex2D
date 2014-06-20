using System;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [AttributeUsage(AttributeTargets.Class)]
    public class DeviceAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new DeviceAttribute class.
        /// </summary>
        /// <param name="friendlyName">The FriendlyName.</param>
        public DeviceAttribute(string friendlyName)
        {
            FriendlyName = friendlyName;
        }

        public string FriendlyName { private set; get; }
    }
}