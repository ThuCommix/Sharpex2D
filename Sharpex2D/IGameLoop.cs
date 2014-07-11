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

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IGameLoop : IComponent
    {
        /// <summary>
        ///     Gets or sets the TargetFrameTime.
        /// </summary>
        float TargetFrameTime { get; set; }

        /// <summary>
        ///     Gets or sets the TargetUpdateTime.
        /// </summary>
        float TargetUpdateTime { get; set; }

        /// <summary>
        ///     Indicates whether the GameLoop is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        ///     Gets the Target FPS.
        /// </summary>
        float TargetFramesPerSecond { get; }

        /// <summary>
        ///     A value indicating whether the game loop should idle.
        /// </summary>
        bool Idle { set; get; }

        /// <summary>
        ///     Gets or sets the IdleDuration.
        /// </summary>
        float IdleDuration { set; get; }

        /// <summary>
        ///     Starts the GameLoop.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops the GameLoop.
        /// </summary>
        void Stop();

        /// <summary>
        ///     Subscribes a IDrawable to the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        void Subscribe(IDrawable drawable);

        /// <summary>
        ///     Unsubscribes a IDrawable from the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        void Unsubscribe(IDrawable drawable);

        /// <summary>
        ///     Subscribes a IUpdateable to the game loop.
        /// </summary>
        /// <param name="updateable">The IDrawable.</param>
        void Subscribe(IUpdateable updateable);

        /// <summary>
        ///     Unsubscribes a IUpdateable from the game loop.
        /// </summary>
        /// <param name="updateable">The IUpdateable.</param>
        void Unsubscribe(IUpdateable updateable);
    }
}