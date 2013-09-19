using System;
using System.Threading.Tasks;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Network.Events;

namespace SharpexGL.Framework.Network.Packages
{
    public class PackageReceiver
    {
        public PackageReceiver(Server server)
        {
            _server = server;
        }

        private Server _server;

        public void BeginReceiving(IConnection connection)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (connection.Connected)
                    {
                        var package = connection.Receive();
                        if (package != null)
                        {
                            _server.OnPackageReceived(connection, package);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _server.Connections.Remove(connection);
                    _server.OnClientLeft(connection);
                    SGL.Components.Get<EventManager>().Publish(new NetworkExceptionEvent(ex.Message));
                }
            });
        }
    }
}
