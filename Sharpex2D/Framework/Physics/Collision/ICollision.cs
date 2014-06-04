namespace Sharpex2D.Framework.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface ICollision
    {
        bool IsIntersecting(Particle particle1, Particle particle2);
    }
}