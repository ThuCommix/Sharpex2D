// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AudioEffectGroup
    {
        private readonly List<AudioEffect> _audioEffects;
        private float _masterVolume;

        /// <summary>
        /// Initializes a new AudioEffectGroup class.
        /// </summary>
        public AudioEffectGroup() : this("", 1)
        {
        }

        /// <summary>
        /// Initializes a new AudioEffectGroup class.
        /// </summary>
        /// <param name="name">The Name.</param>
        public AudioEffectGroup(string name) : this(name, 1)
        {
        }

        /// <summary>
        /// Initializes a new AudioEffectGroup class.
        /// </summary>
        /// <param name="masterVolume">The MasterVolume.</param>
        public AudioEffectGroup(float masterVolume) : this("", masterVolume)
        {
        }

        /// <summary>
        /// Initializes a new AudioEffectGroup class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="masterVolume">The MasterVolume.</param>
        public AudioEffectGroup(string name, float masterVolume)
        {
            _audioEffects = new List<AudioEffect>();
            Name = name;
            MasterVolume = masterVolume;
            SGL.QueryComponents<AudioManager>().AddEffectGroup(this);
        }

        /// <summary>
        /// Gets or sets the name of the AudioEffectGroup.
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
            get { return _audioEffects.Count; }
        }

        /// <summary>
        /// Gets an array containing the audio effects of this instance.
        /// </summary>
        public AudioEffect[] AudioEffects
        {
            get { return _audioEffects.ToArray(); }
        }

        /// <summary>
        /// Adds a new AudioEffect to the AudioEffectGroup.
        /// </summary>
        /// <param name="audioEffect">The AudioEffect.</param>
        public void Add(AudioEffect audioEffect)
        {
            _audioEffects.Add(audioEffect);
            audioEffect.Volume = MasterVolume;
        }

        /// <summary>
        /// Adds an array of AudioEffect to the AudioEffectGroup.
        /// </summary>
        /// <param name="audioEffects">The AudioEffect Array.</param>
        public void Add(AudioEffect[] audioEffects)
        {
            _audioEffects.AddRange(audioEffects);
            foreach (var audioEffect in audioEffects)
            {
                audioEffect.Volume = MasterVolume;
            }
        }

        /// <summary>
        /// Removes an AudioEffect from the AudioEffectGroup.
        /// </summary>
        /// <param name="audioEffect">The AudioEffect.</param>
        public void Remove(AudioEffect audioEffect)
        {
            if (_audioEffects.Contains(audioEffect))
            {
                _audioEffects.Remove(audioEffect);
            }
        }

        /// <summary>
        /// Removes all audio effects of this instance.
        /// </summary>
        public void RemoveAll()
        {
            _audioEffects.Clear();
        }

        /// <summary>
        /// Applys the new master volume to each audio effect.
        /// </summary>
        private void ApplyVolumeChange()
        {
            foreach (var audioEffect in _audioEffects)
            {
                audioEffect.Volume = MasterVolume;
            }
        }
    }
}