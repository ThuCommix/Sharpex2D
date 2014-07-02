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
using System.Threading;

namespace Sharpex2D.Framework.Network.Protocols.Udp
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal class UdpConnectionManager
    {
        public delegate void PingTimedOutEventHandler(object sender, IPAddress ipAddress);

        private readonly List<UdpPingRequest> _pingRequests;
        private bool _isRunning;

        /// <summary>
        ///     Initializes a new UdpConnectionManager class.
        /// </summary>
        public UdpConnectionManager()
        {
            _pingRequests = new List<UdpPingRequest>();
        }

        /// <summary>
        ///     Adds a PingRequest to check.
        /// </summary>
        /// <param name="pingRequest"></param>
        public void AddPingRequest(UdpPingRequest pingRequest)
        {
            _pingRequests.Add(pingRequest);
        }

        /// <summary>
        ///     Removes a PingRequest by ip.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public void RemoveByIP(IPAddress ipAddress)
        {
            for (int i = 0; i <= _pingRequests.Count - 1; i++)
            {
                if (Equals(_pingRequests[i].IP, ipAddress))
                {
                    _pingRequests.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        ///     Starts checking.
        /// </summary>
        public void Start()
        {
            _isRunning = true;
            var threadCheck = new Thread(InternalCheck) {IsBackground = true};
            threadCheck.Start();
        }

        /// <summary>
        ///     Stops checking.
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
                for (int i = 0; i <= _pingRequests.Count - 1; i++)
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

                foreach (UdpPingRequest item in removeList)
                {
                    _pingRequests.Remove(item);
                }

                removeList.Clear();

                Thread.Sleep(15000);
            }
        }

        /// <summary>
        ///     Called if a PingRequest timed out.
        /// </summary>
        public event PingTimedOutEventHandler PingTimedOut;
    }
}