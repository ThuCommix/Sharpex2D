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

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class XmlContentMetaData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { set; get; }

        /// <summary>
        /// Initializes a new XmlContentMetaData class.
        /// </summary>
        public XmlContentMetaData()
        {
            
        }

        /// <summary>
        /// Initializes a new XmlContentMetaData class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="value">The Value.</param>
        public XmlContentMetaData(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
