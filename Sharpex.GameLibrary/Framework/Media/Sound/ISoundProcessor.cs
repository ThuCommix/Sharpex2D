using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Media.Sound
{
    public interface ISoundProcessor
    {
        /// <summary>
        /// Updates the SoundProcessor.
        /// </summary>
        /// <param name="listenerPosition">The ListenerPosition.</param>
        /// <param name="soundOriginPosition">The SoundOriginPosition.</param>
        void Update(Vector2 listenerPosition, Vector2 soundOriginPosition);
        /// <summary>
        /// Gets the SoundManager.
        /// </summary>
        SoundManager SoundManager { get; }
    }
}
