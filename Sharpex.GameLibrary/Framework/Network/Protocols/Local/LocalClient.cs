using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Network.Events;
using SharpexGL.Framework.Network.Logic;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Packages.System;

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
        public void BeginReceive()
        {
            if (!_tcpClient.Connected) throw new InvalidOperationException("The client is not connected.");
            var beginReceiveHandler = new Thread(InternalBeginReceive) {IsBackground = true};
            beginReceiveHandler.Start();
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
        /// A value indicating whether the client is connected.
        /// </summary>
        public bool Connected{
            get { return _tcpClient != null && _tcpClient.Connected; }
        }

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

        /// <summary>
        /// Starts receiving data.
        /// </summary>
        private void InternalBeginReceive()
        {
            while (_tcpClient.Connected)
            {
                try
                {
                    var package = _tcpClient.Available > 0 ? PackageSerializer.Deserialize(_nStream) : null;
                    if (package == null)
                    {
                        continue;
                    }
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

                        //Nothing more to do.
                        continue;
                    }
                    //determine, which package is it.
                    var notificationPackage = package as NotificationPackage;
                    if (notificationPackage != null)
                    {
                        //Notificationpackage

                        switch (notificationPackage.Mode)
                        {
                            //client joined
                            case NotificationMode.ClientJoined:
                                for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientJoined(notificationPackage.Connection[0]);
                                }
                                break;
                            //client exited
                            case NotificationMode.ClientExited:
                                for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientExited(notificationPackage.Connection[0]);
                                }
                                break;
                            //client listing
                            case NotificationMode.ClientList:
                                for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientListing(notificationPackage.Connection);
                                }
                                break;
                            //server shutdown
                            case NotificationMode.ServerShutdown:
                                for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnServerShutdown();
                                    _tcpClient.Close();
                                }
                                break;
                            //we timed out.
                            case NotificationMode.TimeOut:
                                for (var i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientTimedOut();
                                }
                                _tcpClient.Close();
                                break;
                        }

                        //exit sub
                        continue;
                    }
                    var pingPackage = package as PingPackage;
                    if (pingPackage != null)
                    {
                        //Pingpackage
                        //Send the ping package back to the server.
                        Send(pingPackage);
                        //exit sub
                        continue;
                    }

                    SGL.Components.Get<EventManager>().Publish(new UnknownPackageExceptionEvent());
                }
                catch (Exception ex)
                {
                    SGL.Components.Get<EventManager>().Publish(new PackageReceiveExceptionEvent(ex.Message));
                }
            }
        }
    }
}
