using System;

namespace SharpexGL.Framework.Content
{
    public interface IContentExtension
    {
        /// <summary>
        /// Gets the Guid of the ContentExtension.
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        Type ContentType { get; }
        /// <summary>
        /// Creates the IContent from the given Path.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>IContent</returns>
        IContent Create(string path);
    }
}
