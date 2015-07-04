// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System;
using Sharpex2D.Framework.Input.Implementation.Touch;

namespace Sharpex2D.Framework.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class Touch
    {
        /// <summary>
        /// Initializes a new Touch class.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="contact">The ContactSize.</param>
        /// <param name="location">The Location.</param>
        /// <param name="dateTime">The DateTime.</param>
        /// <param name="touchMode">The TouchMode.</param>
        public Touch(int id, Vector2 contact, Vector2 location, DateTime dateTime, TouchMode touchMode)
        {
            Id = id;
            Contact = contact;
            Location = location;
            Time = dateTime;
            TouchMode = touchMode;
            ContactRectangle = new Rectangle(location, contact);
        }

        /// <summary>
        /// Gets the Contact size.
        /// </summary>
        public Vector2 Contact { private set; get; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        public Vector2 Location { private set; get; }

        /// <summary>
        /// Gets the ContactRectangle.
        /// </summary>
        public Rectangle ContactRectangle { private set; get; }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        public int Id { private set; get; }

        /// <summary>
        /// Gets the Time.
        /// </summary>
        public DateTime Time { private set; get; }

        /// <summary>
        /// Gets the TouchMode.
        /// </summary>
        public TouchMode TouchMode { private set; get; }
    }
}