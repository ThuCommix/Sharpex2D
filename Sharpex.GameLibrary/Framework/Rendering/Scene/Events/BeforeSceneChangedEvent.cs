using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Rendering.Scene.Events
{
    public class BeforeSceneChangedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new BeforeSceneChangedEvent class.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        internal BeforeSceneChangedEvent(IScene scene)
        {
            CurrentScene = scene;
        }
        /// <summary>
        /// Gets the CurrentScene.
        /// </summary>
        public IScene CurrentScene { private set; get; }
    }
}
