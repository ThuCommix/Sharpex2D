using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Rendering.Scene.Events
{
    public class AfterSceneChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new AfterSceneChangedEvent class.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        internal AfterSceneChangedEvent(IScene scene)
        {
            CurrentScene = scene;
        }
        /// <summary>
        /// Gets the CurrentScene.
        /// </summary>
        public IScene CurrentScene { private set; get; }
    }
}
