using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IServerProtocol
    {
        /// <summary>
        /// Starts hosting on the given Port.
        /// </summary>
        /// <param name="port">The Port.</param>
        void Host(int port);
        /// <summary>
        /// Accepts a Connection.
        /// </summary>
        void AcceptConnection();
        /// <summary>
        /// Indicates whether the Server is hosting.
        /// </summary>
        bool Hosted { set; get; }
        /// <summary>
        /// Sets the Server.
        /// </summary>
        IServer Server { set; }
    }
}
