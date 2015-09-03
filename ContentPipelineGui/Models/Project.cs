using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ContentPipelineUI.Models
{
    public class Project
    {
        /// <summary>
        /// Gets or sets the project title.
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <remarks>This should be the version of Sharpex2D you currently using.</remarks>
        public string Version { set; get; }

        /// <summary>
        /// Gets or sets the path of the content pipeline.
        /// </summary>
        public string Path { set; get; }

        /// <summary>
        /// Gets or sets the source path.
        /// </summary>
        public string Source { set; get; }

        /// <summary>
        /// Gets or sets the target path.
        /// </summary>
        public string Target { set; get; }

        /// <summary>
        /// Gets the list of all possible plugin positions.
        /// </summary>
        public List<string> Plugins { get; }

        /// <summary>
        /// Initializes a new Project class.
        /// </summary>
        public Project()
        {
            Plugins = new List<string>();
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="path">The Path.</param>
        public void Save(string path)
        {
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            xml.Add(new XElement("Project", new XElement("Title", Title), new XElement("Version", Version.ToString()),
                new XElement("Path", Path), new XElement("Source", Source), new XElement("Target", Target), new XElement("Plugins")));

            var xmlNode = xml.Element("Project").Element("Plugins");

            foreach (var assemblyPath in Plugins)
            {
                xmlNode.Add(new XElement("Plugin", assemblyPath));
            }

            xml.Save(path);
        }

        /// <summary>
        /// Loads a project from xml.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>Project.</returns>
        public static Project LoadFromXml(string path)
        {
            var project = new Project();
            var xml = XDocument.Load(path);

            project.Title = xml.Element("Project").Element("Title").Value;
            project.Version = xml.Element("Project").Element("Version").Value;
            project.Path = xml.Element("Project").Element("Path").Value;
            project.Source = xml.Element("Project").Element("Source").Value;
            project.Target = xml.Element("Project").Element("Target").Value;
            foreach (var entry in xml.Element("Project").Element("Plugins").Elements("Plugins"))
            {
                project.Plugins.Add(entry.Value);
            }

            return project;
        }
    }
}

