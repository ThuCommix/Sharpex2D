using System.Net.NetworkInformation;

namespace SharpexGL.Framework.Game.Services.Availability
{
    public class AvailabilityProvider
    {
        /// <summary>
        /// Checks if the Network is available. Throws an NetworkNotAvailableException if not.
        /// </summary>
        /// <returns>True if the network is available</returns>
        public static bool CheckNetworkConnection()
        {
            var pingRequest = new Ping();
            var reply = pingRequest.Send("www.google.de");
            if (reply == null || reply.Status != IPStatus.Success)
            {
                return false;
            }

            return true;
        }
    }
}
