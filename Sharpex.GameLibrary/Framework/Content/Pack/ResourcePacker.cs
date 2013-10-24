using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

namespace SharpexGL.Framework.Content.Pack
{
    public class ResourcePacker
    {
        #region Public
        /// <summary>
        /// Packs all resources into a single file.
        /// </summary>
        /// <param name="sources">The FilePaths.</param>
        /// <param name="destination">The Destination.</param>
        public void Pack(string[] sources, string destination)
        {
            var threadPack = new Thread(() =>
            {
                //packing code.
                var filesToProcess = sources.Length;
                var currentProcessed = 0;
                var gzipStream = new GZipStream(new FileStream(destination + ".respack", FileMode.Create, FileAccess.ReadWrite),
                    CompressionMode.Compress);
                var streamWriter = new StreamWriter(gzipStream);
                streamWriter.WriteLine(GetResourceHeader(sources.Length));

                foreach (var file in sources)
                {
                    if (!File.Exists(file))
                    {
                        throw new FileNotFoundException("Could not found " + file);
                    }

                    streamWriter.WriteLine(GetFileHeader(file));
                    streamWriter.WriteLine(Convert.ToBase64String(File.ReadAllBytes(file)));
                    currentProcessed++;
                    var percentage = 100*currentProcessed/filesToProcess;

                    if (ProgressChanged == null) continue;
                    foreach (ProgressChangedEventHandler del in ProgressChanged.GetInvocationList())
                    {
                        if (del.Target is Control && ((Control)del.Target).InvokeRequired) 
                            ((Control)del.Target).Invoke(del, this, new ProgressChangedEventArgs(percentage));
                        else
                            del(this, new ProgressChangedEventArgs(percentage));
                    }
                }

                streamWriter.Close();

                if (ProgressCompleted == null) return;
                foreach (ProgressCompletedEventHandler del in ProgressCompleted.GetInvocationList())
                {
                    if (del.Target is Control && ((Control)del.Target).InvokeRequired) 
                        ((Control)del.Target).Invoke(del, this, new EventArgs());
                    else
                        del(this, new EventArgs());
                }
            }) {IsBackground = true};
            threadPack.Start();
        }
        /// <summary>
        /// Unpacks the ResourcePack.
        /// </summary>
        /// <param name="packagePath">The Path of the ResourcePackage.</param>
        /// <param name="destination">The Destination.</param>
        public void Unpack(string packagePath, string destination)
        {
            var threadUnpack = new Thread(() =>
            {
                //unpacking code
                var gzipStream = new GZipStream(new FileStream(packagePath, FileMode.Open, FileAccess.Read),
                    CompressionMode.Decompress);
                var streamReader = new StreamReader(gzipStream);
                var header = streamReader.ReadLine();
                AnalyzeHeader(header);
                var fileCount = Convert.ToInt32(header.Split(',')[3].Replace(" Files=", "").Trim());
                var filesProcessed = 0;
                while (!streamReader.EndOfStream)
                {
                    var filetitle = AnalyzeFileHeader(streamReader.ReadLine());
                    var file = Convert.FromBase64String(streamReader.ReadLine());
                    File.WriteAllBytes(Path.Combine(destination, filetitle), file);
                    filesProcessed++;

                    var percentage = 100*filesProcessed/fileCount;

                    if (ProgressChanged == null) continue;
                    foreach (ProgressChangedEventHandler del in ProgressChanged.GetInvocationList())
                    {
                        if (del.Target is Control && ((Control)del.Target).InvokeRequired)
                            ((Control)del.Target).Invoke(del, this, new ProgressChangedEventArgs(percentage));
                        else
                            del(this, new ProgressChangedEventArgs(percentage));
                    }
                }

                streamReader.Close();

                if (ProgressCompleted == null) return;
                foreach (ProgressCompletedEventHandler del in ProgressCompleted.GetInvocationList())
                {
                    if (del.Target is Control && ((Control)del.Target).InvokeRequired)
                        ((Control)del.Target).Invoke(del, this, new EventArgs());
                    else
                        del(this, new EventArgs());
                }
            }) {IsBackground = true};
            threadUnpack.Start();
        }
        /// <summary>
        /// The ProgressChangedEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
        /// <summary>
        /// The ProgressCompletedEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void ProgressCompletedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered, if the current progress changed.
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;
        /// <summary>
        /// Triggered, if the current progress is finished.
        /// </summary>
        public event ProgressCompletedEventHandler ProgressCompleted;

        /// <summary>
        /// Gets the FileStream of a file located in the ResourcePackage.
        /// </summary>
        /// <param name="packagePath">The PackagePath.</param>
        /// <param name="filename">The Filename.</param>
        /// <returns>Stream</returns>
        public Stream GetFileStreamByName(string packagePath, string filename)
        {
            //open package and have a look on the header

            var gzipStream = new GZipStream(new FileStream(packagePath, FileMode.Open, FileAccess.Read),
                CompressionMode.Decompress);
            var streamReader = new StreamReader(gzipStream);
            var header = streamReader.ReadLine();
            AnalyzeHeader(header);

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line.StartsWith("[Filename="))
                {
                    var packfileName = AnalyzeFileHeader(line);
                    if (packfileName == filename)
                    {
                        return new MemoryStream(Convert.FromBase64String(streamReader.ReadLine()));
                    }
                }
            }

            throw new InvalidOperationException("The file " + filename + " could not be found.");
        }

        #endregion

        #region Internal

        private const string PackerVersion = "1.1521.2363";

        /// <summary>
        /// Returns the ResourceHeader.
        /// </summary>
        /// <param name="fileCount">The FileCount.</param>
        /// <returns>String</returns>
        private string GetResourceHeader(int fileCount)
        {
            return "[" + "PackerVersion=" + PackerVersion + ", Lib=" + SGL.Version + ", PackDate=" +
                   DateTime.Today.ToShortDateString() + ", Files=" + fileCount +"]";
        }
        /// <summary>
        /// Returns a FileHeader for the given file.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>String</returns>
        private string GetFileHeader(string file)
        {
            var fInfo = new FileInfo(file);
            return "[Filename=" + fInfo.Name + fInfo.Extension + ", Size=" + fInfo.Length + "]";
        }

        /// <summary>
        /// Analyzes the header.
        /// </summary>
        /// <param name="header">The Header.</param>
        private void AnalyzeHeader(string header)
        {
            if (header.Split(',').Length == 4)
            {
                var packversion = Convert.ToInt32(header.Split(',')[0].Replace("[PackerVersion", "").Trim().Replace(".", ""));
                if (Convert.ToInt32(PackerVersion.Replace(".", "")) != packversion)
                {
                    throw new InvalidOperationException("The PackerVersion does not match. (Current:" + PackerVersion +
                                                        ", ResourcePackage:" +
                                                        header.Split(',')[0].Replace("[PackerVersion", "").Trim());
                }

                return;
            }

            throw new InvalidOperationException("The loaded ResourcePackage is invalid/corrupted.");
        }

        /// <summary>
        /// Analyzes the FileHeader and returns the filename.
        /// </summary>
        /// <param name="fileHeader">The FileHeader.</param>
        /// <returns>string</returns>
        private string AnalyzeFileHeader(string fileHeader)
        {
            if (fileHeader.Split(',').Length == 2)
            {
                return fileHeader.Split(',')[0].Replace("[Filename=", "").Trim();
            }

            throw new InvalidOperationException("The loaded ResourcePackage is icorrupted.");
        }

        #endregion
    }
}
