using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public Guid Name { set; get; }

        public List<LocalizedValue> LocalizedValues { set; get; }

    }
}
