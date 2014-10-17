// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Sharpex2D.Debug.Logging;
using Sharpex2D.Network.Logic;
using Sharpex2D.Network.Packages;
using Sharpex2D.Network.Packages.System;

namespace Sharpex2D.Network.Protocols.Local
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class LocalClient : IClient, IDisposable
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
                _logger.Error(ex.Message);
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
                _logger.Error(ex.Message);
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
                    _logger.Error(ex.Message);
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
        public void Subscribe(IClientListener subscriber)
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
        public void Unsubscribe(IClientListener unsubscriber)
        {
            _clientListeners.Remove(unsubscriber);
        }

        #endregion

        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    _tcpClient.Close();
                }
            }
        }

        #endregion

        private const int IdleMax = 30;
        private readonly List<IClientListener> _clientListeners;
        private readonly Logger _logger;
        private readonly List<IPackageListener> _packageListeners;
        private readonly TcpClient _tcpClient;
        private int _currentIdle;
        private int _idleTimeout;
        private NetworkStream _nStream;

        /// <summary>
        /// Initializes a new LocalClient class.
        /// </summary>
        public LocalClient()
        {
            _tcpClient = new TcpClient();
            _packageListeners = new List<IPackageListener>();
            _clientListeners = new List<IClientListener>();
            _logger = LogManager.GetClassLogger();
        }

        /// <summary>
        /// A value indicating whether the client is connected.
        /// </summary>
        public bool Connected
        {
            get { return _tcpClient != null && _tcpClient.Connected; }
        }

        /// <summary>
        /// Gets a list of all matching package listeners.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>List of package listeners</returns>
        private IEnumerable<IPackageListener> GetPackageSubscriber(Type type)
        {
            var listenerContext = new List<IPackageListener>();
            for (int i = 0; i <= _packageListeners.Count - 1; i++)
            {
                //if listener type is null go to next
                if (_packageListeners[i].ListenerType == null)
                {
                    continue;
                }

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
                    IBasePackage package = _tcpClient.Available > 0 ? PackageSerializer.Deserialize(_nStream) : null;
                    if (package == null)
                    {
                        //Idle the client.
                        Idle();
                        continue;
                    }
                    var binaryPackage = package as BinaryPackage;
                    if (binaryPackage != null)
                    {
                        //binary package
                        //Gets the subscriber list with the matching origin type
                        IEnumerable<IPackageListener> subscribers = GetPackageSubscriber(binaryPackage.OriginType);
                        foreach (IPackageListener subscriber in subscribers)
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
                                for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientJoined(notificationPackage.Connection[0]);
                                }
                                break;
                                //client exited
                            case NotificationMode.ClientExited:
                                for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientExited(notificationPackage.Connection[0]);
                                }
                                break;
                                //client listing
                            case NotificationMode.ClientList:
                                for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnClientListing(notificationPackage.Connection);
                                }
                                break;
                                //server shutdown
                            case NotificationMode.ServerShutdown:
                                for (int i = 0; i <= _clientListeners.Count - 1; i++)
                                {
                                    _clientListeners[i].OnServerShutdown();
                                    _tcpClient.Close();
                                }
                                break;
                                //we timed out.
                            case NotificationMode.TimeOut:
                                for (int i = 0; i <= _clientListeners.Count - 1; i++)
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

                    _logger.Error("Received unknown package.");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// Idles the thread.
        /// </summary>
        private void Idle()
        {
            //Idle to save cpu power.   
            _currentIdle++;

            if (_idleTimeout > 0)
            {
                Thread.Sleep(_idleTimeout);
            }

            if (_currentIdle < IdleMax) return;

            _currentIdle = 0;
            if (_idleTimeout < 15)
            {
                _idleTimeout++;
            }
        }
    }
}