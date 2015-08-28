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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ExtensibleContentFormat : IEnumerable<MetaInformation>
    {
        private readonly List<MetaInformation> _metaInformations;
        private string _basePath;
        private long _dataOffset;

        /// <summary>
        /// Initializes a new ExtensibleContentFormat class
        /// </summary>
        /// <param name="type">The Type</param>
        public ExtensibleContentFormat(Type type)
        {
            _metaInformations = new List<MetaInformation>
            {
                new MetaInformation("_CONTENT_VERSION_", typeof (GameHost).Assembly.GetName().Version.ToString()),
                new MetaInformation("_FULL_TYPE_", type.FullName)
            };
        }

        /// <summary>
        /// Initializes a new ExtensibleContentFormat class
        /// </summary>
        /// <param name="metaInformations">The MetaInformations</param>
        private ExtensibleContentFormat(List<MetaInformation> metaInformations)
        {
            _metaInformations = metaInformations;
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<MetaInformation> GetEnumerator()
        {
            return _metaInformations.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets a value indicating whether the specified type matches the type of the loaded file
        /// </summary>
        /// <param name="expectedType">The ExpectedType</param>
        /// <returns>True if the type matches</returns>
        public bool EnsureCorrectType(Type expectedType)
        {
            var result = _metaInformations.FirstOrDefault(x => x.Key == "_FULL_TYPE_");
            if (result == null)
                return false;

            return expectedType.FullName == result.Value;
        }

        /// <summary>
        /// Adds a new meta information
        /// </summary>
        /// <param name="metaInformation">The MetaInformation</param>
        public void AddMetaInfo(MetaInformation metaInformation)
        {
            if (!_metaInformations.Contains(metaInformation))
            {
                _metaInformations.Add(metaInformation);
            }
        }

        /// <summary>
        /// Adds a new range of meta informations
        /// </summary>
        /// <param name="metaInformations">The MetaInformations</param>
        public void AddMetaInfos(IEnumerable<MetaInformation> metaInformations)
        {
            foreach (var metaInformation in metaInformations)
            {
                AddMetaInfo(metaInformation);
            }
        }

        /// <summary>
        /// Loads the specified file
        /// </summary>
        /// <param name="file">The File</param>
        /// <returns>ExtensibleContentFormat</returns>
        public static ExtensibleContentFormat LoadFromFile(string file)
        {
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var dataOffset = binaryReader.ReadInt64();
                    var metaCollection = new List<MetaInformation>();
                    while (binaryReader.BaseStream.Position < dataOffset)
                    {
                        metaCollection.Add(new MetaInformation(binaryReader.ReadString(), binaryReader.ReadString()));
                    }

                    return new ExtensibleContentFormat(metaCollection) {_dataOffset = dataOffset, _basePath = file};
                }
            }
        }

        /// <summary>
        /// Saves the data to the specified destination using the specified data
        /// </summary>
        /// <param name="destination">The Destination</param>
        /// <param name="data">The Data</param>
        public void Save(string destination, byte[] data)
        {
            using (var stream = new FileStream(destination, FileMode.Create, FileAccess.Write))
            {
                using (var binaryWriter = new BinaryWriter(stream))
                {
                    binaryWriter.Write(0L);

                    foreach (var metaInformation in _metaInformations)
                    {
                        binaryWriter.Write(metaInformation.Key);
                        binaryWriter.Write(metaInformation.Value);
                    }

                    var dataOffset = binaryWriter.BaseStream.Position;
                    binaryWriter.Seek(0, SeekOrigin.Begin);
                    binaryWriter.Write(dataOffset);
                    binaryWriter.Seek((int) dataOffset, SeekOrigin.Begin);
                    binaryWriter.Write(data);
                }
            }
        }

        /// <summary>
        /// Gets the data stream
        /// </summary>
        /// <returns>Stream</returns>
        public Stream GetDataStream()
        {
            return new ContentStream(_basePath, _dataOffset);
        }

        /// <summary>
        /// Gets the data
        /// </summary>
        /// <returns>Byte array</returns>
        public byte[] GetData()
        {
            using (var stream = GetDataStream())
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}