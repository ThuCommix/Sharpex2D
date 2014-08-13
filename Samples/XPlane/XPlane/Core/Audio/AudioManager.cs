using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpex2D;
using Sharpex2D.Audio;

namespace XPlane.Core.Audio
{
    public class AudioManager
    {
        private static AudioManager _instance;

        /// <summary>
        /// Gets the AudioManager Instance.
        /// </summary>
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AudioManager();

                return _instance;
            }
        }

        public readonly SoundManager Sound;
        public readonly SoundEffect LaserSound;
        public readonly SoundEffect ExplosionSound;

        /// <summary>
        /// Initializes a new AudioManager class.
        /// </summary>
        public AudioManager()
        {
            Sound = SGL.QueryComponents<SoundManager>();

            LaserSound = new SoundEffect(SGL.QueryResource<Sound>("laserFire.wav"));
            ExplosionSound = new SoundEffect(SGL.QueryResource<Sound>("explosion.wav"));
        }
    }
}
