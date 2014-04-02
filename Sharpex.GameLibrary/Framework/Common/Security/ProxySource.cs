using System;

namespace SharpexGL.Framework.Common.Security
{
    public abstract class ProxySource : MarshalByRefObject
    {
        /// <summary>
        /// Gets the LifetimeService.
        /// </summary>
        /// <returns>ILease object.</returns>
        public new virtual object GetLifetimeService()
        {
            return base.GetLifetimeService();
        }
    }
}
