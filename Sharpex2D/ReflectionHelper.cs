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
using System.Reflection;

namespace Sharpex2D.Framework
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets the property value with any access modifier.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="name">The Title.</param>
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
        /// Invokes a contructor with any access modifier.
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
