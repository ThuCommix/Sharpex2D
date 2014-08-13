using Sharpex2D.Math;

namespace XPlane.Core
{
    public interface IDynamicHitbox
    {
        /// <summary>
        /// Gets the Bounds.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// A value indicating whether the hitbox intersects with another.
        /// </summary>
        /// <param name="dynamicHitbox">The other DynamicHitbox.</param>
        /// <returns>True if intersecting.</returns>
        bool IntersectsWith(IDynamicHitbox dynamicHitbox);
    }
}