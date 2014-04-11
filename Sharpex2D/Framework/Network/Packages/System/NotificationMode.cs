using System;

namespace Sharpex2D.Framework.Network.Packages.System
{
    [Serializable]
    internal enum NotificationMode
    {
        ClientJoined = 1,
        ClientExited = 2,
        ServerShutdown = 3,
        ClientList = 4,
        TimeOut = 5,
        None = 0
    }
}
