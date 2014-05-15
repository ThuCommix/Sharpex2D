using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.UI
{
    public class UIManager : IComponent
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("7FE0E5C1-1289-4A56-828A-264D010682DE"); } }

        #endregion

        /// <summary>
        /// Initializes a new UICollection class.
        /// </summary>
        internal UIManager()
        {
            _controls = new List<UIControl>();
        }

        private readonly List<UIControl> _controls;

        /// <summary>
        /// Adds a new UIControl to the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Add(UIControl control)
        {
            _controls.Add(control);
        }

        /// <summary>
        /// Removes a new UIControl from the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Remove(UIControl control)
        {
            _controls.Remove(control);
        }

        /// <summary>
        /// Gets the spezified UIControl.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>UIControl</returns>
        public T Get<T>() where T : UIControl
        {
            for (var i = 0; i <= _controls.Count - 1; i++)
            {
                if (typeof (T) == _controls[i].GetType())
                {
                    return (T)_controls[i];
                }
            }

            throw new ArgumentException("The UIControl " + typeof (T).Name + " could not be found.");
        }

        /// <summary>
        /// Gets the UIControl spezified by its GUID.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>UIControl</returns>
        public UIControl Get(Guid guid)
        {
            for (var i = 0; i <= _controls.Count - 1; i++)
            {
                if (guid == _controls[i].Guid)
                {
                    return _controls[i];
                }
            }

            throw new ArgumentException("The UIControl with GUID " + guid + " could not be found.");
        }

        /// <summary>
        /// Gets all UIControls.
        /// </summary>
        /// <returns>UIControl Array</returns>
        public UIControl[] GetAll()
        {
            return _controls.ToArray();
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            for (var i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Enable)
                {
                    _controls[i].Tick(elapsed);
                }
            }
        }

        /// <summary>
        /// Proceses a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            for (var i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Visible)
                {
                    _controls[i].OnRender(renderer);
                }
            }
        }
    }
}
