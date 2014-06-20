using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sharpex2D.Framework.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    internal class ScriptStorageBuffer
    {
        private readonly Dictionary<Guid, Assembly> _list;

        /// <summary>
        ///     Initializes a new ScriptStorageBuffer class.
        /// </summary>
        public ScriptStorageBuffer()
        {
            _list = new Dictionary<Guid, Assembly>();
        }

        /// <summary>
        ///     Sets or gets the CompiledScript.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>Assembly.</returns>
        public Assembly this[Guid guid]
        {
            get
            {
                if (!_list.ContainsKey(guid))
                {
                    throw new InvalidOperationException("The guid does not exist.");
                }

                return _list[guid];
            }
            set { Update(guid, value); }
        }

        /// <summary>
        ///     Adds a new compiled script to the storage.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="compiledScript">The CompiledScript.</param>
        public void Add(Guid guid, Assembly compiledScript)
        {
            if (_list.ContainsKey(guid))
            {
                throw new InvalidOperationException("The guid already exist.");
            }

            _list.Add(guid, compiledScript);
        }

        /// <summary>
        ///     Removes a compiled script.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        public void Remove(Guid guid)
        {
            if (!_list.ContainsKey(guid))
            {
                throw new InvalidOperationException("The guid does not exist.");
            }

            _list.Remove(guid);
        }

        /// <summary>
        ///     Updates a already existing compiled script.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="compiledScript">The CompiledScript.</param>
        public void Update(Guid guid, Assembly compiledScript)
        {
            if (!_list.ContainsKey(guid))
            {
                throw new InvalidOperationException("The guid does not exist.");
            }

            _list[guid] = compiledScript;
        }

        /// <summary>
        ///     A value indicating whether the compiled script exists.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>True if the compiled script is available.</returns>
        public bool Exists(Guid guid)
        {
            return _list.ContainsKey(guid);
        }
    }
}