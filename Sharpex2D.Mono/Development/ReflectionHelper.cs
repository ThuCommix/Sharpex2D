using System;
using System.Reflection;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class ReflectionHelper
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
    }
}