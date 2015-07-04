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

namespace Sharpex2D.Framework.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class SoundEffectGroup
    {
        private readonly List<SoundEffect> _soundEffects;
        private float _masterVolume;

        /// <summary>
        /// Initializes a new SoundEffectGroup class.
        /// </summary>
        public SoundEffectGroup() : this("", 1)
        {
        }

        /// <summary>
        /// Initializes a new SoundEffectGroup class.
        /// </summary>
        /// <param name="name">The Title.</param>
        public SoundEffectGroup(string name) : this(name, 1)
        {
        }

        /// <summary>
        /// Initializes a new SoundEffectGroup class.
        /// </summary>
        /// <param name="masterVolume">The MasterVolume.</param>
        public SoundEffectGroup(float masterVolume) : this("", masterVolume)
        {
        }

        /// <summary>
        /// Initializes a new SoundEffectGroup class.
        /// </summary>
        /// <param name="name">The Title.</param>
        /// <param name="masterVolume">The MasterVolume.</param>
        public SoundEffectGroup(string name, float masterVolume)
        {
            _soundEffects = new List<SoundEffect>();
            Name = name;
            MasterVolume = masterVolume;
            GameHost.Get<MediaPlayer>().AddEffectGroup(this);
        }

        /// <summary>
        /// Gets or sets the name of the SoundEffectGroup.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MasterVolume.
        /// </summary>
        public float MasterVolume
        {
            get { return _masterVolume; }
            set
            {
                if (value > 1f || value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _masterVolume = value;
                ApplyVolumeChange();
            }
        }

        /// <summary>
        /// Gets the amount of sound effects in this instance.
        /// </summary>
        public int Count
        {
            get { return _soundEffects.Count; }
        }

        /// <summary>
        /// Gets an array containing the sound effects of this instance.
        /// </summary>
        public SoundEffect[] SoundEffects
        {
            get { return _soundEffects.ToArray(); }
        }

        /// <summary>
        /// Adds a new SoundEffect to the SoundEffectGroup.
        /// </summary>
        /// <param name="soundEffect">The SoundEffect.</param>
        public void Add(SoundEffect soundEffect)
        {
            _soundEffects.Add(soundEffect);
            soundEffect.Volume = MasterVolume;
        }

        /// <summary>
        /// Adds a new SoundEffect to the SoundEffectGroup.
        /// </summary>
        /// <param name="soundEffects">The SoundEffect Array.</param>
        public void Add(SoundEffect[] soundEffects)
        {
            _soundEffects.AddRange(soundEffects);
            foreach (SoundEffect soundEffect in soundEffects)
            {
                soundEffect.Volume = MasterVolume;
            }
        }

        /// <summary>
        /// Removes an SoundEffect from the AudioEffectGroup.
        /// </summary>
        /// <param name="soundEffect">The SoundEffect.</param>
        public void Remove(SoundEffect soundEffect)
        {
            if (_soundEffects.Contains(soundEffect))
            {
                _soundEffects.Remove(soundEffect);
            }
        }

        /// <summary>
        /// Removes all sound effects of this instance.
        /// </summary>
        public void RemoveAll()
        {
            _soundEffects.Clear();
        }

        /// <summary>
        /// Applys the new master volume to each sound effect.
        /// </summary>
        private void ApplyVolumeChange()
        {
            foreach (SoundEffect audioEffect in _soundEffects)
            {
                audioEffect.Volume = MasterVolume;
            }
        }
    }
}