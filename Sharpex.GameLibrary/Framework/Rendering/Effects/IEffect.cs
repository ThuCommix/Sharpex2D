using System;

namespace SharpexGL.Framework.Rendering.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Gets the Guid-Identifer.
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// Sets or gets the Duration.
        /// </summary>
        float Duration { set; get; }
        /// <summary>
        /// Starts the Effect.
        /// </summary>
        void Start();
    }
}
