using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Localization
{
    [Serializable]
    public class LocalizedValue
    {
        /// <summary>
        /// Gets or sets the Id of the LocalizedValue.
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// Sets or gets the value of the LocalizedString.
        /// </summary>
        public string LocalizedString { set; get; }
    }
}
