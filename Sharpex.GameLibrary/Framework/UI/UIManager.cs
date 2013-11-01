using System;
using System.Collections.Generic;

namespace SharpexGL.Framework.UI
{
    public class UIManager
    {
        /// <summary>
        /// Initializes a new UICollection class.
        /// </summary>
        public UIManager()
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
    }
}
