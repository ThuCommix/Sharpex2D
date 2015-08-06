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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Script : IContent, IEnumerable<EntryPoint>
    {
        private readonly Dictionary<string, MethodInfo> _entries;
        private readonly List<EntryPoint> _entryPoints;
        private readonly Logger _logger;
        private Task _scriptThread;

        /// <summary>
        /// Initializes a new Script class.
        /// </summary>
        /// <param name="source">The Source.</param>
        /// <param name="scriptType">The ScriptType.</param>
        internal Script(string source, ScriptType scriptType)
        {
            _logger = LogManager.GetClassLogger();
            _entryPoints = new List<EntryPoint>();
            _entries = new Dictionary<string, MethodInfo>();
            Source = source;
            Type = scriptType;

            Assembly assembly = ScriptCompiler.CompileToAssembly(this);
            bool flag = true;

            foreach (Type type in assembly.GetTypes())
            {
                foreach (
                    MethodInfo method in
                        type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    foreach (EntryAttribute entry in method.GetCustomAttributes(typeof (EntryAttribute), true))
                    {
                        if (entry.Name == "")
                        {
                            throw new ScriptException("The entry can not be named String.Empty.");
                        }
                        if (_entries.ContainsKey(entry.Name))
                        {
                            throw new ScriptException("The entry already exists.");
                        }

                        _entries.Add(entry.Name, method);
                        _entryPoints.Add(new EntryPoint(entry.Name));
                        flag = false;
                        break;
                    }
                }
            }

            if (flag)
            {
                throw new ScriptException("The entry point was not found.");
            }
        }

        /// <summary>
        /// Gets the SourceCode.
        /// </summary>
        internal string Source { get; private set; }

        /// <summary>
        /// A value indicating whether the script is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the ScriptType.
        /// </summary>
        public ScriptType Type { get; private set; }

        /// <summary>
        /// Gets the entry points.
        /// </summary>
        public EntryPoint[] EntryPoints
        {
            get { return _entryPoints.ToArray(); }
        }

        /// <summary>
        /// Triggered if the script finished executing.
        /// </summary>
        public event EventHandler<ScriptFinishedEventArgs> Finished;

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="entry">The ScriptEntry.</param>
        /// <param name="parameters">The Parameters.</param>
        public void ExecuteAsync(EntryPoint entry, params object[] parameters)
        {
            if (!_entries.ContainsKey(entry.Name))
            {
                throw new ScriptException("The entry point was not found.");
            }

            _scriptThread = new Task(() =>
            {
                IsRunning = true;
                _logger.Info("Running script <{0}>.", entry);
                object result = null;
                try
                {
                    result = _entries[entry.Name].Invoke(null, parameters);
                }
                catch (Exception ex)
                {
                    throw new ScriptException("Error while invoking method.", ex);
                }
                finally
                {
                    _logger.Info("Finished script <{0}>.", entry);
                    IsRunning = false;
                    if (Finished != null)
                    {
                        Finished(this, new ScriptFinishedEventArgs(result, entry));
                    }
                }
            });
            _scriptThread.Start();
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="entry">The ScriptEntry.</param>
        /// <param name="parameters">The Parameters.</param>
        public object Execute(EntryPoint entry, params object[] parameters)
        {
            if (!_entries.ContainsKey(entry.Name))
            {
                throw new ScriptException("The entry point was not found.");
            }

            IsRunning = true;
            _logger.Info("Running script <{0}>.", entry);
            try
            {
                return _entries[entry.Name].Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                throw new ScriptException("Error while invoking method.", ex);
            }
            finally
            {
                _logger.Info("Finished script <{0}>.", entry);
                IsRunning = false;
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<EntryPoint> GetEnumerator()
        {
            return _entryPoints.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}