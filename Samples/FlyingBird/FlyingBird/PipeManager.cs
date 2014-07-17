using System;
using System.Collections.Generic;
using FlyingBird.Objects;
using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace FlyingBird
{
    public class PipeManager
    {
        public delegate void ScoreChangedEventHandler(object sender, EventArgs e);

        private readonly object _lockObj = new object();

        private readonly Texture2D _pipeBody;
        private readonly Texture2D _pipeBottom;
        private readonly Texture2D _pipeTop;
        private readonly List<Pipe> _pipes;
        private readonly GameRandom _random;
        private float _elapsed;

        /// <summary>
        ///     Initializes a new PipeManager class.
        /// </summary>
        /// <param name="pipeBody">The PipeBody.</param>
        /// <param name="pipeBottom">The PipeBottom head.</param>
        /// <param name="pipeTop">The PipeTop head.</param>
        public PipeManager(Texture2D pipeBody, Texture2D pipeBottom, Texture2D pipeTop)
        {
            _pipes = new List<Pipe>();
            _pipeBody = pipeBody;
            _pipeBottom = pipeBottom;
            _pipeTop = pipeTop;
            _random = new GameRandom();
            Opacity = 1f;
        }

        /// <summary>
        ///     Gets or sets the Opacity.
        /// </summary>
        public float Opacity { set; get; }

        /// <summary>
        ///     Gets the PassesPipes.
        /// </summary>
        public int PassedPipes { private set; get; }

        /// <summary>
        ///     ScoreChanged event.
        /// </summary>
        public event ScoreChangedEventHandler ScoreChanged;

        /// <summary>
        ///     Renders the object.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public void Render(RenderDevice renderer)
        {
            lock (_lockObj)
            {
                foreach (Pipe pipe in _pipes)
                {
                    if (pipe.Position.X > -60) //only draw if its in our view
                    {
                        renderer.DrawTexture(_pipeBody,
                            new Rectangle(pipe.Position.X, pipe.Position.Y, 44, pipe.TopPipeHeight), Opacity);
                        renderer.DrawTexture(_pipeBottom,
                            new Vector2(pipe.Position.X - 1, pipe.Position.Y + pipe.TopPipeHeight), Opacity);

                        renderer.DrawTexture(_pipeBody,
                            new Rectangle(pipe.Position.X, pipe.BottomPipeY, 44, pipe.BottomPipeHeight), Opacity);

                        renderer.DrawTexture(_pipeBody, new Vector2(pipe.Position.X, pipe.BottomPipeY), Opacity);

                        renderer.DrawTexture(_pipeTop, new Vector2(pipe.Position.X - 1, pipe.BottomPipeY - 22), Opacity);
                    }
                }
            }
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime;
            if (_elapsed >= 2800)
            {
                _elapsed = 0;
                var pipe = new Pipe(_random.Next(100, 300), 80);
                pipe.Position = new Vector2(650, 0);
                _pipes.Add(pipe);
            }

            var obsoletPipes = new List<Pipe>();

            foreach (Pipe pipe in _pipes)
            {
                if (pipe.Position.X > -60)
                {
                    pipe.Position = new Vector2(pipe.Position.X - 1.5f, pipe.Position.Y);
                    if (pipe.Position.X < 315 && !pipe.Scored)
                    {
                        pipe.Scored = true;
                        PassedPipes += 1;

                        if (ScoreChanged != null)
                        {
                            ScoreChanged(this, EventArgs.Empty);
                        }
                    }
                }
                else
                {
                    obsoletPipes.Add(pipe);
                }
            }

            lock (_lockObj)
            {
                //remove pipes
                foreach (Pipe pipe in obsoletPipes)
                {
                    _pipes.Remove(pipe);
                }
            }
        }

        /// <summary>
        ///     A value indicating whether the player intersects.
        /// </summary>
        /// <param name="player">The Player.</param>
        /// <returns>True if intersecting.</returns>
        public bool Intersects(Player player)
        {
            lock (_lockObj)
            {
                foreach (Pipe pipe in _pipes)
                {
                    /*/var polygon1 = new Polygon();
                    polygon1.Add(new Vector2(pipe.Position.X, pipe.Position.Y),
                        new Vector2(pipe.Position.X + 44, pipe.Position.Y),
                        new Vector2(pipe.Position.X + 44, pipe.Position.Y + pipe.TopPipeHeight + 22),
                        new Vector2(pipe.Position.X, pipe.Position.Y + pipe.TopPipeHeight + 22));

                    var polygon2 = new Polygon();
                    polygon2.Add(new Vector2(pipe.Position.X, 480), new Vector2(pipe.Position.X + 44, 480),
                      new Vector2(pipe.Position.X + 44, pipe.BottomPipeY - 22),
                                   new Vector2(pipe.Position.X, pipe.BottomPipeY - 22));


                    if (player.Bounds.Intersects(polygon1))
                    {
                        return true;
                    }

                    if (player.Bounds.Intersects(polygon2))
                    {
                        return true;
                    }/*/

                    var rect1 = new Rectangle(pipe.Position.X, pipe.Position.Y, 46, pipe.TopPipeHeight + 22);
                    var rect2 = new Rectangle(pipe.Position.X, pipe.BottomPipeY, 46, pipe.BottomPipeHeight);
                    var playerrect = new Rectangle(player.Position, new Vector2(32, 24));

                    if (playerrect.Intersects(rect1) || playerrect.Intersects(rect2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///     Resets the PipeManager.
        /// </summary>
        public void Reset()
        {
            _pipes.Clear();
            _elapsed = 0;
            PassedPipes = 0;
        }
    }
}