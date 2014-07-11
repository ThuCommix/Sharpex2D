// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Sharpex2D.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ScriptHost<T> : IComponent where T : IScript
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("076AE512-8E9C-44B9-BB91-CF8289BCEDC1"); }
        }

        #endregion

        /// <summary>
        ///     ScriptEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void ScriptEventHandler(object sender, ScriptEventArgs e);

        private readonly IScriptEvaluator<T> _evaluator;
        private readonly Dictionary<string, MethodInfo> _methods;

        /// <summary>
        ///     Initializes a new ScriptHost class.
        /// </summary>
        /// <param name="evaluator">The ScriptEvaluator.</param>
        public ScriptHost(IScriptEvaluator<T> evaluator)
        {
            _methods = new Dictionary<string, MethodInfo>();
            _evaluator = evaluator;
            SGL.Components.Add(this);
        }

        /// <summary>
        ///     ScriptCompleted event.
        /// </summary>
        public event ScriptEventHandler ScriptCompleted;

        /// <summary>
        ///     ScriptRunning event.
        /// </summary>
        public event ScriptEventHandler ScriptRunning;

        /// <summary>
        ///     Executes the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        public void Execute(T script, params object[] objects)
        {
            script.IsActive = true;

            if (ScriptRunning != null)
            {
                ScriptRunning(this, new ScriptEventArgs(script.Guid));
            }

            Task.Factory.StartNew(() => InternalExecute(script, objects));
        }

        /// <summary>
        ///     Internal Execute.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        private void InternalExecute(T script, params object[] objects)
        {
            _evaluator.Evaluate(script, objects);
            script.IsActive = false;

            if (ScriptCompleted != null)
            {
                ScriptCompleted(this, new ScriptEventArgs(script.Guid));
            }
        }

        /// <summary>
        ///     Adds a method to the list.
        /// </summary>
        /// <param name="key">The MethodName.</param>
        /// <param name="methodInfo">The MethodInfo.</param>
        public void AddMethod(string key, MethodInfo methodInfo)
        {
            if (_methods.ContainsKey(key))
            {
                throw new ArgumentException("The key already exist.");
            }

            _methods.Add(key, methodInfo);
        }

        /// <summary>
        ///     Removes a method from the list.
        /// </summary>
        /// <param name="key">The MethodName.</param>
        public void RemoveMethod(string key)
        {
            if (!_methods.ContainsKey(key))
            {
                throw new ArgumentException("The key does not exist.");
            }

            _methods.Remove(key);
        }

        /// <summary>
        ///     Invokes a method.
        /// </summary>
        /// <param name="key">The MethodName.</param>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>Object</returns>
        public object Invoke(string key, object[] parameters)
        {
            if (!_methods.ContainsKey(key))
            {
                throw new ArgumentException("The key does not exist.");
            }

            try
            {
                //Invoke method
                return _methods[key].Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                //Something badly went wrong
                throw new InvalidOperationException("Error while trying to invoke the method " + _methods[key].Name +
                                                    Environment.NewLine + ex.Message);
            }
        }
    }
}