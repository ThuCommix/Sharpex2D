// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class PolygonContainer
    {
        private readonly Dictionary<int, Polygon> _innerPolygons;
        private Vector2 _offset;

        /// <summary>
        ///     Initializes a new PolygonContainer class.
        /// </summary>
        public PolygonContainer()
        {
            _innerPolygons = new Dictionary<int, Polygon>();
        }

        /// <summary>
        ///     Gets or sets the Offset.
        /// </summary>
        public Vector2 Offset
        {
            set
            {
                _offset = value;
                foreach (var polygon in _innerPolygons)
                {
                    polygon.Value.Offset = value;
                }
            }
            get { return _offset; }
        }

        /// <summary>
        ///     Gets the Polygons.
        /// </summary>
        public Polygon[] Polygons
        {
            get { return _innerPolygons.Values.ToArray(); }
        }

        /// <summary>
        ///     Adds a new Polygon.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="polygon">The Polygon.</param>
        public void Add(int index, Polygon polygon)
        {
            _innerPolygons.Add(index, polygon);
        }

        /// <summary>
        ///     Removes a Polygon.
        /// </summary>
        /// <param name="index">The Index.</param>
        public void Remove(int index)
        {
            if (_innerPolygons.ContainsKey(index))
            {
                _innerPolygons.Remove(index);
            }
        }

        /// <summary>
        ///     A value indicating whether the Polygon intersects with the PolygonContainer.
        /// </summary>
        /// <param name="polygon">The Polygon.</param>
        /// <returns>True if intersecting.</returns>
        public bool Intersects(Polygon polygon)
        {
            return _innerPolygons.Any(selectedPolygon => selectedPolygon.Value.Intersects(polygon));
        }

        /// <summary>
        ///     A value indicating whether the PolygonContainer intersects with this PolygonContainer.
        /// </summary>
        /// <param name="polygonContainer">The PolygonContainer.</param>
        /// <returns>True if intersecting.</returns>
        public bool Intersects(PolygonContainer polygonContainer)
        {
            bool flag = false;

            foreach (
                var polygon in
                    _innerPolygons.Where(
                        polygon =>
                            polygonContainer.Polygons.Any(outsidePolygon => polygon.Value.Intersects(outsidePolygon))))
            {
                flag = true;
            }

            return flag;
        }

        /// <summary>
        ///     A value indicating whether the Polygon intersects with the PolygonContainer.
        /// </summary>
        /// <param name="polygon">The Polygon.</param>
        /// <param name="indices">The Indices which are intersects.</param>
        /// <returns>True if intersecting.</returns>
        public bool IntersectsWith(Polygon polygon, out int[] indices)
        {
            bool flag = false;
            var indicesList = new List<int>();

            foreach (
                var selectedPolygon in
                    _innerPolygons.Where(selectedPolygon => selectedPolygon.Value.Intersects(polygon)))
            {
                indicesList.Add(selectedPolygon.Key);
                flag = true;
            }

            indices = indicesList.ToArray();
            return flag;
        }

        /// <summary>
        ///     A value indicating whether the PolygonContainer intersects with this PolygonContainer.
        /// </summary>
        /// <param name="polygonContainer">The PolygonContainer.</param>
        /// <param name="indices">The Indices which are intersects.</param>
        /// <returns>True if intersecting.</returns>
        public bool IntersectsWith(PolygonContainer polygonContainer, out int[] indices)
        {
            bool flag = false;
            var indicesList = new List<int>();

            foreach (var polygon in from polygon in _innerPolygons
                from outsidePolygon in
                    polygonContainer.Polygons.Where(outsidePolygon => polygon.Value.Intersects(outsidePolygon))
                select polygon)
            {
                flag = true;
                indicesList.Add(polygon.Key);
            }

            indices = indicesList.ToArray();
            return flag;
        }
    }
}