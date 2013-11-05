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
        /// <summary>
        /// A value indicating whether the Effect is completed.
        /// </summary>
        bool Completed { get; }
        /// <summary>
        /// Gets the Callback action.
        /// </summary>
        Action Callback { get; }
    }
}
