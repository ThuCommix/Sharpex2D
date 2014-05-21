using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Rendering.Scene.Events
{
    public class BeforeSceneChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new BeforeSceneChangedEvent class.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        internal BeforeSceneChangedEvent(Scene scene)
        {
            CurrentScene = scene;
        }
        /// <summary>
        /// Gets the CurrentScene.
        /// </summary>
        public Scene CurrentScene { private set; get; }
    }
}
