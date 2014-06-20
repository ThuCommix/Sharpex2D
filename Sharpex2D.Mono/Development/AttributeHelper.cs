using System;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class AttributeHelper
    {
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

        /// <summary>
        ///     Trys to get the Attribute.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="type">The Type.</param>
        /// <param name="value">The Value.</param>
        /// <returns>True on success.</returns>
        public static bool TryGetAttribute<T>(Type type, out T value) where T : Attribute
        {
            try
            {
                value = GetAttribute<T>(type);
                return true;
            }
            catch (Exception)
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        ///     Trys to get the Attribute.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <param name="value">The Value.</param>
        /// <returns>True on success.</returns>
        public static bool TryGetAttribute<T>(object obj, out T value) where T : Attribute
        {
            try
            {
                value = GetAttribute<T>(obj.GetType());
                return true;
            }
            catch (Exception)
            {
                value = default(T);
                return false;
            }
        }
    }
}