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

using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.Framework.Audio
{
    public class SoundEffectPool : Singleton<SoundEffectPool>
    {
        /// <summary>
        /// Represents the amount of the maximum simultaneously sounds available.
        /// </summary>
        public const int MaxSimultaneouslySounds = 32;

        private readonly List<SoundEffect> _soundEffectPool;

        /// <summary>
        /// Initializes a new SoundEffectPool class.
        /// </summary>
        public SoundEffectPool()
        {
            _soundEffectPool = new List<SoundEffect>();
            for (int i = 0; i < MaxSimultaneouslySounds; i++)
            {
                _soundEffectPool.Add(new SoundEffect());
            }
        }

        /// <summary>
        /// Gets the amount of requestable sound effects.
        /// </summary>
        public int RequestableAudioEffects
        {
            get { return _soundEffectPool.Count(x => x.PlaybackState == PlaybackState.Stopped); }
        }

        /// <summary>
        /// A value indicating whether that atleast one free sound effect is available.
        /// </summary>
        public bool CanRequest
        {
            get { return _soundEffectPool.Any(audioEffect => audioEffect.PlaybackState == PlaybackState.Stopped); }
        }

        /// <summary>
        /// Requests an idle sound effect.
        /// </summary>
        /// <returns>SoundEffect.</returns>
        public SoundEffect RequestSoundEffect()
        {
            foreach (SoundEffect soundEffect in _soundEffectPool)
            {
                if (soundEffect.PlaybackState == PlaybackState.Stopped)
                    return soundEffect;
            }
            throw new SoundException("Unable to request an audio effect.");
        }
    }
}
