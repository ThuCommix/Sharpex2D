using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Scripting.Events;

namespace SharpexGL.Framework.Scripting
{
    public class ScriptProvider : IScriptProvider
    {
        #region IScriptProvider Implementation
        /// <summary>
        /// Adds a new Method to the script.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="linkedMethod">The LinkedMethod.</param>
        public void AddMethod(string name, Action linkedMethod)
        {
            _methods.Add(name, linkedMethod);
        }
        /// <summary>
        /// Removes a Method from the script.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        public void RemoveMethod(string name)
        {
            if (_methods.ContainsKey(name))
            {
                _methods.Remove(name);
            }
        }
        /// <summary>
        /// Runs the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        public void Run(IScript script)
        {
            Task.Factory.StartNew(() =>
            {
                script.IsActive = true;
                SGL.Components.Get<EventManager>().Publish(new ScriptRunningEvent(script.Guid));
                _scriptInterpreter.Evaluate(script);
                SGL.Components.Get<EventManager>().Publish(new ScriptCompletedEvent(script.Guid));
                script.IsActive = false;
            });
        }
        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="parameter">The Parameters.</param>
        public void Call(string name, params object[] parameter)
        {
            if (_methods.ContainsKey(name))
            {
                Action action;
                if (_methods.TryGetValue(name, out action))
                {
                    action.DynamicInvoke(parameter);
                }

                throw new InvalidOperationException(name + " does not exist.");
            }
            throw new InvalidOperationException(name + " does not exist.");
        }

        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="parameter">The Parameters.</param>
        /// <param name="callback">The return object.</param>
        public void Call(string name, out object callback, params object[] parameter)
        {
            if (_methods.ContainsKey(name))
            {
                Action action;
                if (_methods.TryGetValue(name, out action))
                {
                    callback = action.DynamicInvoke(parameter);
                    return;
                }

                throw new InvalidOperationException(name + " does not exist.");
            }
            throw new InvalidOperationException(name + " does not exist.");
        }

        #endregion

        private readonly Dictionary<string, Action> _methods;
        private readonly ScriptInterpreter _scriptInterpreter;

        /// <summary>
        /// Initializes a new ScriptProvider class.
        /// </summary>
        public ScriptProvider(ScriptInterpreter scriptInterpreter)
        {
            _methods = new Dictionary<string, Action>();
            _scriptInterpreter = scriptInterpreter;
        }
    }
}
