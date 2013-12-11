
using System.Collections.Generic;
using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Rendering
{
    public interface IAnimation : IContent
    {
        /// <summary>
        /// Gets the SpriteSheet on which the Animation is based.
        /// </summary>
        ISpriteSheet SpriteSheet { get; }
        /// <summary>
        /// Sets or gets the Duration.
        /// </summary>
        float Duration { set; get; }
        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        void Tick(float elapsed);
        /// <summary>
        /// Sets or gets the Keyframes.
        /// </summary>
        List<Keyframe> Keyframes { set; get; } 
        /// <summary>
        /// Gets the Texture.
        /// </summary>
        ITexture Texture { get; }
    }
}
