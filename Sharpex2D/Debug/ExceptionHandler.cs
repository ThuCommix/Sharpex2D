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
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class ExceptionHandler : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        /// Gets the guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("4FE0400B-0520-4B43-ADAD-B588B13C38D8"); }
        }

        #endregion

        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if ((value && _enabled) || (!value && !_enabled))
                {
                }
                else
                {
                    if (value)
                    {
                        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    }
                    else
                    {
                        AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                    }

                    _enabled = value;
                }
            }
        }

        /// <summary>
        /// Logs the unhandled exceptions.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.GetClassLogger().Critical(((Exception) e.ExceptionObject).Message);
        }
    }
}