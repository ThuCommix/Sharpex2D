namespace Sharpex2D.Framework.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Ellipse : IGeometry
    {
        private readonly Polygon _polygon;
        private Vector2 _position;

        /// <summary>
        ///     Initializes a new Ellipse class.
        /// </summary>
        /// <param name="radiusX">The X-Radius.</param>
        /// <param name="radiusY">The Y-Radius.</param>
        public Ellipse(float radiusX, float radiusY)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            _position = new Vector2(0, 0);
            _polygon = new Polygon();
            UpdateEllipse();
        }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        public Vector2 Position
        {
            set
            {
                _position = value;
                UpdateEllipse();
            }
            get { return _position; }
        }

        /// <summary>
        ///     Gets the X-Radius.
        /// </summary>
        public float RadiusX { private set; get; }

        /// <summary>
        ///     Gets the Y-Radius.
        /// </summary>
        public float RadiusY { private set; get; }

        /// <summary>
        ///     Gets the points.
        /// </summary>
        public Vector2[] Points
        {
            get { return _polygon.Points; }
        }

        /// <summary>
        ///     Updates the Ellipse if something changed.
        /// </summary>
        private void UpdateEllipse()
        {
            _polygon.Reset();

            for (int i = 1; i <= 360; i++)
            {
                _polygon.Add(
                    new Vector2(RadiusX*MathHelper.Cos(i*(float) MathHelper.PiOverOneEighty) + _position.X,
                        RadiusY*MathHelper.Sin(i*(float) MathHelper.PiOverOneEighty) + _position.Y));
            }
        }
    }
}