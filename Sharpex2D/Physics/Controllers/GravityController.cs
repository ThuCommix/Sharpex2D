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

using Sharpex2D.Math;

namespace Sharpex2D.Physics.Controllers
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class GravityController : Controller
    {
        private readonly Vector2 _gravity;

        /// <summary>
        /// Initializes a new GravityController class.
        /// </summary>
        public GravityController() : this(9.81f)
        {
        }

        /// <summary>
        /// Initializes a new GravityController class.
        /// </summary>
        /// <param name="gravity">The Gravity.</param>
        public GravityController(float gravity)
        {
            _gravity = new Vector2(0, gravity);
            Name = string.Format("Gravity Controller (Gravity={0})", _gravity.Y);
        }

        /// <summary>
        /// Updates the controller.
        /// </summary>
        /// <param name="world">The World.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public override void Update(World world, float elapsed)
        {
            float dseconds = elapsed/1000;
            Vector2 currentGravity = _gravity*dseconds;

            for (int i = 0; i <= world.Bodies.Count - 1; i++)
            {
                world.Bodies[i].ApplyForce(currentGravity, world.Bodies[i].Center);
            }
        }
    }
}