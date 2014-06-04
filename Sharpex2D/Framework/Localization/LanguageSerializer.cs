using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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