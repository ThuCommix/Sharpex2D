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

using System.IO;

namespace Sharpex2D.Framework.Content.Importers
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public abstract class Importer
    {
        private XmlContent _xmlContent;

        /// <summary>
        /// Raises when the xml content is loaded and ready for processing.
        /// </summary>
        /// <param name="xmlContent">The XmlContent.</param>
        /// <returns>IContent.</returns>
        public abstract IContent OnCreate(XmlContent xmlContent);

        /// <summary>
        /// Loads the xml content.
        /// </summary>
        /// <param name="file">The File.</param>
        internal void LoadXmlContent(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                LoadXmlContent(fileStream);
            }
        }

        /// <summary>
        /// Loads the xml content.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        internal void LoadXmlContent(Stream stream)
        {
            var xmlContent = XmlContent.FromStream(stream);
            if (ValidateType(xmlContent))
            {
                _xmlContent = xmlContent;
            }
            else
            {
                throw new InvalidXmlContentTypeException(
                    string.Format("The XmlContent type ({0}) does not match the importer type ({1}).",
                        _xmlContent.Type.FullName,
                        AttributeHelper.GetAttribute<ImportContentAttribute>(GetType()).Type.FullName));
            }
        }

        /// <summary>
        /// Imports the xml content.
        /// </summary>
        /// <returns>IContent.</returns>
        internal IContent ImportXmlContent()
        {
            return OnCreate(_xmlContent);
        }

        /// <summary>
        /// Unloads the xml content.
        /// </summary>
        internal void UnloadXmlContent()
        {
            _xmlContent.Dispose();
            _xmlContent = null;
        }

        /// <summary>
        /// Gets a value indicating whether the xml content type is the same as declared in the ImportContent attribute.
        /// </summary>
        /// <param name="xmlContent">The XmlContent.</param>
        /// <returns>True if the type matchs.</returns>
        internal bool ValidateType(XmlContent xmlContent)
        {
            var type = AttributeHelper.GetAttribute<ImportContentAttribute>(GetType()).Type;
            return xmlContent.Type.FullName == type.FullName;
        }
    }
}
