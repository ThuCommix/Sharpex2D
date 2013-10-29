using System.Net.NetworkInformation;

namespace SharpexGL.Framework.Game.Services
{
    public class NetworkService
    {
        /// <summary>
        /// Checks if the Network is available. Throws an NetworkNotAvailableException if not.
        /// </summary>
        public static void CheckAvailability()
        {
            var pingRequest = new Ping();
            var reply = pingRequest.Send("www.google.de");
            if (reply == null && reply.Status != IPStatus.Success)
            {
                throw new NetworkNotAvailableException("The network is not available.");
            }
        }
    }
}
