using System;

namespace SharpexGL.Framework.Components
{
    public interface IComponent
    {
        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        Guid Guid { get; }
    }
}
