using System;
using System.Collections.Generic;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.UI
{
    public static class UIManager
    {
        /// <summary>
        /// Initializes a new UICollection class.
        /// </summary>
        static UIManager()
        {
            Controls = new List<UIControl>();
        }

        private static readonly List<UIControl> Controls;

        /// <summary>
        /// Adds a new UIControl to the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public static void Add(UIControl control)
        {
            Controls.Add(control);
        }

        /// <summary>
        /// Removes a new UIControl from the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public static void Remove(UIControl control)
        {
            Controls.Remove(control);
        }

        /// <summary>
        /// Gets the spezified UIControl.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>UIControl</returns>
        public static T Get<T>() where T : UIControl
        {
            for (var i = 0; i <= Controls.Count - 1; i++)
            {
                if (typeof (T) == Controls[i].GetType())
                {
                    return (T)Controls[i];
                }
            }

            throw new ArgumentException("The UIControl " + typeof (T).Name + " could not be found.");
        }

        /// <summary>
        /// Gets the UIControl spezified by its GUID.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>UIControl</returns>
        public static UIControl Get(Guid guid)
        {
            for (var i = 0; i <= Controls.Count - 1; i++)
            {
                if (guid == Controls[i].Guid)
                {
                    return Controls[i];
                }
            }

            throw new ArgumentException("The UIControl with GUID " + guid + " could not be found.");
        }

        /// <summary>
        /// Gets all UIControls.
        /// </summary>
        /// <returns>UIControl Array</returns>
        public static UIControl[] GetAll()
        {
            return Controls.ToArray();
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public static void Tick(float elapsed)
        {
            for (var i = 0; i <= Controls.Count - 1; i++)
            {
                Controls[i].OnTick(elapsed);
            }
        }

        /// <summary>
        /// Proceses a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public static void Render(IRenderer renderer, float elapsed)
        {
            for (var i = 0; i <= Controls.Count - 1; i++)
            {
                if (Controls[i].Visible)
                {
                    Controls[i].OnTick(elapsed);
                }
            }
        }
    }
}
