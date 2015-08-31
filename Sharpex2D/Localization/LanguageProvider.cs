// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sharpex2D.Framework.Localization
{
    public class LanguageProvider
    {
        private readonly List<Language> _languages;
        private Language _currentLanguage;

        /// <summary>
        /// Initalizes a new LanguageProvider class
        /// </summary>
        public LanguageProvider()
        {
            _languages = new List<Language>();
        }

        /// <summary>
        /// LanguageChanged event.
        /// </summary>
        public event EventHandler<LanguageEventArgs> LanguageChanged;

        /// <summary>
        /// Changes the current language.
        /// </summary>
        /// <param name="guid">The LanguageGuide.</param>
        public void ChangeLanguage(Guid guid)
        {
            Language lang = _languages.FirstOrDefault(language => language.Guid == guid);
            if (lang != null)
            {
                _currentLanguage = lang;

                if (LanguageChanged != null)
                {
                    LanguageChanged(this, new LanguageEventArgs(_currentLanguage));
                }
            }
            else
            {
                throw new InvalidOperationException("Language " + guid + " not exists.");
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

            foreach (LocalizedValue localized in _currentLanguage.LocalizedValues.Where(localized => localized.Id == id)
                )
            {
                return localized.LocalizedString;
            }
            throw new InvalidOperationException("LocalizedString Id not found in " + _currentLanguage.Guid);
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
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                try
                {
                    _languages.Add(LanguageSerializer.Deserialize(file));
                }
                catch (Exception)
                {
                    Logger.Instance.Warn($"Error while deserializing {file}.");
                }
            }
        }
    }
}
