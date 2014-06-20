using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IDeviceResource : IContent
    {
        /// <summary>
        ///     Gets or sets the DebugName.
        /// </summary>
        string DebugName { set; get; }
    }
}