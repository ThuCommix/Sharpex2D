using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Audio.WaveOut;
using Sharpex2D.GameService;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.OpenGL;
using Sharpex2D.Surface;
using XPlane.Core.Scenes;

namespace XPlane.Core
{
    public class Game1 : Game
    {
        /// <summary>
        /// Initializes the game.
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        /// <returns>EngineConfiguration.</returns>
        public override EngineConfiguration OnInitialize(LaunchParameters launchParameters)
        {
            return new EngineConfiguration(new OpenGLRenderDevice(), new WaveOutInitializer());
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void OnLoadContent()
        {
            GameComponentManager.Add(SceneManager);

            Content.Load<Sound>("laserFire.wav");
            Content.Load<Sound>("explosion.wav");
            Content.Load<Sound>("menuMusic.mp3");
            Content.Load<Sound>("gameMusic.mp3");

            AudioManager.SoundEffectGroups.Add(new SoundEffectGroup());

            SceneManager.AddScene(new GameScene());
            SceneManager.AddScene(new MenuScene());
            SceneManager.AddScene(new IntroScene());
            SceneManager.AddScene(new EndScene());
            SceneManager.ActiveScene = SceneManager.Get<IntroScene>();

            /*SGL.QueryComponents<RenderTarget>().Window.Size = new Sharpex2D.Math.Vector2(1200, 720);
            SGL.Components.Get<RenderDevice>().GraphicsDevice.BackBuffer.Scaling = true;*/
        }
    }
}