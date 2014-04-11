
namespace Sharpex2D.Framework.Physics.Collision
{
    public interface ICollision
    {
        bool IsIntersecting(Particle particle1, Particle particle2);
    }
}
