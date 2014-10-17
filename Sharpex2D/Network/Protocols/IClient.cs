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

using System.Net;
using Sharpex2D.Network.Logic;

namespace Sharpex2D.Network.Protocols
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IClient : ISender, IReceiver
    {
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        void Connect(IPAddress ip);

        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IPackageListener subscriber);

        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IClientListener subscriber);

        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IPackageListener unsubscriber);

        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IClientListener unsubscriber);
    }
}