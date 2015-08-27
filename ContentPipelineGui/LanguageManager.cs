using System;
using System.Threading;
using System.Windows;

namespace ContentPipelineUI
{
    public class LanguageManager
    {
        /// <summary>
        /// Setup the language.
        /// </summary>
        /// <param name="source">The ResourceDictionary.</param>
        public static void SetupLanguage(ResourceDictionary source)
        {
            var resourceDictionary = new ResourceDictionary();

            try
            {
                resourceDictionary.Source = new Uri(string.Format("..\\Resources\\{0}.xaml", Thread.CurrentThread.CurrentCulture), UriKind.Relative);
            }
            catch
            {
                resourceDictionary.Source = new Uri("..\\Resources\\en-US.xaml", UriKind.Relative);
            }

            source.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
