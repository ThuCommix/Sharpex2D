using System;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class LocalizedValue
    {
        /// <summary>
        ///     Gets or sets the Id of the LocalizedValue.
        /// </summary>
        public string Id { set; get; }

        /// <summary>
        ///     Sets or gets the value of the LocalizedString.
        /// </summary>
        public string LocalizedString { set; get; }
    }
}