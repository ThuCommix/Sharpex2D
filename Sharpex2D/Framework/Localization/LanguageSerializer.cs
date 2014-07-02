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

using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class LanguageSerializer
    {
        /// <summary>
        ///     Serializes the given Language.
        /// </summary>
        /// <param name="path">The Filepath.</param>
        /// <param name="language">The Language.</param>
        public static void Serialize(string path, Language language)
        {
            var serializer = new XmlSerializer(typeof (Language));
            var xmlWriter = new StreamWriter(path, false, Encoding.UTF8);
            serializer.Serialize(xmlWriter, language);
            xmlWriter.Close();
        }

        /// <summary>
        ///     Deserializes a Language.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>Language</returns>
        public static Language Deserialize(string path)
        {
            var serializer = new XmlSerializer(typeof (Language));
            var xmlReader = new StreamReader(path, Encoding.UTF8);
            var language = (Language) serializer.Deserialize(xmlReader);
            xmlReader.Close();
            return language;
        }
    }
}