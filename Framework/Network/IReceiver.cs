using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Network
{
    public interface IReceiver : IConnection
    {
        /// <summary>
        /// Sets or gets the list of the current connections.
        /// </summary>
        IList<IConnection> Connections { set; get; }
        /// <summary>
        /// Sends an object to the current connections.
        /// </summary>
        /// <param name="obj">The Object.</param>
        void Send(object obj);
        /// <summary>
        /// Receives an object.
        /// </summary>
        /// <returns>The Object.</returns>
        object Receive();

    }
}
