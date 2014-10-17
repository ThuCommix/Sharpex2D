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

namespace Sharpex2D.GameService
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public abstract class GameTrigger
    {
        /// <summary>
        /// GameTriggerEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void GameTriggerEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Initializes a new GameTrigger class.
        /// </summary>
        protected GameTrigger() : this("Undefined GameTrigger")
        {
        }

        /// <summary>
        /// The GameTrigger.
        /// </summary>
        /// <param name="name">The Name.</param>
        protected GameTrigger(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Triggered event.
        /// </summary>
        public event GameTriggerEventHandler Triggered;

        /// <summary>
        /// Triggers the GameTrigger.
        /// </summary>
        public virtual void Trigger()
        {
            if (Triggered != null)
            {
                Triggered(this, EventArgs.Empty);
            }
        }
    }
}