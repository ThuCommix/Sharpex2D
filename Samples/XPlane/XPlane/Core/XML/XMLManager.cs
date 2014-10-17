
using System.IO;
using System.Xml.Serialization;

namespace XPlane.Core.XML
{
    public class XmlManager<T>
    {
        /// <summary>
        /// Saves an object to xml.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <param name="obj">The Object.</param>
        public void Save(string path, T obj)
        {
            if (File.Exists(path))
                File.Delete(path);

            using (var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof (T));
                serializer.Serialize(fileStream, obj);
            }
        }

        /// <summary>
        /// Loads the object from xml.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>TObject.</returns>
        public T Load(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var deserializer = new XmlSerializer(typeof (T));
                return (T)deserializer.Deserialize(fileStream);
            }
        }
    }
}
