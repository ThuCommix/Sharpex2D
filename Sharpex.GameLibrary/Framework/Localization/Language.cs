using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Localization
{
    [Serializable]
    public class Language
    {
        public Language()
        {
            LocalizedValues = new List<LocalizedValue>();
        }
        /// <summary>
        /// Gets or sets the guid of the language.
        /// </summary>
        public Guid Guid { set; get; }

        public List<LocalizedValue> LocalizedValues { set; get; }

    }
}
