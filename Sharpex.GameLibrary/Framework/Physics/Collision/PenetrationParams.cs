using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Physics.Collision
{
    public class PenetrationParams
    {
        /// <summary>
        /// Gets the Involved Particles.
        /// </summary>
        public Particle[] InvolvedParticles {set; get; }
        /// <summary>
        /// Gets the InnerEnergy.
        /// </summary>
        public float InnerEnergy { set; get; }
    }
}
