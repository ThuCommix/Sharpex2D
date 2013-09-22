using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IClient : ISender, IReceiver
    {
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        void Connect(IPAddress ip);
        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        void Disconnect();
    }
}
