namespace SharpexGL.Framework.Media.Sound
{
    public class SoundEffect
    {

        private readonly SoundManager _soundProvider;

        /// <summary>
        /// Initializes a new SoundEffect.
        /// </summary>
        public SoundEffect()
        {
            _soundProvider = (SoundManager)SGL.Components.Get<SoundManager>().Clone();
        }
        /// <summary>
        /// Plays a Sound.
        /// </summary>
        /// <param name="sound">The Sound.</param>
        public void Play(Sound sound)
        {
            _soundProvider.Play(sound, PlayMode.None);
        }
        /// <summary>
        /// Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            set { _soundProvider.Balance = value; }
            get { return _soundProvider.Balance; }
        }
        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            set { _soundProvider.Volume = value; }
            get { return _soundProvider.Volume; }
        }
    }
}
