using System;
using Sharpex2D;
using Sharpex2D.Debug;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using XPlane.Core.Entities;

namespace XPlane.Core.Miscellaneous
{
    public class DebugDisplay : IDrawable, IUpdateable
    {
        private const float RefreshRate = 1000;
        private readonly CpuWatcher _cpuWatcher;
        private readonly EntityComposer _currentEntityComposer;
        private readonly Rectangle _display;
        private readonly Font _font;
        private readonly GameLoop _gameLoop;
        private readonly MemoryWatcher _memoryWatcher;
        private readonly Pen _pen;
        private readonly Pen _pen2;
        private readonly Pen _pen3;
        private readonly ThreadWatcher _threadWatcher;
        private float _currentTime;

        private string _debugMessage;

        /// <summary>
        /// Initializes a new DebugDisplay class.
        /// </summary>
        /// <param name="entityComposer">The EntityComposer.</param>
        public DebugDisplay(EntityComposer entityComposer)
        {
            _currentEntityComposer = entityComposer;
            _gameLoop = SGL.QueryComponents<GameLoop>();

            _font = new Font("Segoe UI", 12, TypefaceStyle.Regular);
            _cpuWatcher = new CpuWatcher();
            _memoryWatcher = new MemoryWatcher();
            _threadWatcher = new ThreadWatcher();
            _display = new Rectangle(0, 0, 800, 480);
            _debugMessage = "Query information ...";
            _pen = new Pen(Color.Green, 1);
            _pen2 = new Pen(Color.Red, 1);
            _pen3 = new Pen(Color.Blue, 1);

            _cpuWatcher.Start();
            _memoryWatcher.Start();
            _threadWatcher.Start();
        }

        /// <summary>
        /// A value indicating whether the debug display is visible.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Draws the DebugDisplay.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Visible) return;

            foreach (Enemy enemy in _currentEntityComposer.Enemies)
            {
                spriteBatch.DrawRectangle(_pen, enemy.Bounds);
            }

            foreach (Projectile projectile in _currentEntityComposer.Projectiles)
            {
                spriteBatch.DrawRectangle(_pen2, projectile.Bounds);
            }

            spriteBatch.DrawRectangle(_pen3, _currentEntityComposer.Player.Bounds);

            spriteBatch.DrawString(_debugMessage, _font, _display, Color.White);
        }

        /// <summary>
        /// Updates the DebugDisplay.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!Visible) return;
            _currentTime += gameTime.ElapsedGameTime;
            if (_currentTime >= RefreshRate)
            {
                _currentTime = 0;
                Memory memory = _memoryWatcher.Memory;
                memory.Convert(MemoryUnit.Kilobyte);

                _debugMessage =
                    string.Format(
                        "Frames: {0}, Updates: {1}, ThreadTime: {2}ms, CPU: {3}%, Allocated Memory: {4}KB, Threads: {5} {6}{6}" +
                        "PlayerHealth: {7}{6} ProjectileDamage: {8}{6} EnemyDamage: {9}{6} PlayerVelocity: {10}{6} ProjectileVelocity: {11}{6} ActiveEnemies: {12}{6} ActiveExplosions: {13}{6} ActiveProjectiles: {14}{6} GameOver: {15}{6}",
                        _gameLoop.MeasuredFrames, _gameLoop.MeasuredUpdates, gameTime.ElapsedGameTime,
                        _cpuWatcher.CpuUsage,
                        memory.Size, _threadWatcher.Count, Environment.NewLine, _currentEntityComposer.Player.Health,
                        Projectile.AttackDamage, Enemy.AttackDamage, Player.Velocity, Projectile.Velocity,
                        _currentEntityComposer.Enemies.Count, _currentEntityComposer.Explosions.Count,
                        _currentEntityComposer.Projectiles.Count, _currentEntityComposer.GameOver);
            }
        }
    }
}