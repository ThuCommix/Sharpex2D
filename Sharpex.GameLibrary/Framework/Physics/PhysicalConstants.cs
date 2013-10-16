using System;

namespace SharpexGL.Framework.Physics
{
    public static class PhysicalConstants
    {
        /// <summary>
        /// Gets the gravityconstant.
        /// </summary>
        public static float Gravitation
        {
            get { return 9.81f; }
        }
        /// <summary>
        /// Gets the velocity of light in m/s.
        /// </summary>
        public static Int32 VelocityOfLight{
            get { return 299792458; }
        }
    }
}
