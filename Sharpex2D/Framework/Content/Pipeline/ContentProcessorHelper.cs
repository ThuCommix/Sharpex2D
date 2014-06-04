using System;
using System.Reflection;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ContentProcessorHelper
    {
        /// <summary>
        ///     Gets the property value with any access modifier.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="name">The Name.</param>
        /// <param name="obj">The Object being queried.</param>
        /// <returns>T Value.</returns>
        public static T GetPropertyValue<T>(string name, object obj)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Instance |
                                                                          BindingFlags.NonPublic |
                                                                          BindingFlags.Public))
            {
                if (property.Name == name && property.PropertyType == typeof (T))
                {
                    return (T) property.GetValue(obj, null);
                }
            }

            throw new MissingMemberException("Property " + name + " not found in " + obj.GetType().Name);
        }

        /// <summary>
        ///     Invokes a contructor with any access modifier.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>T.</returns>
        public static T InvokeConstructor<T>(object[] parameters)
        {
            try
            {
                return
                    (T)
                        Activator.CreateInstance(typeof (T), BindingFlags.NonPublic | BindingFlags.Instance, null,
                            parameters, null);
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException("Unable to call ctor of " + typeof (T).Name, ex);
            }
        }

        /// <summary>
        ///     Gets the Attribute.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <returns>Attribute.</returns>
        public static T GetAttribute<T>(object obj) where T : Attribute
        {
            foreach (object attribute in obj.GetType().GetCustomAttributes(typeof (T), true))
            {
                if (attribute.GetType() == typeof (T))
                {
                    return (T) attribute;
                }
            }

            throw new InvalidOperationException("The Attribute with type " + typeof (T).Name + " was not found in " +
                                                obj.GetType().Name);
        }

        /// <summary>
        ///     Gets the Attribute.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="type">The Type.</param>
        /// <returns>Attribute.</returns>
        public static T GetAttribute<T>(Type type) where T : Attribute
        {
            foreach (object attribute in type.GetCustomAttributes(typeof (T), true))
            {
                if (attribute.GetType() == typeof (T))
                {
                    return (T) attribute;
                }
            }

            throw new InvalidOperationException("The Attribute with type " + typeof (T).Name + " was not found in " +
                                                type.Name);
        }
    }
}