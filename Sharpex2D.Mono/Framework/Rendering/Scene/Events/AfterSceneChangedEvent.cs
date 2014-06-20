using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Rendering.Scene.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class AfterSceneChangedEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new AfterSceneChangedEvent class.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        internal AfterSceneChangedEvent(Scene scene)
        {
            CurrentScene = scene;
        }

        /// <summary>
        ///     Gets the CurrentScene.
        /// </summary>
        public Scene CurrentScene { private set; get; }
    }
}