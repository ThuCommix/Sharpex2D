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
using System.Net;

namespace Sharpex2D.Framework.Network.Packages
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IBasePackage
    {
        /// <summary>
        ///     Gets or sets the type of the package Content
        /// </summary>
        Type OriginType { get; }

        /// <summary>
        ///     Gets or sets the serialized package content.
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        ///     Sets or gets the package identifer.
        /// </summary>
        /// <remarks>This is not neccessary for serialization.</remarks>
        string Identifer { set; get; }

        /// <summary>
        ///     Gets the sender.
        /// </summary>
        IPAddress Sender { get; }

        /// <summary>
        ///     Gets the receiver.
        /// </summary>
        IPAddress Receiver { get; set; }

        /// <summary>
        ///     Serializes an object into the Content.
        /// </summary>
        /// <param name="content">The Object.</param>
        void SerializeContent(object content);

        /// <summary>
        ///     Deserializes an object out of the Content.
        /// </summary>
        /// <returns>The Object.</returns>
        T DeserializeContent<T>();
    }
}