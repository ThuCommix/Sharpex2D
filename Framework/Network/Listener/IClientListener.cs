using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Listener
{
    public interface IClientListener
    {
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        Type PackageType { get; }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnReceive(IClient client, IPackage<Object> package);
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnBeginSend(IClient client, IPackage<Object> package);
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnSent(IClient client, IPackage<Object> package);
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(IClient client, float elapsed);
    }
}
