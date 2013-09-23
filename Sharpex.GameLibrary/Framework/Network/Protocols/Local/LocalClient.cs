using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Network.Events;
using SharpexGL.Framework.Network.Logic;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols.Local
{
    public class LocalClient : IClient
    {
        #region IClient Implementation
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void Send(IBasePackage package)
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            try
            {
                PackageSerializer.Serialize(package, _nStream);
                _nStream.Flush();
            }
            catch (Exception ex)
            {
                SGL.Components.Get<EventManager>().Publish(new PackageSentExceptionEvent(ex.Message));
            }
        }
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        public void Send(IBasePackage package, IPAddress receiver)
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            try
            {
                package.Receiver = receiver;
                PackageSerializer.Serialize(package, _nStream);
                _nStream.Flush();
            }
            catch (Exception ex)
            {
                SGL.Components.Get<EventManager>().Publish(new PackageSentExceptionEvent(ex.Message));
            }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public void Receive()
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            try
            {
                var package = _tcpClient.Available > 0 ? PackageSerializer.Deserialize(_nStream) : null;
                var binaryPackage = package as BinaryPackage;
                if (binaryPackage != null)
                {
                    //binary package
                    //Gets the subscriber list with the matching origin type
                    var subscribers = GetPackageSubscriber(binaryPackage.OriginType);
                    foreach (var subscriber in subscribers)
                    {
                        subscriber.OnPackageReceived(binaryPackage);
                    }
                }
                else
                {
                    //system package
                    //determine, which package is it.
                }
            }
            catch (Exception ex)
            {
                SGL.Components.Get<EventManager>().Publish(new PackageReceiveExceptionEvent(ex.Message));
            }
        }
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        public void Connect(IPAddress ip)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    _tcpClient.Connect(new IPEndPoint(ip, 2563));
                    _nStream = _tcpClient.GetStream();
                }
                catch (Exception ex)
                {
                    SGL.Components.Get<EventManager>().Publish(new SocketExceptionEvent(ex.Message));
                }
            });
        }
        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        public void Disconnect()
        {
            if (_tcpClient.Connected)
            {
                _tcpClient.Close();
            }
        }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(IPackageListener subscriber)
        {
            _packageListeners.Add(subscriber);
        }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        public void Subscribe(ClientListener subscriber)
        {
            _clientListeners.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IPackageListener unsubscriber)
        {
            _packageListeners.Remove(unsubscriber);
        }
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(ClientListener unsubscriber)
        {
            _clientListeners.Remove(unsubscriber);
        }

        #endregion

        private readonly TcpClient _tcpClient;
        private NetworkStream _nStream;
        private readonly List<IPackageListener> _packageListeners;
        private readonly List<ClientListener> _clientListeners;

        /// <summary>
        /// Initializes a new LocalClient class.
        /// </summary>
        public LocalClient()
        {
            _tcpClient = new TcpClient();
            _packageListeners = new List<IPackageListener>();
            _clientListeners = new List<ClientListener>();
        }

        /// <summary>
        /// Gets a list of all matching package listeners.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>List of package listeners</returns>
        private IEnumerable<IPackageListener> GetPackageSubscriber(Type type)
        {
            var listenerContext = new List<IPackageListener>();
            for (var i = 0; i <= _packageListeners.Count - 1; i++)
            {
                if (_packageListeners[i].ListenerType == type)
                {
                    listenerContext.Add(_packageListeners[i]);
                }
            }
            return listenerContext;
        }
    }
}
