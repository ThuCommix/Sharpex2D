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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class SoundEffect
    {
        private readonly Sound _sound;
        private readonly SoundManager _soundProvider;
        private bool _muted;
        private float _vBeforeMute;

        /// <summary>
        ///     Initializes a new SoundEffect.
        /// </summary>
        /// <param name="sound">The Sound.</param>
        public SoundEffect(Sound sound)
        {
            _soundProvider = SGL.Components.Get<SoundManager>().CreateNew();
            _sound = sound;
        }

        /// <summary>
        ///     Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            set { _soundProvider.Balance = value; }
            get { return _soundProvider.Balance; }
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            set { _soundProvider.Volume = value; }
            get { return _soundProvider.Volume; }
        }

        /// <summary>
        ///     Sets or gets the Sound.
        /// </summary>
        public Sound Sound
        {
            get { return _sound; }
        }

        /// <summary>
        ///     A value indicating whether the sound is muted.
        /// </summary>
        public bool Muted
        {
            set
            {
                if ((value && Muted) | (!value && !Muted))
                {
                    return;
                }

                if (value)
                {
                    _vBeforeMute = Volume;
                    Volume = 0;
                }
                else
                {
                    Volume = _vBeforeMute;
                }

                _muted = value;
            }
            get { return _muted; }
        }

        /// <summary>
        ///     Plays the SoundEffect.
        /// </summary>
        public void Play()
        {
            if (_sound != null)
            {
                _soundProvider.Play(_sound, PlayMode.None);
            }
            else
            {
                throw new NullReferenceException("Sound can not be null.");
            }
        }
    }
}