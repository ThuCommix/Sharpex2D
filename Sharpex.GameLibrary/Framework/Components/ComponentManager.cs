using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpexGL.Framework.Components
{
    public class ComponentManager : IConstructable
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("6A3D114D-6DF4-429E-82ED-F7CD0AE29CF8"); }
        }

        #endregion

        public delegate void ComponentChangedEventHandler(object sender, EventArgs e);
        private List<IComponent> _internalComponents = new List<IComponent>();
        private bool _alreadyCalledConstruct;
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
            if (_alreadyCalledConstruct)
            {
                //Single Construct.
                var com = component as IConstructable;
                if (com != null)
                {
                    com.Construct();
                }
            }
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

            //if not found query interfaces 
            foreach (var component in _internalComponents)
            {
                if (QueryInterface(component.GetType(), typeof (T)))
                {
                    return (T) component;
                }
            }

            throw new InvalidOperationException("Component not found (" + typeof(T).FullName + ").");
        }
        /// <summary>
        /// Initializes all Components.
        /// </summary>
        public void Construct()
        {
            for (var i = 0; i<= _internalComponents.Count -1; i++)
            {
                var com = _internalComponents[i] as IConstructable;
                if (com != null)
                {
                    com.Construct();
                }
            }
            _alreadyCalledConstruct = true;
        }
        /// <summary>
        /// Queries a type.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <param name="target">The TargetType.</param>
        /// <returns>True on success</returns>
        private bool QueryInterface(Type type, Type target)
        {
            return type.GetInterfaces().Any(implementation => implementation == target);
        }
    }
}
