using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IServer : ISender, IReceiver
    {
        /// <summary>
        /// A value indicating whether the server is active.
        /// </summary>
        bool IsActive { get; }
    }
}
