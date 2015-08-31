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

namespace Sharpex2D.Framework
{
    public class VersionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new VersionAttribute class.
        /// </summary>
        /// <param name="major">The Major.</param>
        public VersionAttribute(int major) : this(major, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new VersionAttribute class.
        /// </summary>
        /// <param name="major">The Major.</param>
        /// <param name="minor">The Minor.</param>
        public VersionAttribute(int major, int minor) : this(major, minor, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new VersionAttribute class.
        /// </summary>
        /// <param name="major">The Major.</param>
        /// <param name="minor">The Minor.</param>
        /// <param name="patch">The Patch.</param>
        public VersionAttribute(int major, int minor, int patch) : this(major, minor, patch, 0)
        {
        }

        /// <summary>
        /// Initializes a new VersionAttribute class.
        /// </summary>
        /// <param name="major">The Major.</param>
        /// <param name="minor">The Minor.</param>
        /// <param name="patch">The Patch.</param>
        /// <param name="build">The Build.</param>
        public VersionAttribute(int major, int minor, int patch, int build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = build;
        }

        /// <summary>
        /// Gets the Major.
        /// </summary>
        public int Major { private set; get; }

        /// <summary>
        /// Gets the Minor.
        /// </summary>
        public int Minor { private set; get; }

        /// <summary>
        /// Gets the Patch.
        /// </summary>
        public int Patch { private set; get; }

        /// <summary>
        /// Gets the Build.
        /// </summary>
        public int Build { private set; get; }
    }
}
