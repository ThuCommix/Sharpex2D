using System;
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Media.Sound.Processors
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public class CircleSoundProcessor : ISoundProcessor
    {
        #region ISoundProcessor Implementation

        /// <summary>
        ///     Updates the SoundProcessor.
        /// </summary>
        /// <param name="listenerPosition">The ListenerPosition.</param>
        /// <param name="soundOriginPosition">The SoundOriginPosition.</param>
        public void Update(Vector2 listenerPosition, Vector2 soundOriginPosition)
        {
            if (Radius <= 0) throw new InvalidOperationException("The radius can not be lower or equal to zero.");
            float originDistance = (soundOriginPosition - listenerPosition).Length;
            if (originDistance > Radius)
            {
                //listener is out of range.
                SoundManager.Volume = 0;
            }
            else
            {
                float volume = originDistance/Radius; //8 / 10 = 0.8
                SoundManager.Volume = 1f - volume; //1 - 0.8 = 0.2 volume
                if (listenerPosition.X > soundOriginPosition.X)
                {
                    //balance left
                    SoundManager.Balance = 0.25f;
                }
                else if (listenerPosition.X < soundOriginPosition.X)
                {
                    //balance rigt
                    SoundManager.Balance = 0.75f;
                }
                else
                {
                    //balance mid
                    SoundManager.Balance = 0.5f;
                }
            }
        }

        /// <summary>
        ///     Gets the SoundManager.
        /// </summary>
        public SoundManager SoundManager { get; private set; }

        #endregion

        /// <summary>
        ///     Initializes a new CircleSoundProcessor class.
        /// </summary>
        public CircleSoundProcessor()
        {
            SoundManager = (SoundManager) SGL.Components.Get<SoundManager>().Clone();
        }

        /// <summary>
        ///     Sets or gets the Radius.
        /// </summary>
        public float Radius { set; get; }
    }
}