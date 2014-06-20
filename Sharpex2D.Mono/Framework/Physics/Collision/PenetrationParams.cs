namespace Sharpex2D.Framework.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PenetrationParams
    {
        /// <summary>
        ///     Gets the Involved Particles.
        /// </summary>
        public Particle[] InvolvedParticles { set; get; }

        /// <summary>
        ///     Gets the InnerEnergy.
        /// </summary>
        public float InnerEnergy { set; get; }
    }
}