
namespace SharpexGL.Framework.Media.Sound
{
    public interface ISoundInitializer
    {
        /// <summary>
        /// Creates the ISoundProvider.
        /// </summary>
        /// <returns>ISoundProvider</returns>
        ISoundProvider CreateProvider();
    }
}
