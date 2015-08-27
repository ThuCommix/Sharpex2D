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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ContentPipeline.Exporters;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Content;

namespace ContentPipeline.Actions
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class CompileAction : IAction
    {
        /// <summary>
        /// Gets the option name.
        /// </summary>
        public string Option { get { return "--compile"; } }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="args">The Arguments.</param>
        /// <returns>Application ExitCode.</returns>
        public int Execute(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            if (args.Length < 2)
            {
                Console.WriteLine(
                    "Invalid number of arguments. Please specify 1) the input directory, 2) the output directory.");
                return -1;
            }

            var inputDirectory = new DirectoryInfo(args[0]);
            var outputDirectory = new DirectoryInfo(args[1]);

            if (!inputDirectory.Exists)
            {
                Console.WriteLine("The input directory does not exist.");
                return -1;
            }

            if (!outputDirectory.Exists)
            {
                try
                {
                    outputDirectory.Create();
                }
                catch
                {
                    Console.WriteLine("Unable to create output directory.");
                    return -1;
                }
            }

            Console.WriteLine("Resolving exporters ...");

            var exporters = new Dictionary<string, Exporter>();
            var assemblyList = new List<Assembly>();

            assemblyList.Add(Assembly.GetExecutingAssembly());

            for (int i = 2; i < args.Length; i++)
            {
                if (!File.Exists(args[i]) || !args[i].EndsWith(".dll"))
                    continue;

                try
                {
                    assemblyList.Add(Assembly.LoadFrom(args[i]));
                }
                catch
                {
                    Console.WriteLine("-- Error while resolving {0}", args[i]);
                }
            }

            foreach (var assembly in assemblyList)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType == typeof(Exporter))
                    {
                        ExportContentAttribute attribute;
                        if (AttributeHelper.TryGetAttribute(type, out attribute))
                        {
                            try
                            {
                                var exporter = (Exporter)Activator.CreateInstance(type);
                                foreach (var extension in exporter.FileFilter)
                                {
                                    if (exporters.ContainsKey(extension))
                                    {
                                        exporters[extension] = exporter;
                                        Console.WriteLine("-- Overwrite {0} for {1}", type.Name, extension);
                                    }
                                    else
                                    {
                                        exporters.Add(extension, exporter);
                                        //Console.WriteLine("-- Registered {0} for {1}", type.Name, extension);
                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("-- Error while instantiating {0}", type.FullName);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("-- Resolved {0} exporters", exporters.Count);
            Console.WriteLine("-- Supported file types:");
            foreach (var keypair in exporters)
            {
                Console.WriteLine("-- " + keypair.Key);
            }

            Console.WriteLine("Preparing ...");

            try
            {
                string[] cleanFiles = Directory.GetFiles(outputDirectory.FullName, "*", SearchOption.AllDirectories);

                foreach (string file in cleanFiles)
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }

                string[] cleanDirectories = Directory.GetDirectories(outputDirectory.FullName, "*",
                    SearchOption.AllDirectories);

                foreach (string directory in cleanDirectories)
                {
                    if (Directory.Exists(directory))
                        Directory.Delete(directory);
                }
            }
            catch
            {

            }


            string[] files = Directory.GetFiles(inputDirectory.FullName, "*", SearchOption.AllDirectories);

            string[] directories = Directory.GetDirectories(inputDirectory.FullName, "*", SearchOption.AllDirectories);

            foreach (string directory in directories)
            {
                if (!Directory.Exists(directory.Replace(inputDirectory.FullName, outputDirectory.FullName)))
                {
                    try
                    {
                        Directory.CreateDirectory(directory.Replace(inputDirectory.FullName, outputDirectory.FullName));
                    }
                    catch
                    {
                        Console.WriteLine("Unable to create directory {0}",
                            directory.Replace(inputDirectory.FullName, outputDirectory.FullName));
                        return -1;
                    }
                }
            }

            int compiled = 0;
            int skipped = 0;
            int errors = 0;

            Console.WriteLine("Exporting files ...");

            Parallel.ForEach(files, file =>
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                {
                    if (exporters.ContainsKey(fileInfo.Extension))
                    {
                        var memoryStream = new MemoryStream();

                        try
                        {
                            var exporter = exporters[fileInfo.Extension.ToLower()];
                            var metaInformations = exporter.OnCreate(file, memoryStream);
                            var xcf = new ExtensibleContentFormat(AttributeHelper.GetAttribute<ExportContentAttribute>(exporter).Type);
                            xcf.AddMetaInfos(metaInformations);
                            xcf.Save(
                                file.Replace(inputDirectory.FullName, outputDirectory.FullName)
                                    .Replace(fileInfo.Extension, ".xcf"), memoryStream.ToArray());

                            Console.WriteLine("Compiled {0}", fileInfo.Name);
                            compiled++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error {0} a compile error occured", e.StackTrace);
                            errors++;
                        }

                        memoryStream.Dispose();
                    }
                    else
                    {
                        if (File.Exists(file.Replace(inputDirectory.FullName, outputDirectory.FullName)))
                            File.Delete(file.Replace(inputDirectory.FullName, outputDirectory.FullName));
                        File.Copy(file, file.Replace(inputDirectory.FullName, outputDirectory.FullName));
                        Console.WriteLine("Copy {0}", fileInfo.Name);
                        skipped++;
                    }
                }
                else
                {
                    Console.WriteLine("Skipping {0} because the file does not longer exists", file);
                    skipped++;
                }
            });

            sw.Stop();

            Console.WriteLine("========== Compiled {0} files, Skipped {1} files, Errors {2}, Time {3} ==========",
                compiled,
                skipped, errors, sw.Elapsed.ToString("hh':'mm':'ss':'fff"));
            return errors == 0 ? 0 : -1;
        }
    }
}
