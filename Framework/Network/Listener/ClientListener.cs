using System;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Listener
{
    public abstract class ClientListener : IClientListener
    {
        #region IClientListener Implementation
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        public Type PackageType { get; private set; }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientListener.OnReceive(IClient client, IPackage<object> package)
        {
            PackageType = package.GetType();
            OnReceive(client, package);
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientListener.OnBeginSend(IClient client, IPackage<object> package)
        {
            PackageType = package.GetType();
            OnBeginSend(client, package);
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientListener.OnSent(IClient client, IPackage<object> package)
        {
            PackageType = package.GetType();
            OnSent(client, package);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="elapsed">The elapsed.</param>
        void IClientListener.Tick(IClient client, float elapsed)
        {
            Tick(client, elapsed);
        }
        #endregion

        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnReceive(IClient client, IPackage<object> package)
        {
            
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnBeginSend(IClient client, IPackage<object> package)
        {
          
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSent(IClient client, IPackage<object> package)
        {
      
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(IClient client, float elapsed)
        {
           
        }
    }
}
