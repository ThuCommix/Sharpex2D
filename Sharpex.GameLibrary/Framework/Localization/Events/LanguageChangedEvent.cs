using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Localization.Events
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
