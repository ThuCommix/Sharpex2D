using System;
using System.Windows.Forms;
using FlyingBird.Misc;
using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Audio.WaveOut;
using Sharpex2D.GameService;
using Sharpex2D.Input;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.DirectX9;
using Sharpex2D.Rendering.GDI;
using Sharpex2D.Surface;
using MouseButtons = Sharpex2D.Input.MouseButtons;

namespace FlyingBird
{
    public class MainGame : Game
    {
        private AnimatedBackground _animatedBackground;
        private string _deviceHint;
        private Vector2 _deviceHintPosition;
        private SoundEffect _die;
        private Font _font;
        private bool _gameStarted;
        private Instructions _instructions;
        private bool _isDead;
        private PipeManager _pipeManager;
        private Player _player;
        private SoundEffect _receivedCoin;
        private string _resolution;
        private Scoreboard _scoreboard;
        private SoundEffect _swing;
        private MouseState _newMouseState;

        /// <summary>
        ///     Updates the game.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public override void OnUpdate(GameTime gameTime)
        {
            UpdateBackground(gameTime);
            if (!_gameStarted || _isDead)
            {
                UpdateInstructions(gameTime);
            }

            if (_gameStarted)
            {
                UpdateCollision();
                UpdatePlayer(gameTime);
                UpdatePipes(gameTime);
            }
            UpdateInput();
        }

        /// <summary>
        ///     Renders the game.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public override void OnRendering(RenderDevice renderer, GameTime gameTime)
        {
            renderer.Begin();
            DrawBackground(renderer);
            DrawRenderHint(renderer);
            if (_gameStarted)
            {
                DrawPipes(renderer);
                DrawPlayer(renderer);
                DrawScore(renderer);
            }

            if (!_gameStarted || _isDead)
            {
                _instructions.InstructionFlag = _isDead;
                DrawInstructions(renderer);
            }

            renderer.End();
        }

        /// <summary>
        ///     Initializes the game.
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        /// <returns>EngineConfiguration.</returns>
        public override EngineConfiguration OnInitialize(LaunchParameters launchParameters)
        {
            _resolution = "640x480";
            if (launchParameters.KeyAvailable("Resolution"))
            {
                _resolution = launchParameters["Resolution"];
            }

            var waveOutInitializer = new WaveOutInitializer();

            if (!waveOutInitializer.IsSupported)
            {
                MessageBox.Show("WaveOut wird nicht supported. Das Spiel wird ohne Ton gestartet.", "FlyingBird",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (launchParameters.KeyAvailable("Device"))
            {
                switch (launchParameters["Device"])
                {
                    case "DirectX9":
                        _deviceHint = "DirectX9";
                        return new EngineConfiguration(
                            new DirectXRenderDevice(),
                            waveOutInitializer.IsSupported ? waveOutInitializer : null);
                    case "DirectX10":
                        _deviceHint = "DirectX10";
                        return new EngineConfiguration(
                            new Sharpex2D.Rendering.DirectX10.DirectXRenderDevice(),
                            waveOutInitializer.IsSupported ? waveOutInitializer : null);
                    case "DirectX11":
                        _deviceHint = "DirectX11";
                        return new EngineConfiguration(
                            new Sharpex2D.Rendering.DirectX11.DirectXRenderDevice(),
                            waveOutInitializer.IsSupported ? waveOutInitializer : null);
                    case "GDI+":
                        _deviceHint = "GDI+ :<(";
                        return new EngineConfiguration(new GDIRenderDevice(),
                            waveOutInitializer.IsSupported ? waveOutInitializer : null);
                }
            }
            _deviceHint = "DirectX11";
            return new EngineConfiguration(
                new Sharpex2D.Rendering.DirectX11.DirectXRenderDevice(),
                waveOutInitializer.IsSupported ? waveOutInitializer : null);
        }

        /// <summary>
        ///     Loads the content.
        /// </summary>
        public override void OnLoadContent()
        {
            _animatedBackground = new AnimatedBackground(Content.Load<Texture2D>("background.png"));
            _instructions = new Instructions(Content.Load<Texture2D>("instructions.png")) {Visible = true};
            _player = new Player(Content.Load<Texture2D>("bird_sprite.png"), Content.Load<Texture2D>("bird_erased.png"));
            _receivedCoin = new SoundEffect(Content.Load<Sound>("score.wav")) { Volume = 0.2f };
            _die = new SoundEffect(Content.Load<Sound>("dead.wav")) { Volume = 0.2f };
            _scoreboard = new Scoreboard();
            _pipeManager = new PipeManager(Content.Load<Texture2D>("pipe_body.png"),
                Content.Load<Texture2D>("pipe_bottom.png"), Content.Load<Texture2D>("pipe_top.png"));
            _swing = new SoundEffect(Content.Load<Sound>("swing.wav")){Volume = 0.2f};
            _pipeManager.ScoreChanged += _pipeManager_ScoreChanged;
            _font = new Font("Segoe UI", 9, TypefaceStyle.Regular);
            _deviceHintPosition = new Vector2(0, 0);

            int x = Convert.ToInt32(_resolution.Split('x')[0]);
            int y = Convert.ToInt32(_resolution.Split('x')[1]);

            SGL.Components.Get<RenderTarget>().Window.Size = new Vector2(x, y);
            SGL.Components.Get<GraphicsDevice>().BackBuffer.Scaling = true;
        }

        /// <summary>
        ///     ScoreChanged.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _pipeManager_ScoreChanged(object sender, EventArgs e)
        {
            _receivedCoin.Play();
        }

        /// <summary>
        ///     Draws the background.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawBackground(RenderDevice renderer)
        {
            _animatedBackground.Render(renderer);
        }

        /// <summary>
        ///     Draws the Instructions.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawInstructions(RenderDevice renderer)
        {
            _instructions.Render(renderer);
        }

        /// <summary>
        ///     Draws a player.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawPlayer(RenderDevice renderer)
        {
            _player.Render(renderer);
        }

        /// <summary>
        ///     Draws the score.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawScore(RenderDevice renderer)
        {
            _scoreboard.Score = _pipeManager.PassedPipes;
            _scoreboard.Render(renderer);
        }

        /// <summary>
        ///     Draws the renderhint.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawRenderHint(RenderDevice renderer)
        {
            renderer.DrawString(_deviceHint, _font, _deviceHintPosition, Color.Black);
        }

        /// <summary>
        ///     Draws the pipes.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        private void DrawPipes(RenderDevice renderer)
        {
            if (_isDead)
            {
                if (_pipeManager.Opacity > 0)
                {
                    _pipeManager.Opacity -= 0.05f;
                }
            }
            _pipeManager.Render(renderer);
        }

        /// <summary>
        ///     Updates the background.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateBackground(GameTime gameTime)
        {
            if (!_isDead)
            {
                _animatedBackground.Update(gameTime);
            }
        }

        /// <summary>
        ///     Updates the Pipes.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdatePipes(GameTime gameTime)
        {
            if (!_isDead)
            {
                _pipeManager.Update(gameTime);
            }
        }

        /// <summary>
        ///     Updates the Instructions.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateInstructions(GameTime gameTime)
        {
            _instructions.Update(gameTime);
        }

        /// <summary>
        ///     Updates the Player.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdatePlayer(GameTime gameTime)
        {
            _player.Dead = _isDead;
            if (_player.Position.Y >= 456)
            {
                if (!_isDead)
                {
                    _die.Play();
                    _isDead = true;
                }
                return;
            }

            if (_player.Position.Y <= 0)
            {
                if (!_isDead)
                {
                    _die.Play();
                    _isDead = true;
                }
            }

            _player.Update(gameTime);
        }

        /// <summary>
        ///     Updates the Input.
        /// </summary>
        private void UpdateInput()
        {
            _newMouseState = Input.Mouse.GetState();

            if (!_gameStarted)
            {
                if (_newMouseState.IsMouseButtonDown(MouseButtons.Left))
                {
                    _gameStarted = true;
                }
            }
            else
            {
                if (!_isDead)
                {
                    if (_newMouseState.IsMouseButtonDown(MouseButtons.Left))
                    {
                        _player.Velocity= new Vector2(0, -4.5f);
                        _swing.Play();
                    }
                }
            }

            if (_gameStarted && _isDead)
            {
                if (_newMouseState.IsMouseButtonDown(MouseButtons.Left))
                {
                    //reset game.
                    _player.Position = new Vector2(300, 230);
                    _player.Dead = false;
                    _player.Velocity = new Vector2(0, 0);
                    _isDead = false;
                    _gameStarted = false;
                    _pipeManager.Reset();
                    _pipeManager.Opacity = 1f;
                }
            }
        }

        /// <summary>
        ///     Updates the collision.
        /// </summary>
        private void UpdateCollision()
        {
            if (!_isDead)
            {
                if (_pipeManager.Intersects(_player))
                {
                    _isDead = true;
                    _die.Play();
                }
            }
        }
    }
}