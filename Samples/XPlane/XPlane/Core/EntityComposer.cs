using System.Collections.Generic;
using Sharpex2D;
using Sharpex2D.Audio;
using Sharpex2D.Content;
using Sharpex2D.Input;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using XPlane.Core.Entities;
using XPlane.Core.Miscellaneous;

namespace XPlane.Core
{
    public class EntityComposer : IUpdateable, IDrawable
    {
        /// <summary>
        /// Gets the SpawnRate.
        /// </summary>
        public const float SpawnRate = 0.7f;

        /// <summary>
        /// Gets the Achievementmanager
        /// </summary>
        public AchievementManager AchievementManager { get; private set; }

        /// <summary>
        /// Gets the FireDelay.
        /// </summary>
        public const float FireDelay = 500;

        private readonly Texture2D _enemyTexture;
        private readonly Texture2D _explosionTexture;
        private readonly Texture2D _projectileTexture;
        private readonly GameRandom _random;
        private float _currentFireDelay = FireDelay;
        private bool _enableHpBars;
        private float _timeSinceLastEnemy;
        private readonly SoundEffect _laserFire;
        private readonly SoundEffect _explosion;
        private readonly List<ScoreIndicator> _scoreIndicators;

        /// <summary>
        /// Initializes a new EntityComposer class.
        /// </summary>
        public EntityComposer()
        {
            Projectiles = new List<Projectile>();
            Enemies = new List<Enemy>();
            Explosions = new List<Explosion>();
            Input = SGL.QueryComponents<InputManager>();
            _scoreIndicators = new List<ScoreIndicator>();
            AchievementManager = new AchievementManager();
            Score = 0;
            _random = new GameRandom();

            var contentManager = SGL.QueryComponents<ContentManager>();

            var playerTexture = contentManager.Load<Texture2D>("shipAnimation.png");
            _projectileTexture = contentManager.Load<Texture2D>("laser.png");
            _enemyTexture = contentManager.Load<Texture2D>("mineAnimation.png");
            _explosionTexture = contentManager.Load<Texture2D>("explosion.png");
            EnableHPBars = true;

            _laserFire = new SoundEffect(SGL.QueryResource<Sound>("laserFire.wav"),
                AudioManager.Instance.SoundEffectGroups[0]);
            _explosion = new SoundEffect(SGL.QueryResource<Sound>("explosion.wav"),
                AudioManager.Instance.SoundEffectGroups[0]);

            AudioManager.Instance.SoundEffectGroups[0].MasterVolume = 0.05f;

            Player = new Player(playerTexture);
        }

        /// <summary>
        /// Gets the Player.
        /// </summary>
        public Player Player { private set; get; }

        /// <summary>
        /// Gets the Projectiles.
        /// </summary>
        public List<Projectile> Projectiles { private set; get; }

        /// <summary>
        /// Gets the Enemies.
        /// </summary>
        public List<Enemy> Enemies { private set; get; }

        /// <summary>
        /// Gets the Explosions.
        /// </summary>
        public List<Explosion> Explosions { private set; get; }

        /// <summary>
        /// Gets the Score.
        /// </summary>
        public int Score { private set; get; }

        /// <summary>
        /// A value indicating whether the game is over.
        /// </summary>
        public bool GameOver { private set; get; }

        /// <summary>
        /// A value indicating whether HP bars should be displayed.
        /// </summary>
        public bool EnableHPBars
        {
            set
            {
                _enableHpBars = value;
                foreach (Enemy enemy in Enemies)
                {
                    enemy.EnableHPBar = value;
                }
            }
            get { return _enableHpBars; }
        }

        /// <summary>
        /// Gets or sets the InputManager.
        /// </summary>
        private InputManager Input { set; get; }

        /// <summary>
        /// Renders the EntityComposer.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(RenderDevice renderer, GameTime gameTime)
        {
            Player.Render(renderer, gameTime);
            RenderEnemies(renderer, gameTime);
            RenderProjectiles(renderer, gameTime);
            RenderExplosions(renderer, gameTime);
            RenderDamageIndicators(renderer, gameTime);
            RenderGameMessage(renderer, gameTime);
        }

        /// <summary>
        /// Updates the EntityComposer.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            HandlePlayerInput(gameTime);
            CreateEnemyIfRequired(gameTime);
            UpdateEnemies(gameTime);
            UpdateProjectiles(gameTime);
            UpdateCollisions(gameTime);
            UpdateExplosion(gameTime);
            UpdateDamageIndicators(gameTime);
            UpdateGameMessage(gameTime);
            Player.Update(gameTime);

            AchievementManager.Get<SustainAchievement>().Add(gameTime.ElapsedGameTime);
        }

        /// <summary>
        /// Renders the enemies.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        private void RenderEnemies(RenderDevice renderer, GameTime gameTime)
        {
            foreach (Enemy t in Enemies)
            {
                t.Render(renderer, gameTime);
            }
        }

        /// <summary>
        /// Renders the projectiles.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        private void RenderProjectiles(RenderDevice renderer, GameTime gameTime)
        {
            foreach (Projectile t in Projectiles)
            {
                t.Render(renderer, gameTime);
            }
        }

        /// <summary>
        /// Renders the damage indicators.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        private void RenderDamageIndicators(RenderDevice renderer, GameTime gameTime)
        {
            foreach (var indicator in _scoreIndicators)
            {
                indicator.Render(renderer, gameTime);
            }
        }

        /// <summary>
        /// Renders the projectiles.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        private void RenderExplosions(RenderDevice renderer, GameTime gameTime)
        {
            foreach (Explosion t in Explosions)
            {
                t.Render(renderer, gameTime);
            }
        }

        /// <summary>
        /// Updates the projectiles.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectile t = Projectiles[i];
                if (t.Position.X > 801)
                {
                    Projectiles.Remove(t);
                    i++;
                }
                else
                {
                    t.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Updates the explosions.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateExplosion(GameTime gameTime)
        {
            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosion explosion = Explosions[i];
                if (explosion.RemainingLifeTime <= 0)
                {
                    Explosions.Remove(explosion);
                    i++;
                }
                else
                {
                    explosion.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Creates a new DamageIndicator.
        /// </summary>
        /// <param name="position">The Position.</param>
        /// <param name="score">The Score.</param>
        /// <param name="color">The Color.</param>
        private void CreateScoreIndicator(Vector2 position, int score, Color color)
        {
            var indicator = new ScoreIndicator {Position = position, Score = score, Color = color};
            _scoreIndicators.Add(indicator);
        }

        /// <summary>
        /// Creates a new explosion.
        /// </summary>
        /// <param name="position">The Position.</param>
        private void CreateExplosion(Vector2 position)
        {
            var explosion = new Explosion(_explosionTexture) {Position = position};

            Explosions.Add(explosion);
        }

        /// <summary>
        /// Updates the damage indicators.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateDamageIndicators(GameTime gameTime)
        {
            for (var i = 0; i < _scoreIndicators.Count; i++)
            {
                if (_scoreIndicators[i].LifeTime <= 0)
                {
                    _scoreIndicators.Remove(_scoreIndicators[i]);
                    i++;
                }
                else
                {
                    _scoreIndicators[i].Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Updates the game message.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateGameMessage(GameTime gameTime)
        {
            GameMessage.Instance.Update(gameTime);
        }

        /// <summary>
        /// Updates the game message.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        private void RenderGameMessage(RenderDevice renderer, GameTime gameTime)
        {
            GameMessage.Instance.Render(renderer, gameTime);
        }

        /// <summary>
        /// Creates a new game message.
        /// </summary>
        /// <param name="message">The Message.</param>
        private void CreateGameMessage(string message)
        {
            GameMessage.Instance.QueueMessage(message);
        }

        /// <summary>
        /// Updates the collisions.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateCollisions(GameTime gameTime)
        {
            var deadEnemies = new List<Enemy>();

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemy enemy = Enemies[i];
                if (enemy.IntersectsWith(Player))
                {
                    Player.Damage(Enemy.AttackDamage);
                    Enemies.Remove(enemy);
                    i++;

                    if (Player.Health <= 0)
                    {
                        GameOver = true;
                    }

                    //create explosion
#if AUDIO_ENABLED
                    _explosion.Play();
#endif

                    CreateExplosion(new Vector2(enemy.Position.X - 40, enemy.Position.Y - 33));
                }
                else
                {
                    for (int x = 0; x < Projectiles.Count; x++)
                    {
                        Projectile projectile = Projectiles[x];
                        if (projectile.IntersectsWith(enemy))
                        {
                            enemy.Damage(Projectile.AttackDamage);
                            //remove the projectile on hit.
                            Projectiles.Remove(projectile);
                            x++;

                            if (enemy.Health <= 0)
                            {
                                //remove the enemy if health is 0 or lower.
                                Enemies.Remove(enemy);
                                i++;
                                var score = (int) (enemy.Velocity*1000);

                                CreateScoreIndicator(enemy.Position, score, Color.White);
                                Score += score;


                                //notify achievement
                                AchievementManager.Get<EnemyDestroyedAchievement>().Add(1);
                                AchievementManager.Get<ScoreAchievement>().Add(Score);

                                //create explosion
#if AUDIO_ENABLED
                                _explosion.Play();
#endif

                                CreateExplosion(new Vector2(enemy.Position.X - 20, enemy.Position.Y - 30));
                            }
                        }
                    }
                }

                //check collision between enemies.

                for (int x = 0; x < Enemies.Count - 1; x++)
                {
                    if (enemy != Enemies[x])
                    {
                        if (enemy.IntersectsWith(Enemies[x]))
                        {
                            enemy.Damage(Enemy.AttackDamage);
                            Enemies[x].Damage(Enemy.AttackDamage);

#if AUDIO_ENABLED
                            _explosion.Play();
#endif

                            deadEnemies.Add(enemy);
                            CreateExplosion(new Vector2(enemy.Position.X - 20, enemy.Position.Y - 30));
                            deadEnemies.Add(Enemies[x]);
                            CreateExplosion(new Vector2(Enemies[x].Position.X - 20, Enemies[x].Position.Y - 30));
                        }
                    }
                }
            }

            foreach (Enemy enemy in deadEnemies)
            {
                Enemies.Remove(enemy);
            }
        }

        /// <summary>
        /// Updates the enemies.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void UpdateEnemies(GameTime gameTime)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemy enemy = Enemies[i];
                enemy.Position = new Vector2(enemy.Position.X - (enemy.Velocity*gameTime.ElapsedGameTime),
                    enemy.Position.Y);

                if (enemy.Position.X <= -48)
                {
                    //the enemy is out of bounds, remove it.
                    Enemies.Remove(enemy);
                    i++;

                    //new patch: also remove some score (75%)
                    Score -= (int)(enemy.Velocity * 1000 * 0.75);
                    if (Score < 0)
                    {
                        Score = 0;
                    }
                }
                else
                {
                    enemy.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Creates a new Enemy if required.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        private void CreateEnemyIfRequired(GameTime gameTime)
        {
            _timeSinceLastEnemy += gameTime.ElapsedGameTime;

            if (_timeSinceLastEnemy >= 750)
            {
                _timeSinceLastEnemy = 0;

                if (_random.NextBoolean(SpawnRate))
                {
                    var enemy = new Enemy(_enemyTexture)
                    {
                        Position = new Vector2(850, _random.Next(10, 420)),
                        Velocity = _random.Next(50, 200)/1000f,
                    };

                    enemy.MaximumHealth = 7 + (int) (40*enemy.Velocity);
                    enemy.EnableHPBar = EnableHPBars;

                    Enemies.Add(enemy);
                }
            }
        }

        /// <summary>
        /// Creates a new projectile.
        /// </summary>
        private void CreateProjectile()
        {
            var projectile = new Projectile(_projectileTexture)
            {
                Position = new Vector2(Player.Position.X + 119, Player.Position.Y + 33)
            };

            Projectiles.Add(projectile);

#if AUDIO_ENABLED
            _laserFire.Play();
#endif
        }

        /// <summary>
        /// Handles the player input.
        /// </summary>
        private void HandlePlayerInput(GameTime gameTime)
        {
            if (_currentFireDelay > 0)
            {
                _currentFireDelay -= gameTime.ElapsedGameTime;
            }

            //Keyboard input

            KeyboardState keyState = Input.Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left))
            {
                Player.Position = new Vector2(Player.Position.X - (Player.Velocity*gameTime.ElapsedGameTime),
                    Player.Position.Y);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                Player.Position = new Vector2(Player.Position.X + (Player.Velocity*gameTime.ElapsedGameTime),
                    Player.Position.Y);
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                Player.Position = new Vector2(Player.Position.X,
                    Player.Position.Y - (Player.Velocity*gameTime.ElapsedGameTime));
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                Player.Position = new Vector2(Player.Position.X,
                    Player.Position.Y + (Player.Velocity*gameTime.ElapsedGameTime));
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (_currentFireDelay <= 0)
                {
                    _currentFireDelay = FireDelay;
                    CreateProjectile();
                    AchievementManager.Get<LasterTimeAchievement>().Add(1);
                }
            }

            //bound player into screen

            if (Player.Position.X < 0)
            {
                Player.Position = new Vector2(0, Player.Position.Y);
            }

            if (Player.Position.X > 684)
            {
                Player.Position = new Vector2(684, Player.Position.Y);
            }

            if (Player.Position.Y < 0)
            {
                Player.Position = new Vector2(Player.Position.X, 0);
            }

            if (Player.Position.Y > 411)
            {
                Player.Position = new Vector2(Player.Position.X, 411);
            }
        }
    }
}