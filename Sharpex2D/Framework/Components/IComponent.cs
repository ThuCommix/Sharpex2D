using System;

namespace Sharpex2D.Framework.Components
{
    public interface IComponent
    {
        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        Guid Guid { get; }
    }
}
