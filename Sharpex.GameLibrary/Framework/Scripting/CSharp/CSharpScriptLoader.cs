using System;
using System.IO;
using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Scripting.CSharp
{
    public class CSharpScriptLoader : IContentExtension
    {
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("7E434340-EDCC-42FD-85BC-044536E5D02B"); } }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType { get { return typeof (CSharpScript); } }
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

            return new CSharpScript {Content = File.ReadAllText(path)};
        }
    }
}
