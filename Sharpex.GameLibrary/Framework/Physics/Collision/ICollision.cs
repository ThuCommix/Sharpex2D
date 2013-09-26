using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpexGL.Framework.Physics.Collision
{
    public interface ICollision
    {
        bool IsIntersecting(Particle particle1, Particle particle2);
    }
}
