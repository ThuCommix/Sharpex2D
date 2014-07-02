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

using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Game.Timing.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class TargetFrameTimeChangedEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        public TargetFrameTimeChangedEvent()
        {
        }

        /// <summary>
        ///     Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        /// <param name="fps">The FramesPerSecond.</param>
        /// <param name="targetFrameTime">The TargetFrameTime.</param>
        public TargetFrameTimeChangedEvent(float fps, float targetFrameTime)
        {
            FramesPerSecond = fps;
            TargetFrameTime = targetFrameTime;
        }

        /// <summary>
        ///     Gets the changed fps amount.
        /// </summary>
        public float FramesPerSecond { private set; get; }

        /// <summary>
        ///     Gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime { private set; get; }
    }
}