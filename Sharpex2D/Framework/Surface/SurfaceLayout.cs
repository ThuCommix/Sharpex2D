
namespace Sharpex2D.Framework.Surface
{
    public class SurfaceLayout
    {
        /// <summary>
        /// Initializes a new SurfaceLayout class.
        /// </summary>
        public SurfaceLayout()
        {
            CanMinimize = true;
            CanClose = true;
            CanMaximize = false;
        }
        /// <summary>
        /// Initializes a new SurfaceLayout class.
        /// </summary>
        /// <param name="canMinimize">The State.</param>
        /// <param name="canMaximize">The State.</param>
        /// <param name="canClose">The State.</param>
        public SurfaceLayout(bool canMinimize, bool canMaximize, bool canClose)
        {
            CanMaximize = canMaximize;
            CanMinimize = canMinimize;
            CanClose = canClose;
        }

        /// <summary>
        /// A value indicating whether the surface is minimizable.
        /// </summary>
        public bool CanMinimize { set; get; }
        /// <summary>
        /// A value indicating whether the surface is maximizable.
        /// </summary>
        public bool CanMaximize { set; get; }
        /// <summary>
        /// A value indicating whether the surface is closable.
        /// </summary>
        public bool CanClose { set; get; }
    }
}
