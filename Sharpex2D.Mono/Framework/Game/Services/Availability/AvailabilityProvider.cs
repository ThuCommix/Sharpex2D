using System.Net.NetworkInformation;

namespace Sharpex2D.Framework.Game.Services.Availability
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class AvailabilityProvider : IGameService
    {
        /// <summary>
        ///     Checks if the Network is available. Throws an NetworkNotAvailableException if not.
        /// </summary>
        /// <returns>True if the network is available</returns>
        public static bool CheckNetworkConnection()
        {
            var pingRequest = new Ping();
            PingReply reply = pingRequest.Send("www.google.de");
            if (reply == null || reply.Status != IPStatus.Success)
            {
                return false;
            }

            return true;
        }
    }
}