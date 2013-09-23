using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Localization.Events;

namespace SharpexGL.Framework.Localization
{
    public class LanguageProvider
    {
        public LanguageProvider()
        {
            _languages = new List<Language>();
        }

        private List<Language> _languages;
        private Language _currentLanguage;

        /// <summary>
        /// Changes the current language.
        /// </summary>
        /// <param name="name">The Languagename.</param>
        public void ChangeLanguage(string name)
        {
            var lang = _languages.FirstOrDefault(language => language.Name == name);
            if (lang != null)
            {
                _currentLanguage = lang;
                SGL.Components.Get<EventManager>().Publish(new LanguageChangedEvent(lang));
            }
            else
            {
                throw new InvalidOperationException("Language " + name + " not exists.");
            }
        }

        /// <summary>
        /// Gets the LocalizedString from the current Language.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>String</returns>
        public string GetLocalizedString(string id)
        {
            if (_currentLanguage == null)
                throw new InvalidOperationException(
                    "Unable to get localizedString due there is no language selected. Use ChangeLanguage() before.");

            foreach (var localized in _currentLanguage.LocalizedValues)
            {
                if (localized.Id == id)
                {
                    return localized.LocalizedString;
                }
            }
            throw new InvalidOperationException("LocalizedString Id not found in " + _currentLanguage.Name);
        }

        /// <summary>
        /// Loads a language based on the Path.
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
        /// Loads a languages in the given Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        public void LoadLanguagesFromDirectory(string directoryPath)
        {
            var files = SGL.Components.Get<ContentManager>().FileSystem.GetFiles(directoryPath);
            foreach (var file in files)
            {
                try
                {
                    _languages.Add(LanguageSerializer.Deserialize(file));
                }
                catch(Exception)
                {
                    throw new LanguageSerializationException("Error while deserializing " + file);
                }
            }
        }
    }
}
