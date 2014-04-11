using System;
using System.IO;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Scripting.VB
{
    public class VBScriptLoader : IContentExtension
    {
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("9E03FBEF-80A3-4E38-B99A-12A29BC38022"); } }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType { get { return typeof(VBScript); } }
        /// <summary>
        /// Creates the Content.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>IContent</returns>
        public IContent Create(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            return new VBScript { Content = File.ReadAllText(path) };
        }
    }
}
