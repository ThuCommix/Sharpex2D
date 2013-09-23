using System;
using System.Collections.Generic;
using System.Linq;
namespace SharpexGL.Framework.Components
{
    public class ComponentManager : IConstructable
    {
        public delegate void ComponentChangedEventHandler(object sender, EventArgs e);
        private List<IComponent> _internalComponents = new List<IComponent>();
        public event ComponentChangedEventHandler ComponentChanged;
        /// <summary>
        /// Access to the Components enumeration.
        /// </summary>
        public List<IComponent> Components
        {
            get
            {
                return _internalComponents;
            }
            set
            {
                _internalComponents = value;
                if (ComponentChanged != null)
                {
                    ComponentChanged(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Adds a new Component to the enumeration.
        /// </summary>
        /// <param name="component">The Component.</param>
        public void AddComponent(IComponent component)
        {
            Components.Add(component);
        }
        /// <summary>
        /// Removes a Component from the enumeration.
        /// </summary>
        /// <param name="component">The Component.</param>
        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component);
        }
        /// <summary>
        /// Returns a specific component if exists. 
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Component</returns>
        public T Get<T>()
        {
            foreach (var component in _internalComponents)
            {
                if (component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }
            throw new InvalidOperationException("Component not found (" + typeof(T).FullName + ").");
        }
        /// <summary>
        /// Initializes all Components.
        /// </summary>
        public void Construct()
        {
            /*/foreach (var com in Components.OfType<IConstructable>())
            {
                com.Construct();
            }/*/

            for (var i = 0; i<= _internalComponents.Count -1; i++)
            {
                var com = _internalComponents[i] as IConstructable;
                if (com != null)
                {
                    com.Construct();
                }
            }
        }
    }
}
