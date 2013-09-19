using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network
{
    public class ConnectionProvider
    {
        public ConnectionProvider(Server server)
        {
            _server = server;
            _receiver = new PackageReceiver(server);
        }

        public void BeginAcceptingConnections()
        {
            //Wait till the server is up
            while (!_server.Protocol.Hosting)
            {
            }
            Task.Factory.StartNew(() =>
            {
                while (_server.IsRunning)
                {
                    var connection = _server.Protocol.AcceptConnection();
                    _server.Connections.Add(connection);
                    //Start receiving
                    //notify server
                    _server.OnClientJoined(connection);
                }
            });
        }

        private Server _server;
        private PackageReceiver _receiver;
    }
}
