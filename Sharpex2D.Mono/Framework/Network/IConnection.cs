using System.Net;

namespace Sharpex2D.Framework.Network
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IConnection
    {
        /// <summary>
        ///     Sets or gets the Latency.
        /// </summary>
        float Latency { set; get; }

        /// <summary>
        ///     Sets or gets the IPAddress.
        /// </summary>
        IPAddress IPAddress { get; }

        /// <summary>
        ///     A value indicating whether the connection is still available.
        /// </summary>
        bool Connected { get; }
    }
}