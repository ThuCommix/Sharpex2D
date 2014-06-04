using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Localization.Events;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public class LanguageProvider
    {
        private readonly List<Language> _languages;
        private Language _currentLanguage;

        public LanguageProvider()
        {
            _languages = new List<Language>();
        }

        /// <summary>
        ///     Changes the current language.
        /// </summary>
        /// <param name="guid">The LanguageGuide.</param>
        public void ChangeLanguage(Guid guid)
        {
            Language lang = _languages.FirstOrDefault(language => language.Guid == guid);
            if (lang != null)
            {
                _currentLanguage = lang;
                SGL.Components.Get<EventManager>().Publish(new LanguageChangedEvent(lang));
            }
            else
            {
                throw new InvalidOperationException("Language " + guid + " not exists.");
            }
        }

        /// <summary>
        ///     Gets the LocalizedString from the current Language.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>String</returns>
        public string GetLocalizedString(string id)
        {
            if (_currentLanguage == null)
                throw new InvalidOperationException(
                    "Unable to get localizedString due there is no language selected. Use ChangeLanguage() before.");

            foreach (var localized in _currentLanguage.LocalizedValues.Where(localized => localized.Id == id))
            {
                return localized.LocalizedString;
            }
            throw new InvalidOperationException("LocalizedString Id not found in " + _currentLanguage.Guid);
        }

        /// <summary>
        ///     Loads a language based on the Path.
        /// </summary>
        /// <param name="path">The Filepath.</param>
        public void LoadLanguage(string path)
        {
            try
            {
                _languages.Add(LanguageSerializer.Deserialize(path));
            }
            catch (Exception)
            {
                throw new LanguageSerializationException("Error while deserializing " + path);
            }
        }

        /// <summary>
        ///     Loads a languages in the given Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        public void LoadLanguagesFromDirectory(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                try
                {
                    _languages.Add(LanguageSerializer.Deserialize(file));
                }
                catch (Exception)
                {
                    throw new LanguageSerializationException("Error while deserializing " + file);
                }
            }
        }
    }
}