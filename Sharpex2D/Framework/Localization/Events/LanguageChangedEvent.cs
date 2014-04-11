using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Localization.Events
{
    public class LanguageChangedEvent : IEvent
    {
        /// <summary>
        /// Creates a new LanguageChanged Event.
        /// </summary>
        /// <param name="language">The SelectedLanguage.</param>
        public LanguageChangedEvent(Language language)
        {
            SelectedLanguage = language;
        }
        /// <summary>
        /// Gets the SelectedLanguage.
        /// </summary>
        public Language SelectedLanguage { get; private set; }
    }
}
