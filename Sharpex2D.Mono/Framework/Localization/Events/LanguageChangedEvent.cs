using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Localization.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class LanguageChangedEvent : IEvent
    {
        /// <summary>
        ///     Creates a new LanguageChanged Event.
        /// </summary>
        /// <param name="language">The SelectedLanguage.</param>
        public LanguageChangedEvent(Language language)
        {
            SelectedLanguage = language;
        }

        /// <summary>
        ///     Gets the SelectedLanguage.
        /// </summary>
        public Language SelectedLanguage { get; private set; }
    }
}