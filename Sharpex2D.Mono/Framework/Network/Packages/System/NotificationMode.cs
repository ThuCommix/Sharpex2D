using System;

namespace Sharpex2D.Framework.Network.Packages.System
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
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