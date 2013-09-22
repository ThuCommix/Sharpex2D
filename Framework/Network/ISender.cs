using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network
{
    public interface ISender
    {
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        void Send(IBasePackage package);
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        void Send(IBasePackage package, IConnection receiver);
    }
}
