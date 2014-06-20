using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sharpex2D.Framework.Game.Services.Settings
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    [Serializable]
    public class GameSettings : IGameService
    {
        internal readonly Dictionary<string, object> Settings;

        /// <summary>
        ///     Initializes a new GameSettings class.
        /// </summary>
        public GameSettings()
        {
            Settings = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Adds a new Setting to the GameSettings class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="value">The Value.</param>
        public void AddSetting(string name, object value)
        {
            if (!Settings.ContainsKey(name))
            {
                Settings.Add(name, value);
                return;
            }

            throw new ArgumentException("A setting with the same name already exists.");
        }

        /// <summary>
        ///     Returns a SettingValue.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <returns>Object</returns>
        public object GetSetting(string name)
        {
            if (Settings.ContainsKey(name))
            {
                return Settings[name];
            }

            throw new ArgumentException("The setting does not exist.");
        }

        /// <summary>
        ///     Returns a SettingValue.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="name">The Name.</param>
        /// <returns>T.</returns>
        public T GetSetting<T>(string name)
        {
            if (Settings.ContainsKey(name))
            {
                return (T) Settings[name];
            }

            throw new ArgumentException("The setting does not exist.");
        }

        /// <summary>
        ///     Sets a new value of a setting.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="value">The Value.</param>
        public void SetSeting(string name, object value)
        {
            if (Settings.ContainsKey(name))
            {
                Settings[name] = value;
                return;
            }

            throw new ArgumentException("The setting does not exist.");
        }

        /// <summary>
        ///     Loads the GameSettings from a specific path.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>GameSettings</returns>
        public static GameSettings LoadFrom(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (GameSettings) new BinaryFormatter().Deserialize(fileStream);
            }
        }

        /// <summary>
        ///     Saves the setting to a specific path.
        /// </summary>
        /// <param name="path">The Path.</param>
        public void Save(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(fileStream, this);
            }
        }
    }
}