using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.Shell;
using VSIntegration.Commands;

namespace VSIntegration
{
    public class PackageBase : Package
    {
        /// <summary>
        /// Gets the service
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>T</returns>
        public T GetService<T>()
        {
            return (T) base.GetService(typeof (T));
        }

        /// <summary>
        /// Gets the global service
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>T</returns>
        public static T GetGlobalService<T>()
        {
            return (T) GetGlobalService(typeof (T));
        }

        /// <summary>
        /// Gets the service as the second specified type
        /// </summary>
        /// <typeparam name="T1">The service type</typeparam>
        /// <typeparam name="T2">The return type</typeparam>
        /// <returns>T2</returns>
        public T2 GetServiceAs<T1, T2>() where T2 : class
        {
            return base.GetService(typeof(T1)) as T2;
        }

        /// <summary>
        /// Gets the global service as the second specified type
        /// </summary>
        /// <typeparam name="T1">The service type</typeparam>
        /// <typeparam name="T2">The return type</typeparam>
        /// <returns>T2</returns>
        public static T2 GetGlobalServiceAs<T1, T2>() where T2 : class
        {
            return GetGlobalService(typeof (T1)) as T2;
        }

        /// <summary>
        /// Initializes the package
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            var commands =
                typeof (CommandBase).Assembly.GetTypes()
                    .Where(x => typeof (CommandBase).IsAssignableFrom(x) && !x.IsAbstract)
                    .ToArray();

            foreach (var command in commands)
            {
                try
                {
                    Activator.CreateInstance(command, this);
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to initialize {command.Name}.");
                }
            }
        }
    }
}
