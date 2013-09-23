using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network
{
    public interface IReceiver
    {
        /// <summary>
        /// Receives a package.
        /// </summary>
        void Receive();
    }
}
