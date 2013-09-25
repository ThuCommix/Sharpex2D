using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Game.Timing
{
    internal interface IActiveWaiting
    {
        /// <summary>
        /// Waits the thread for the specified miliseconds.
        /// </summary>
        /// <param name="miliseconds">The Miliseconds.</param>
        void Busy(TimeSpan miliseconds);
    }
}
