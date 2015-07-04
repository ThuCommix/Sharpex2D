// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Network.Packages.System
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Serializable]
    internal class NotificationPackage : BasePackage
    {
        /// <summary>
        /// Initializes a new NotificationPackage class.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        public NotificationPackage(IConnection[] connection)
        {
            Connection = connection;
            Mode = NotificationMode.None;
        }

        /// <summary>
        /// Initializes a new NotificationPackage class.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        /// <param name="mode">The NotificationMode.</param>
        public NotificationPackage(IConnection[] connection, NotificationMode mode)
        {
            Connection = connection;
            Mode = mode;
        }

        /// <summary>
        /// Sets or gets the NotificationMode.
        /// </summary>
        public NotificationMode Mode { set; get; }

        /// <summary>
        /// Gets the corresponding connection.
        /// </summary>
        public IConnection[] Connection { private set; get; }
    }
}