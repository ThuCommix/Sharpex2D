using System;

namespace Sharpex2D.Framework.Rendering.Effects
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IEffect
    {
        /// <summary>
        ///     Gets the Guid-Identifer.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        ///     Sets or gets the Duration.
        /// </summary>
        float Duration { set; get; }

        /// <summary>
        ///     A value indicating whether the Effect is completed.
        /// </summary>
        bool Completed { get; }

        /// <summary>
        ///     Gets the Callback action.
        /// </summary>
        Action Callback { get; }

        /// <summary>
        ///     Starts the Effect.
        /// </summary>
        void Start();
    }
}