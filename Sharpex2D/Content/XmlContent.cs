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
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class XmlContent : IDisposable, IEnumerable<XmlContentMetaData>
    {
        // ReSharper disable PossibleNullReferenceException
        private XDocument _xmlFile;
        private XmlContentCompression _compression;
        private List<XmlContentMetaData> _metadata; 

        /// <summary>
        /// Initializes a new XmlContent class.
        /// </summary>
        public XmlContent()
        {
            _metadata = new List<XmlContentMetaData>();
            _compression = XmlContentCompression.None;
            _xmlFile = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            _xmlFile.Add(
                new XElement("Content", new XAttribute("Target", GetType().Assembly.GetName().Version),
                    new XAttribute("Type", ""), new XAttribute("Name", ""),
                new XElement("Data", new XAttribute("Compression", (int) Compression))));
        }

        /// <summary>
        /// Initializes a new XmlContent class.
        /// </summary>
        /// <param name="xml">The XDocument.</param>
        private XmlContent(XDocument xml)
        {
            ValidateXml(xml);

            _xmlFile = xml;
            _metadata = new List<XmlContentMetaData>();

            Type = Type.GetType(xml.Element("Content").Attribute("Type").Value);
            _compression =
                (XmlContentCompression)
                    int.Parse(_xmlFile.Element("Content").Element("Data").Attribute("Compression").Value);

            var elements = xml.Element("Content").Elements();

            foreach (var element in elements.Where(x => x.Name != "Data"))
            {
                _metadata.Add(new XmlContentMetaData(element.Name.ToString(), element.Value));
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<XmlContentMetaData> GetEnumerator()
        {
            return _metadata.GetEnumerator();
        }

        /// <summary>
        /// Deconstructs the XmlContent class.
        /// </summary>
        ~XmlContent()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Loads the XmlContent from file.
        /// </summary>
        /// <param name="fileName">The FileName.</param>
        /// <returns>XmlContent.</returns>
        public static XmlContent FromFile(string fileName)
        {
            return new XmlContent(XDocument.Load(fileName));
        }

        /// <summary>
        /// Loads the XmlContent from stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>XmlContent.</returns>
        public static XmlContent FromStream(Stream stream)
        {
            return new XmlContent(XDocument.Load(stream));
        }

        /// <summary>
        /// Gets the content compression.
        /// </summary>
        public XmlContentCompression Compression
        {
            private set
            {
                _compression = value;
                _xmlFile.Element("Content").Element("Data").Attribute("Compression").Value =
                    ((int) value).ToString(CultureInfo.InvariantCulture);
            }
            get { return _compression; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { private set; get; }

        /// <summary>
        /// A value indicating whether the xml content has meta data.
        /// </summary>
        public bool HasMetaData { get { return _metadata.Count > 0; } }

        /// <summary>
        /// A value indicating whether the xml content has data.
        /// </summary>
        public bool HasData
        {
            get { return _xmlFile.Element("Content").Element("Data").Value != ""; }
        }

        /// <summary>
        /// Adds a new meta data.
        /// </summary>
        /// <param name="metadata">The XmlContentMetaData.</param>
        public void Add(XmlContentMetaData metadata)
        {
            if (!_metadata.Contains(metadata))
            {
                _metadata.Add(metadata);
                _xmlFile.Element("Content").Add(new XElement(metadata.Name, metadata.Value));
            }
        }

        /// <summary>
        /// Removes an existing meta data.
        /// </summary>
        /// <param name="metadata">The XmlContentMetaData.</param>
        public void Remove(XmlContentMetaData metadata)
        {
            if (_metadata.Contains(metadata))
            {
                _metadata.Remove(metadata);
                if (_xmlFile.Element("Content").Element(metadata.Name) != null)
                {
                    _xmlFile.Element("Content").Element(metadata.Name).Remove();
                }
            }
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <param name="data">The Data.</param>
        public void SetData(byte[] data, Type type)
        {
            SetData(data, type, XmlContentCompression.None);
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="type">The Type.</param>
        /// <param name="compression">The XmlContentCompression.</param>
        public void SetData(byte[] data, Type type, XmlContentCompression compression)
        {
            Type = type;

            using (var memoryStream = new MemoryStream())
            {
                if (compression == XmlContentCompression.GZip)
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                    {
                        gzipStream.Write(data, 0, data.Length);
                    }
                }
                else if (compression == XmlContentCompression.Deflate)
                {
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                    {
                        deflateStream.Write(data, 0, data.Length);
                    }
                }
                else
                {
                    memoryStream.Write(data, 0, data.Length);
                }

                Compression = compression;
                _xmlFile.Element("Content").Attribute("Type").Value = type.FullName;
                _xmlFile.Element("Content").Attribute("Name").Value = type.Name;

                _xmlFile.Element("Content").Element("Data").Value = Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Sets the data stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="type">The Type.</param>
        public void SetDataStream(Stream stream, Type type)
        {
            SetDataStream(stream, type, XmlContentCompression.None);
        }

        /// <summary>
        /// Sets the data stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <param name="type">The Type.</param>
        /// <param name="compression">The XmlContentCompression.</param>
        public void SetDataStream(Stream stream, Type type, XmlContentCompression compression)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                SetData(memoryStream.ToArray(), type, compression);
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>Byte array containing the content data.</returns>
        public byte[] GetData()
        {
            var compression =
                (XmlContentCompression)
                    int.Parse(_xmlFile.Element("Content").Element("Data").Attribute("Compression").Value);
            var data = Convert.FromBase64String(_xmlFile.Element("Content").Element("Data").Value);

            using (var destination = new MemoryStream())
            {
                using (var memoryStream = new MemoryStream(data))
                {
                    if (compression == XmlContentCompression.GZip)
                    {
                        using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                        {
                            gzipStream.CopyTo(destination);
                        }

                        return destination.ToArray();
                    }
                    else if (compression == XmlContentCompression.Deflate)
                    {
                        using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                        {
                            deflateStream.CopyTo(destination);
                        }

                        return destination.ToArray();
                    }
                    else
                    {
                        return data;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the data stream.
        /// </summary>
        /// <returns></returns>
        public Stream GetDataStream()
        {
            return new MemoryStream(GetData());
        }

        /// <summary>
        /// Saves the xml content.
        /// </summary>
        /// <param name="destination">The Destination.</param>
        public void Save(string destination)
        {
            using (var fileStream = new FileStream(destination, FileMode.Create, FileAccess.Write))
            {
                Save(fileStream);
            }
        }

        /// <summary>
        /// Saves the xml content.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        public void Save(Stream stream)
        {
            _xmlFile.Save(stream);
        }

        /// <summary>
        /// Validates the xml.
        /// </summary>
        /// <param name="xml">The XDocument.</param>
        private void ValidateXml(XDocument xml)
        {
            if (xml == null)
                throw new ArgumentNullException();
            if (xml.Element("Content") == null)
                throw new InvalidXmlContentException("Content element is not existent.");
            if (xml.Element("Content").Element("Data") == null)
                throw new InvalidXmlContentException("Data element is not existent.");
            if (xml.Element("Content").Attribute("Target") == null)
                throw new InvalidXmlContentException("Target attribute is not existent.");
            if (xml.Element("Content").Attribute("Type") == null)
                throw new InvalidXmlContentException("Type attribute is not existent.");
            if (xml.Element("Content").Attribute("Name") == null)
                throw new InvalidXmlContentException("Name attribute is not existent.");
            if (xml.Element("Content").Element("Data").Attribute("Compression") == null)
                throw new InvalidXmlContentException("Compression attribute is not existent.");

            if (GetType().Assembly.GetName().Version !=
                new Version(xml.Element("Content").Attribute("Target").Value))
            {
                throw new DeprecatedXmlContentException(
                    string.Format(
                        "The xml content data ({0}) is not compatible with the current library version ({1}).",
                        xml.Element("Content").Attribute("Target").Value, GetType().Assembly.GetName().Version));
            }
        }

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
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _xmlFile = null;
                _metadata = null;
            }
        }
    }
}
