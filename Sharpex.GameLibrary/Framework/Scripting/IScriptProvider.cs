using System;

namespace SharpexGL.Framework.Scripting
{
    public interface IScriptProvider
    {
        /// <summary>
        /// Adds a new Method to the script.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="linkedMethod">The LinkedMethod.</param>
        void AddMethod(string name, Action linkedMethod);
        /// <summary>
        /// Removes a Method from the script.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        void RemoveMethod(string name);
        /// <summary>
        /// Runs the script.
        /// </summary>
        /// <param name="script">The Script.</param>
        void Run(IScript script);
        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="parameter">The Parameters.</param>
        void Call(string name, params object[] parameter);
        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="name">The MethodName.</param>
        /// <param name="parameter">The Parameters.</param>
        /// <param name="callback">The return object.</param>
        void Call(string name, out object callback, params object[] parameter);
    }
}
