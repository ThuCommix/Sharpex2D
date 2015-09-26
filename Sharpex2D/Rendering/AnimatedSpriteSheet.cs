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

namespace Sharpex2D.Framework.Rendering
{
    public class AnimatedSpriteSheet : SpriteSheet, IUpdateable
    {
        private readonly List<Keyframe> _keyframes;
        private int _ckeyframe;
        private float _durationPassed;

        /// <summary>
        /// Initializes a new AnimatedSpriteSheet class.
        /// </summary>
        /// <param name="texture2D">The Texture2D.</param>
        public AnimatedSpriteSheet(Texture2D texture2D) : base(texture2D)
        {
            _keyframes = new List<Keyframe>();
        }

        /// <summary>
        /// Gets the current Keyframe.
        /// </summary>
        public Keyframe CurrentKeyframe { private set; get; }

        /// <summary>
        /// Gets the amount of keyframes.
        /// </summary>
        public int KeyframeCount => _keyframes.Count;

        /// <summary>
        /// Gets the current keyframe index
        /// </summary>
        public int CurrentKeyframeIndex => _ckeyframe;

        /// <summary>
        /// A value indicating whether the animation should be looped
        /// </summary>
        public bool Loop { set; get; }

        /// <summary>
        /// A value indicating whether a not looped <see cref="Loop"/> animation is finished
        /// </summary>
        public bool IsFinished { private set; get; }

        /// <summary>
        /// A value indicating whether the AnimatedSpriteSheet should update automatically.
        /// </summary>
        public bool AutoUpdate { set; get; }

        #region IUpdate Implementation

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (AutoUpdate)
            {
                _durationPassed += gameTime.ElapsedGameTime;

                if (_ckeyframe <= _keyframes.Count - 1)
                {
                    IsFinished = false;
                    if (_durationPassed >= _keyframes[_ckeyframe].Duration)
                    {
                        _ckeyframe++;
                        _durationPassed = 0;
                    }
                }
                else
                {
                    if (Loop)
                    {
                        _ckeyframe = 0;
                    }
                    else
                    {
                        IsFinished = true;
                    }
                }

                ActivateKeyframe(_ckeyframe);
            }
        }

        #endregion

        /// <summary>
        /// Adds a new Keyframe.
        /// </summary>
        /// <param name="keyframe">The Keyframe.</param>
        public void Add(Keyframe keyframe)
        {
            _keyframes.Add(keyframe);
        }

        /// <summary>
        /// Removes a Keyframe.
        /// </summary>
        /// <param name="keyframe">The Keyframe.</param>
        public void Remove(Keyframe keyframe)
        {
            if (_keyframes.Contains(keyframe))
            {
                _keyframes.Remove(keyframe);
            }
        }

        /// <summary>
        /// Removes a Keyframe at a specific index.
        /// </summary>
        /// <param name="index">The Index.</param>
        public void RemoveAt(int index)
        {
            if (index < _keyframes.Count - 1)
            {
                _keyframes.RemoveAt(index);
            }
        }

        /// <summary>
        /// Activates a Keyframe.
        /// </summary>
        /// <param name="index">The Index.</param>
        public void ActivateKeyframe(int index)
        {
            if (index <= _keyframes.Count - 1)
            {
                CurrentKeyframe = _keyframes[index];
                Rectangle = CurrentKeyframe.DisplayRectangle;
            }
        }
    }
}
