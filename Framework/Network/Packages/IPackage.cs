using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpexGL.Framework.Network.Packages
{
    public interface IPackage<T>
    {
        /// <summary>
        /// Gets the Package Data.
        /// </summary>
        byte[] PackageData { get; }
        /// <summary>
        /// Gets the Identifer of the Package.
        /// </summary>
        int Identifer {  get; }
        /// <summary>
        /// Serializes a TClass into a package.
        /// </summary>
        /// <param name="instance">TClass</param>
        void Serialize(T instance);
        /// <summary>
        /// Deserializes the PackageData into TClass.
        /// </summary>
        /// <returns>TClass</returns>
        T Deserialize();

    }
}
