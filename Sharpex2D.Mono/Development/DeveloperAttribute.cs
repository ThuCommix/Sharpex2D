using System;

namespace Sharpex2D
{
    public class DeveloperAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new DeveloperAttribute class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="contact">The contact option.</param>
        public DeveloperAttribute(string name, string contact)
        {
            Developer = name;
            Contact = contact;
        }

        /// <summary>
        ///     Gets the Developer name.
        /// </summary>
        public string Developer { private set; get; }

        /// <summary>
        ///     Gets the contact option.
        /// </summary>
        public string Contact { private set; get; }
    }
}