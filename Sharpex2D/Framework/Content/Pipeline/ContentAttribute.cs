using System;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ContentAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new ContentAttribute class.
        /// </summary>
        /// <param name="displayName">The DisplayName.</param>
        public ContentAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        ///     Gets the DisplayName.
        /// </summary>
        public string DisplayName { private set; get; }
    }
}