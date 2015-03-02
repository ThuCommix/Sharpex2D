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
using System.Windows.Forms;

namespace Sharpex2D.Input.Implementation
{
#if Windows

    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class MessageFilter : NativeWindow
    {
        /// <summary>
        /// MessageEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        private bool _filterAssigned;
        private int _messageFilter;

        /// <summary>
        /// Initializes a new WindowProcessor class.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        public MessageFilter(IntPtr handle)
        {
            AssignHandle(handle);
        }

        /// <summary>
        /// Gets or sets the MessageFilter.
        /// </summary>
        public int Filter
        {
            set
            {
                _messageFilter = value;
                _filterAssigned = true;
            }
            get { return _messageFilter; }
        }

        /// <summary>
        /// MessageArrived event.
        /// </summary>
        public event MessageEventHandler MessageArrived;

        /// <summary>
        /// WindProc.
        /// </summary>
        /// <param name="m">The Message.</param>
        protected override void WndProc(ref Message m)
        {
            if (_filterAssigned)
            {
                if (m.Msg == Filter)
                {
                    if (MessageArrived != null)
                    {
                        MessageArrived(this, new MessageEventArgs(m));
                    }
                }
            }
            base.WndProc(ref m);
        }
    }

#endif
}