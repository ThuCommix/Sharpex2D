using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Game.Timing.Events
{
    public class TargetFrameTimeChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        public TargetFrameTimeChangedEvent()
        {
            
        }
        /// <summary>
        /// Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        /// <param name="fps">The FramesPerSecond.</param>
        /// <param name="targetFrameTime">The TargetFrameTime.</param>
        public TargetFrameTimeChangedEvent(float fps, float targetFrameTime)
        {
            FramesPerSecond = fps;
            TargetFrameTime = targetFrameTime;
        }
        /// <summary>
        /// Gets the changed fps amount.
        /// </summary>
        public float FramesPerSecond { private set; get; }
        /// <summary>
        /// Gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime { private set; get; }
    }
}
