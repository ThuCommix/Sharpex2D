using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class UIManager
    {
        private readonly List<UIControl> _controls;

        /// <summary>
        ///     Initializes a new UICollection class.
        /// </summary>
        internal UIManager()
        {
            _controls = new List<UIControl>();
        }

        /// <summary>
        ///     Sets or gets the Description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        ///     Adds a new UIControl to the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Add(UIControl control)
        {
            _controls.Add(control);
        }

        /// <summary>
        ///     Removes a new UIControl from the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Remove(UIControl control)
        {
            _controls.Remove(control);
        }

        /// <summary>
        ///     Gets the spezified UIControl.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>UIControl</returns>
        public T Get<T>() where T : UIControl
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (typeof (T) == _controls[i].GetType())
                {
                    return (T) _controls[i];
                }
            }

            throw new ArgumentException("The UIControl " + typeof (T).Name + " could not be found.");
        }

        /// <summary>
        ///     Gets the UIControl spezified by its GUID.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>UIControl</returns>
        public UIControl Get(Guid guid)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (guid == _controls[i].Guid)
                {
                    return _controls[i];
                }
            }

            throw new ArgumentException("The UIControl with GUID " + guid + " could not be found.");
        }

        /// <summary>
        ///     Clears the UIManager.
        /// </summary>
        public void Clear()
        {
            _controls.Clear();
        }

        /// <summary>
        ///     Gets all UIControls.
        /// </summary>
        /// <returns>UIControl Array</returns>
        public UIControl[] GetAll()
        {
            return _controls.ToArray();
        }

        /// <summary>
        ///     Processes a Tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Tick(GameTime gameTime)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Enable)
                {
                    _controls[i].Tick(gameTime);
                }
            }
        }

        /// <summary>
        ///     Proceses a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(IRenderer renderer, GameTime gameTime)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Visible)
                {
                    _controls[i].OnRender(renderer);
                }
            }
        }
    }
}