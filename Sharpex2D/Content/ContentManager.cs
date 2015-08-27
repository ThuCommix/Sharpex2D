// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Sharpex2D.Framework.Content.Importers;
using Sharpex2D.Framework.Logging;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ContentManager : IComponent
    {
        private string _rootPath;
        private readonly Dictionary<Type, Importer> _importers;
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new ContentManager.
        /// </summary>
        public ContentManager()
        {
            _logger = LogManager.GetClassLogger();
            _importers = new Dictionary<Type, Importer>();

            AddImporterFromAssembly(GetType().Assembly);
            AddImporterFromAssembly(Assembly.GetExecutingAssembly());

        }

        /// <summary>
        /// Sets or gets the base ContentPath.
        /// </summary>
        public string RootPath
        {
            get { return _rootPath; }
            set
            {
                _rootPath = value;


                if (!Directory.Exists(RootPath))
                {
                    Directory.CreateDirectory(RootPath);
                }
            }
        }

        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0DD94218-396E-4EBA-9B3C-EAD05420A375"); }
        }

        #endregion

        /// <summary>
        /// Adds all Importer objects from the specified assembly.
        /// </summary>
        /// <param name="assembly">The Assembly.</param>
        /// <param name="allowOverwrite">Indicates whether importer can be overwritten.</param>
        /// <remarks>Allowing overwrite is dangerous.</remarks>
        public void AddImporterFromAssembly(Assembly assembly, bool allowOverwrite = false)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.BaseType == typeof(Importer))
                {
                    ImportContentAttribute attribute;
                    if (AttributeHelper.TryGetAttribute(type, out attribute))
                    {
                        if (!_importers.ContainsKey(attribute.Type) || allowOverwrite)
                        {
                            try
                            {
                                var importer = (Importer) Activator.CreateInstance(type);
                                if (_importers.ContainsKey(attribute.Type))
                                {
                                    _importers[attribute.Type] = importer;
                                    _logger.Info("Overwriting {0} for type {1}", type.Name, attribute.Type);
                                }
                                else
                                {
                                    _importers.Add(attribute.Type, importer);
                                    _logger.Info("Registered {0} for type {1}", type.Name, attribute.Type);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new ContentLoadException("Error while instantiating a content importer.", ex);
                            }
                        }
                    }
                    else
                    {
                        _logger.Info("Overwriting is not allowed for type {0}.", attribute.Type);
                    }
                }
            }
        }

        /// <summary>
        /// Loads an asset.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <returns>TContent.</returns>
        public T Load<T>(string asset) where T : IContent
        {
            if (_importers.All(x => x.Key != typeof (T)))
            {
                throw new ContentLoadException(string.Format("No importer specified for the given type ({0})",
                    typeof (T).Name));
            }

            var importer = _importers.First(x => x.Key == typeof (T)).Value;
            return (T)importer.LoadXcf(SolveFileLocation(asset));
        }

        /// <summary>
        /// Loads an asset and checks the hash.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <param name="expectedHash">The expected Hash.</param>
        /// <param name="algorithm"></param>
        /// <returns>TContent.</returns>
        public T Load<T>(string asset, string expectedHash, HashAlgorithm algorithm) where T : IContent
        {
            string filepath = SolveFileLocation(asset);

            string hash = BitConverter.ToString(algorithm.ComputeHash(File.ReadAllBytes(filepath)));
            if (hash.ToLower().Replace("-", "") != expectedHash.ToLower().Replace("-", ""))
            {
                throw new ContentLoadException(
                    string.Format("The computed hash for {0} does not match the expected hash.", filepath));
            }

            return Load<T>(asset);
        }

        /// <summary>
        /// Solves the file location.
        /// </summary>
        /// <param name="asset">The Asset.</param>
        /// <returns>Returns the absolute file location of the specified asset.</returns>
        private string SolveFileLocation(string asset)
        {
            //make the path valid if not
            asset = asset.Replace("/", @"\");

            string filepath = Path.Combine(RootPath, asset);

            if (!File.Exists(Path.Combine(RootPath, asset)))
            {
                //May a reason could be that no extension was provided. Search
                //in the directory specified by the asset for files with the same name
                //and return the first matching one else we tried our best.

                string directory = filepath.Substring(0, filepath.LastIndexOf(@"\", StringComparison.Ordinal));

                if (Directory.Exists(directory))
                {
                    string pattern = filepath.Substring(filepath.LastIndexOf(@"\", StringComparison.Ordinal),
                        filepath.Length - filepath.LastIndexOf(@"\", StringComparison.Ordinal)).Replace(@"\", "");

                    try
                    {
                        return Directory.GetFiles(directory, pattern + ".*", SearchOption.TopDirectoryOnly).First();
                    }
                    catch
                    {
                        // throw new ContentLoadException("Asset not found, I really tried hard </3.");
                        throw new ContentLoadException("Asset not found.");
                    }
                }

                throw new ContentLoadException("Asset not found.");
            }

            return filepath;
        }
    }
}