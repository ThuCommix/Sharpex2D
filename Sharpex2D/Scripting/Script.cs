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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Scripting
{
    public class Script : IContent, IEnumerable<MethodAttribute>
    {   
        private readonly Dictionary<MethodAttribute, MethodInfo> _methods;
        private Task _scriptThread;

        /// <summary>
        /// Initializes a new Script class.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="source">The Source.</param>
        /// <param name="scriptType">The ScriptType.</param>
        internal Script(string name, string source, ScriptType scriptType)
        {
            _methods = new Dictionary<MethodAttribute, MethodInfo>();
            Name = name;

            var assembly = ScriptCompiler.CompileToAssembly(source, scriptType);
            var flag = true;

            foreach (var method in assembly.GetTypes().SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)))
            {
                foreach (MethodAttribute entry in method.GetCustomAttributes(typeof (MethodAttribute), true))
                {
                    if (entry.Name == "")
                    {
                        throw new ScriptException("The method can not be named String.Empty.");
                    }
                    if (_methods.ContainsKey(entry))
                    {
                        throw new ScriptException("The method already exists.");
                    }

                    _methods.Add(entry, method);
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                throw new ScriptException("No methods found.");
            }
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { internal set; get; }

        /// <summary>
        /// A value indicating whether the script is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the entry points.
        /// </summary>
        public MethodAttribute[] Methods => _methods.Keys.ToArray();

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<MethodAttribute> GetEnumerator()
        {
            return _methods.Keys.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Triggered if the script finished executing.
        /// </summary>
        public event EventHandler<ScriptFinishedEventArgs> Finished;

        /// <summary>
        /// Runs a method asynchronously
        /// </summary>
        /// <param name="method">The method</param>
        /// <param name="parameters">The Parameters.</param>
        public void RunAsync(MethodAttribute method, params object[] parameters)
        {
            if (!_methods.ContainsKey(method))
            {
                throw new ScriptException("The method was not found.");
            }

            _scriptThread = new Task(() =>
            {
                IsRunning = true;
                Logger.Instance.Debug($"Running script <{Name}> with method <{method.Name}>.");
                object result = null;
                try
                {
                    result = _methods[method].Invoke(null, parameters);
                }
                catch (Exception ex)
                {
                    throw new ScriptException("Error while invoking method.", ex);
                }
                finally
                {
                    Logger.Instance.Debug($"Finished script <{Name}> with method <{method.Name}>.");
                    IsRunning = false;
                    Finished?.Invoke(this, new ScriptFinishedEventArgs(result, method));
                }
            });
            _scriptThread.Start();
        }

        /// <summary>
        /// Runs a method synchronously
        /// </summary>
        /// <param name="method">The method</param>
        /// <param name="parameters">The Parameters.</param>
        public object Run(MethodAttribute method, params object[] parameters)
        {
            if (!_methods.ContainsKey(method))
            {
                throw new ScriptException("The method was not found.");
            }

            IsRunning = true;
            Logger.Instance.Debug($"Running script <{Name}> with method <{method.Name}>.");
            try
            {
                return _methods[method].Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                throw new ScriptException("Error while invoking method.", ex);
            }
            finally
            {
                Logger.Instance.Debug($"Finished script <{Name}> with method <{method.Name}>.");
                IsRunning = false;
            }
        }
    }
}
