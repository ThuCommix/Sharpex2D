using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SharpexGL.Framework.Components;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Scripting.Events;

namespace SharpexGL.Framework.Scripting
{
    public class ScriptHost : IComponent
    {

        private readonly Dictionary<string, MethodInfo> _methods;
        private readonly IScriptEvaluator _evaluator;

        /// <summary>
        /// Initializes a new ScriptHost class.
        /// </summary>
        /// <param name="evaluator">The ScriptEvaluator.</param>
        public ScriptHost(IScriptEvaluator evaluator)
        {
            _methods = new Dictionary<string, MethodInfo>();
            _evaluator = evaluator;
            SGL.Components.AddComponent(this);
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        public void Execute(IScript script)
        {
            script.IsActive = true;
            SGL.Components.Get<EventManager>().Publish(new ScriptRunningEvent(script.Guid));
            Task.Factory.StartNew(() => _evaluator.Evaluate(script.Content));
            script.IsActive = false;
            SGL.Components.Get<EventManager>().Publish(new ScriptCompletedEvent(script.Guid));
        }

        /// <summary>
        /// Adds a method to the list.
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
        /// Removes a method from the list.
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
        /// Invokes a method.
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
