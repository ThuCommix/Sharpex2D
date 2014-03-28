
namespace SharpexGL.Framework.Media.Video
{
    public interface IVideoInitializer
    {
        /// <summary>
        /// Creates the IVideoProvider.
        /// </summary>
        /// <returns>IVideoProvider</returns>
        IVideoProvider CreateProvider();
    }
}
