using System.Collections.Generic;
using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using XPlane.Core.Entities;

namespace XPlane.Core.Miscellaneous
{
    public class Minimap : IDrawable, IUpdateable
    {
        /// <summary>
        /// Gets the Width.
        /// </summary>
        public const int Width = 150;

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public const int Height = 150;

        /// <summary>
        /// Gets the X.
        /// </summary>
        public const int X = 650;

        /// <summary>
        /// Gets the Y.
        /// </summary>
        public const int Y = 380;

        private readonly EntityComposer _currentEntityComposer;
        private readonly List<Vector2> _enemyPositions;

        private readonly float _magicNumberX;
        private readonly float _magicNumberY;

        private readonly Texture2D _minimap;
        private readonly List<Vector2> _projectilePositions;

        private Vector2 _playerPosition;

        /// <summary>
        /// Initializes a new Minimap class.
        /// </summary>
        /// <param name="entityComposer">The EntityComposer.</param>
        public Minimap(EntityComposer entityComposer)
        {
            _enemyPositions = new List<Vector2>();
            _currentEntityComposer = entityComposer;
            _projectilePositions = new List<Vector2>();
            _magicNumberX = 800/Width;
            _magicNumberY = 800/Height;
            Visible = true;
            _minimap = SGL.QueryComponents<ContentManager>().Load<Texture2D>("minimap.png");
        }

        /// <summary>
        /// A value indicating whether the Minimap is visible.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Renders the Minimap.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(RenderDevice renderer, GameTime gameTime)
        {
            if (!Visible) return;

            renderer.DrawTexture(_minimap, new Vector2(X, Y), 0.4f);

            foreach (Vector2 enemy in _enemyPositions)
            {
                renderer.FillRectangle(Color.Red, new Rectangle(enemy.X, enemy.Y, 5, 5));
            }

            foreach (Vector2 projectile in _projectilePositions)
            {
                renderer.FillRectangle(Color.Blue, new Rectangle(projectile.X, projectile.Y, 2, 2));
            }

            renderer.FillRectangle(Color.Green, new Rectangle(_playerPosition.X, _playerPosition.Y, 5, 5));
        }

        /// <summary>
        /// Updates the minimap.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!Visible) return;

            _enemyPositions.Clear();
            _projectilePositions.Clear();

            foreach (Enemy enemy in _currentEntityComposer.Enemies)
            {
                if (enemy.Position.X < 0 || enemy.Position.X > 800) continue;

                _enemyPositions.Add(new Vector2(X + (enemy.Position.X/_magicNumberX),
                    Y + (enemy.Position.Y/_magicNumberY)));
            }

            foreach (Projectile projectile in _currentEntityComposer.Projectiles)
            {
                _projectilePositions.Add(new Vector2(X + (projectile.Position.X/_magicNumberX),
                    Y + (projectile.Position.Y/_magicNumberY)));
            }

            _playerPosition = new Vector2(X + ((_currentEntityComposer.Player.Position.X + 60)/_magicNumberX),
                Y + ((_currentEntityComposer.Player.Position.Y + 34)/_magicNumberY));
        }
    }
}