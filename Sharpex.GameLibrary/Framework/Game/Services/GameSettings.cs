using System;
using System.Collections.Generic;
using System.IO;
using SharpexGL.Framework.Content.Serialization;

namespace SharpexGL.Framework.Game.Services
{
    [Serializable]
    public class GameSettings
    {
        /// <summary>
        /// Initializes a new GameSettings class.
        /// </summary>
        public GameSettings()
        {
            Settings = new Dictionary<string, object>();
        }

        internal readonly Dictionary<string, object> Settings;

        /// <summary>
        /// Adds a new Setting to the GameSettings class.
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
        /// Returns a SettingValue.
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
        /// Sets a new value of a setting.
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
        /// Loads the GameSettings from a specific path.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>GameSettings</returns>
        public static GameSettings LoadFrom(string path)
        {
            var deserializer = SGL.Implementations.Get<GameSettingsSerializer>();
            return deserializer.Read(new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)));
        }

        /// <summary>
        /// Saves the setting to a specific path.
        /// </summary>
        /// <param name="path">The Path.</param>
        public void Save(string path)
        {
            var deserializer = SGL.Implementations.Get<GameSettingsSerializer>();
            deserializer.Write(new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite)), this);
        }
    }
}
