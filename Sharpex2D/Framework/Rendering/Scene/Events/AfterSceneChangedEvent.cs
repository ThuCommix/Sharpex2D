using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Rendering.Scene.Events
{
    public class AfterSceneChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new AfterSceneChangedEvent class.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        internal AfterSceneChangedEvent(Scene scene)
        {
            CurrentScene = scene;
        }
        /// <summary>
        /// Gets the CurrentScene.
        /// </summary>
        public Scene CurrentScene { private set; get; }
    }
}
