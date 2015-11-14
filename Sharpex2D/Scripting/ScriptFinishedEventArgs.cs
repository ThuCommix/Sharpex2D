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

namespace Sharpex2D.Framework.Scripting
{
    public class ScriptFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new ScriptFinishedEventArgs.
        /// </summary>
        /// <param name="result">The Result.</param>
        /// <param name="method">The method</param>
        internal ScriptFinishedEventArgs(object result, MethodAttribute method)
        {
            Result = result;
            ExecutedMethod = method;
        }

        /// <summary>
        /// Gets the Result.
        /// </summary>
        public object Result { get; private set; }

        /// <summary>
        /// Gets the executed method
        /// </summary>
        public MethodAttribute ExecutedMethod { private set; get; }
    }
}
