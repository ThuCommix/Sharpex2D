using System;

namespace Sharpex2D.Framework.Common.Security
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public abstract class ProxySource : MarshalByRefObject
    {
        /// <summary>
        ///     Gets the LifetimeService.
        /// </summary>
        /// <returns>ILease object.</returns>
        public new virtual object GetLifetimeService()
        {
            return base.GetLifetimeService();
        }
    }
}