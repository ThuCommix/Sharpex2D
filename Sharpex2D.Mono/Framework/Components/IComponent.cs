using System;

namespace Sharpex2D.Framework.Components
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IComponent
    {
        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        Guid Guid { get; }
    }
}