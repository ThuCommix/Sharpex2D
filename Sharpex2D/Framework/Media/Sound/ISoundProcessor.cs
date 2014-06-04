using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface ISoundProcessor
    {
        /// <summary>
        ///     Gets the SoundManager.
        /// </summary>
        SoundManager SoundManager { get; }

        /// <summary>
        ///     Updates the SoundProcessor.
        /// </summary>
        /// <param name="listenerPosition">The ListenerPosition.</param>
        /// <param name="soundOriginPosition">The SoundOriginPosition.</param>
        void Update(Vector2 listenerPosition, Vector2 soundOriginPosition);
    }
}