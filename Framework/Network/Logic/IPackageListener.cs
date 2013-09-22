using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Network.Logic
{
    public interface IPackageListener
    {
        /// <summary>
        /// Gets the ListenerType.
        /// </summary>
        Type ListenerType { get; }
    }
}
