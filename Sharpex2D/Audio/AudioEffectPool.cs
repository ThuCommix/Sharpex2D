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

using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Common;

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class AudioEffectPool : Singleton<AudioEffectPool>
    {
        /// <summary>
        /// Represents the amount of the maximum simultaneously sounds available.
        /// </summary>
        public const int MaxSimultaneouslySounds = 32;

        private readonly List<AudioEffect> _audioEffectPool;

        /// <summary>
        /// Initializes a new AudioEffectPool class.
        /// </summary>
        public AudioEffectPool()
        {
            _audioEffectPool = new List<AudioEffect>();
            for (int i = 0; i < MaxSimultaneouslySounds; i++)
            {
                _audioEffectPool.Add(new AudioEffect());
            }
        }

        /// <summary>
        /// Gets the amount of requestable audio effects.
        /// </summary>
        public int RequestableAudioEffects
        {
            get { return _audioEffectPool.Count(x => x.PlaybackState == PlaybackState.Stopped); }
        }

        /// <summary>
        /// Requests an idle audio effect.
        /// </summary>
        /// <returns>AudioEffect.</returns>
        public AudioEffect RequestAudioEffect()
        {
            foreach (var audioEffect in _audioEffectPool)
            {
                if (audioEffect.PlaybackState == PlaybackState.Stopped)
                    return audioEffect;
            }
            throw new AudioException("Unable to request an audio effect.");
        }
    }
}