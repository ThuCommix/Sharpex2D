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

namespace Sharpex2D.Scripting
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal class ScriptStorageBuffer
    {
        private readonly Dictionary<Guid, Assembly> _list;

        /// <summary>
        /// Initializes a new ScriptStorageBuffer class.
        /// </summary>
        public ScriptStorageBuffer()
        {
            _list = new Dictionary<Guid, Assembly>();
        }

        /// <summary>
        /// Sets or gets the CompiledScript.
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
        /// Adds a new compiled script to the storage.
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
        /// Removes a compiled script.
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
        /// Updates a already existing compiled script.
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
        /// A value indicating whether the compiled script exists.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>True if the compiled script is available.</returns>
        public bool Exists(Guid guid)
        {
            return _list.ContainsKey(guid);
        }
    }
}