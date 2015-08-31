// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;

namespace Sharpex2D.Framework
{
    public static class AttributeHelper
    {
        /// <summary>
        /// Gets the Attribute.
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
        /// Gets the Attribute.
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
        /// Gets the Attributes.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <returns>Attribute.</returns>
        public static T[] GetAttributes<T>(object obj) where T : Attribute
        {
            return
                obj.GetType()
                    .GetCustomAttributes(typeof (T), true)
                    .Where(attribute => attribute.GetType() == typeof (T))
                    .Cast<T>()
                    .ToArray();
        }

        /// <summary>
        /// Gets the Attributes.
        /// </summary>
        /// <typeparam name="T">The Attribute Type.</typeparam>
        /// <param name="type">The Type.</param>
        /// <returns>Attribute.</returns>
        public static T[] GetAttributes<T>(Type type) where T : Attribute
        {
            return
                type.GetCustomAttributes(typeof (T), true)
                    .Where(attribute => attribute.GetType() == typeof (T))
                    .Cast<T>()
                    .ToArray();
        }

        /// <summary>
        /// Trys to get the Attribute.
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
        /// Trys to get the Attribute.
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
