namespace Sharpex2D.Framework.Media.Video
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IVideoProvider
    {
        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        float Volume { set; get; }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        long Position { set; get; }

        /// <summary>
        ///     A value indicating whether the VideoProvider is playing.
        /// </summary>
        bool IsPlaying { set; get; }

        /// <summary>
        ///     Gets the video length.
        /// </summary>
        long Length { get; }

        /// <summary>
        ///     Plays the video.
        /// </summary>
        /// <param name="video">The VideoFile.</param>
        void Play(Video video);

        /// <summary>
        ///     Resumes a video.
        /// </summary>
        void Resume();

        /// <summary>
        ///     Pause a video.
        /// </summary>
        void Pause();

        /// <summary>
        ///     Seeks a video to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        void Seek(long position);
    }
}