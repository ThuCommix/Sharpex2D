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
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Game.Services.Availability
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class NetworkNotAvailableException : Exception
    {
        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        public NetworkNotAvailableException()
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public NetworkNotAvailableException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public NetworkNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public NetworkNotAvailableException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}