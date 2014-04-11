using System;

namespace Sharpex2D.Framework.Network.Packages.System
{
    [Serializable]
    internal class NotificationPackage : BasePackage
    {
        /// <summary>
        /// Initializes a new NotificationPackage class.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        public NotificationPackage(IConnection[] connection)
        {
            Connection = connection;
            Mode = NotificationMode.None;
        }
        /// <summary>
        /// Initializes a new NotificationPackage class.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        /// <param name="mode">The NotificationMode.</param>
        public NotificationPackage(IConnection[] connection, NotificationMode mode)
        {
            Connection = connection;
            Mode = mode;
        }
        /// <summary>
        /// Sets or gets the NotificationMode.
        /// </summary>
        public NotificationMode Mode { set; get; }
        /// <summary>
        /// Gets the corresponding connection.
        /// </summary>
        public IConnection[] Connection { private set; get; }
    }
}
