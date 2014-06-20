using System;
using System.Collections.Generic;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class Language
    {
        public Language()
        {
            LocalizedValues = new List<LocalizedValue>();
        }

        /// <summary>
        ///     Gets or sets the guid of the language.
        /// </summary>
        public Guid Guid { set; get; }

        public List<LocalizedValue> LocalizedValues { set; get; }
    }
}