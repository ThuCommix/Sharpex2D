using System;

namespace Sharpex2D.Framework.Physics
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class PhysicalConstants
    {
        /// <summary>
        ///     Gets the gravityconstant.
        /// </summary>
        public static float Gravitation
        {
            get { return 9.81f; }
        }

        /// <summary>
        ///     Gets the velocity of light in m/s.
        /// </summary>
        public static Int32 VelocityOfLight
        {
            get { return 299792458; }
        }
    }
}