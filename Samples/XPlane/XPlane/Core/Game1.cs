using System;
using System.Net;
using System.Windows.Forms;
using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Audio.WaveOut;
using Sharpex2D.GameService;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.OpenGL;
using Sharpex2D.Surface;
using XPlane.Core.Miscellaneous;
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
            return new EngineConfiguration(new OpenGLGraphicsManager(), new WaveOutInitializer());
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void OnLoadContent()
        {
            GameComponentManager.Add(SceneManager);
            GameComponentManager.Add(GameMessage.Instance);

            GameMessage.Instance.QueueMessage(string.Format("Welcome {0}.", Environment.UserName));

            var webClient = new WebClient();
            webClient.DownloadStringCompleted += DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://games.thucommix.de/xplane/cv.txt"));

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
        }

        /// <summary>
        /// Triggered if the download is completed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && e.Cancelled == false)
            {
                try
                {
                    if (Version.Parse(Application.ProductVersion) < Version.Parse(e.Result))
                    {
                        GameMessage.Instance.QueueMessage("A new update is available");
                    }
                }
                catch
                {

                }
            }
            else
            {
                GameMessage.Instance.QueueMessage("Can't connect to http://games.thucommix.de/.");
            }
        }
    }
}