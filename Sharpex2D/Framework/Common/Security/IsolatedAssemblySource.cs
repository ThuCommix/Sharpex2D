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
using System.Security;

namespace Sharpex2D.Framework.Common.Security
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class IsolatedAssemblySource<T> : IDisposable where T : ProxySource
    {
        private AppDomain _appDomain;
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new IsolatedAssemblySource class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="assemblyPath">The AssemblyPath.</param>
        public IsolatedAssemblySource(Guid guid, string assemblyPath) : this(guid.ToString(), assemblyPath)
        {
        }

        /// <summary>
        ///     Initializes a new IsolatedAssemblySource class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="assemblyPath">The AssemblyPath.</param>
        public IsolatedAssemblySource(string name, string assemblyPath)
        {
            _appDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence,
                AppDomain.CurrentDomain.SetupInformation);

            Instance = (T) _appDomain.CreateInstanceFromAndUnwrap(assemblyPath, typeof (T).FullName);
        }

        /// <summary>
        ///     Initializes a new IsolatedAssemblySource class.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="assemblyPath">The AssemblyPath.</param>
        /// <param name="permSet">The PermissionSet.</param>
        public IsolatedAssemblySource(string name, string assemblyPath, PermissionSet permSet)
        {
            _appDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence,
                AppDomain.CurrentDomain.SetupInformation, permSet);

            Instance = (T) _appDomain.CreateInstanceFromAndUnwrap(assemblyPath, typeof (T).FullName);
        }

        /// <summary>
        ///     Gets the Instance.
        /// </summary>
        public T Instance { private set; get; }

        /// <summary>
        ///     Disposes the Object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    if (_appDomain != null)
                    {
                        AppDomain.Unload(_appDomain);
                        _appDomain = null;
                    }
                }
            }
        }
    }
}