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
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sharpex2D.Network.Packages
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class BasePackage : IBasePackage
    {
        /// <summary>
        /// Gets or sets the type of the package Content
        /// </summary>
        public Type OriginType { get; private set; }

        /// <summary>
        /// Gets or sets the serialized package content.
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Sets or gets the package identifer.
        /// </summary>
        /// <remarks>This is not neccessary for serialization.</remarks>
        public string Identifer { get; set; }

        /// <summary>
        /// Serializes an object into the Content.
        /// </summary>
        /// <param name="content">The Object.</param>
        public void SerializeContent(object content)
        {
            using (var mStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(mStream, content);
                Content = mStream.ToArray();
                OriginType = content.GetType();
            }
        }

        /// <summary>
        /// Deserializes an object out of the Content.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <remarks>The type is determined by the OriginType Property.</remarks>
        /// <returns>The Object.</returns>
        public T DeserializeContent<T>()
        {
            using (var mStream = new MemoryStream(Content))
            {
                return (T) new BinaryFormatter().Deserialize(mStream);
            }
        }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        public IPAddress Sender { internal set; get; }

        /// <summary>
        /// Gets the receiver.
        /// </summary>
        public IPAddress Receiver { set; get; }
    }
}