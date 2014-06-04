using System;
using System.Security;

namespace Sharpex2D.Framework.Common.Security
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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