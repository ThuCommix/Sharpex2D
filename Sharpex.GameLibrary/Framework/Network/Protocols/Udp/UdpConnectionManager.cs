using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace SharpexGL.Framework.Network.Protocols.Udp
{
    internal class UdpConnectionManager
    {
        /// <summary>
        /// Initializes a new UdpConnectionManager class.
        /// </summary>
        public UdpConnectionManager()
        {
            _pingRequests = new List<UdpPingRequest>();
        }

        private readonly List<UdpPingRequest> _pingRequests;
        private bool _isRunning;

        /// <summary>
        /// Adds a PingRequest to check.
        /// </summary>
        /// <param name="pingRequest"></param>
        public void AddPingRequest(UdpPingRequest pingRequest)
        {
            _pingRequests.Add(pingRequest);
        }

        /// <summary>
        /// Removes a PingRequest by ip.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public void RemoveByIP(IPAddress ipAddress)
        {
            for (var i = 0; i <= _pingRequests.Count - 1; i++)
            {
                if (Equals(_pingRequests[i].IP, ipAddress))
                {
                    _pingRequests.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Starts checking.
        /// </summary>
        public void Start()
        {
            _isRunning = true;
            var threadCheck = new Thread(InternalCheck) {IsBackground = true};
            threadCheck.Start();
        }

        /// <summary>
        /// Stops checking.
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
        }

        private void InternalCheck()
        {
            //Because we send a ping every 15 seconds, a client could be timed out
            //if it dont respond after 25 seconds

            var timeSpan = new TimeSpan(0, 0, 25);
            var removeList = new List<UdpPingRequest>();

            while (_isRunning)
            {

                for (var i = 0; i <= _pingRequests.Count - 1; i++)
                {
                    if (DateTime.Now - _pingRequests[i].Timestamp > timeSpan)
                    {
                        //the client highly likely timed out.
                        if (PingTimedOut != null)
                        {
                            PingTimedOut(this, _pingRequests[i].IP);
                        }
                        removeList.Add(_pingRequests[i]);
                    }
                }

                //remove timed out entries

                foreach (var item in removeList)
                {
                    _pingRequests.Remove(item);
                }

                removeList.Clear();

                Thread.Sleep(15000);
            }
        }

        public delegate void PingTimedOutEventHandler(object sender, IPAddress ipAddress);

        /// <summary>
        /// Called if a PingRequest timed out.
        /// </summary>
        public event PingTimedOutEventHandler PingTimedOut;
    }
}
