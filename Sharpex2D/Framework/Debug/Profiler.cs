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

using System;
using System.Diagnostics;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Profiler
    {
        private static readonly Logger Logger;

        /// <summary>
        ///     Initializes a  new Profiler class.
        /// </summary>
        static Profiler()
        {
            Logger = LogManager.GetClassLogger();
        }

        /// <summary>
        ///     Profiles an action.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="action">The Action.</param>
        public static void Profile(Guid guid, Action action)
        {
            var sw = new Stopwatch();
            Logger.Info("Profiling: {0}.", guid);
            sw.Start();
            action.Invoke();
            sw.Stop();
            Logger.Info("End profiling: {0}, Time: {1}ms", guid, sw.ElapsedMilliseconds);
            sw.Reset();
        }
    }
}