using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiAnimation : IAnimation
    {
        private float _duration;

        #region IAnimation Implementation
        /// <summary>
        /// Gets the SpriteSheet on which the Animation is based.
        /// </summary>
        public ISpriteSheet SpriteSheet { get; private set; }

        /// <summary>
        /// Sets or gets the Duration.
        /// </summary>
        public float Duration
        {
            get { return _duration; }
            set { if(value > 0)_duration = value; }
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            _elapsed += elapsed;
            if (_elapsed >= _duration)
            {
                _index++;
                if (_index > Keyframes.Count - 1) _index = 0;
                Texture = GetKeyframe(Keyframes[_index]);
                _elapsed = 0;
            }
        }
                /// <summary>
        /// Sets or gets the Keyframes.
        /// </summary>
        public List<Keyframe> Keyframes { get; set; }
        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public ITexture Texture { get; private set; }

        #endregion

        private float _elapsed;
        private int _index;

        /// <summary>
        /// Initializes a new GdiAdnimation class.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="keyframe">The first Keyframe.</param>
        public GdiAnimation(ISpriteSheet spriteSheet, Keyframe keyframe)
        {
            if (spriteSheet.GetType() != typeof (GdiSpriteSheet))
                throw new ArgumentException("GdiAnimation expects a GdiSpriteSheet as resource");

            Keyframes = new List<Keyframe>();
            SpriteSheet = spriteSheet;
            Duration = 100;

            Keyframes.Add(keyframe);

            Texture = GetKeyframe(Keyframes[0]);
        }
        /// <summary>
        /// Initializes a new GdiAdnimation class.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="keyframes">The Keyframes.</param>
        public GdiAnimation(ISpriteSheet spriteSheet, params Keyframe[] keyframes)
        {
            if (spriteSheet.GetType() != typeof(GdiSpriteSheet))
                throw new ArgumentException("GdiAnimation expects a GdiSpriteSheet as resource");

            Keyframes = new List<Keyframe>();
            SpriteSheet = spriteSheet;
            Duration = 100;

            Keyframes.AddRange(keyframes);

            Texture = GetKeyframe(Keyframes[0]);
        }
        /// <summary>
        /// Gets the Keyframe.
        /// </summary>
        /// <param name="keyframe">The Keyframe.</param>
        /// <returns>Texture</returns>
        private ITexture GetKeyframe(Keyframe keyframe)
        {
            return SpriteSheet.GetSprite(keyframe.X, keyframe.Y, keyframe.Width, keyframe.Height);
        }
    }
}
