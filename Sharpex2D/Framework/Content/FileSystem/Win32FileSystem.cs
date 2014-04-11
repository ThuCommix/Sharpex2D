using System.IO;

namespace Sharpex2D.Framework.Content.FileSystem
{
    public class Win32FileSystem : IFileSystem
    {
        /// <summary>
        /// Opens an existing file.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>FileStream</returns>
        public FileStream Open(string file)
        {
            return new FileStream(file, FileMode.Open, FileAccess.ReadWrite);
        }
        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>FileStream</returns>
        public FileStream Create(string file)
        {
            return new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
        }
        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        public void Delete(string file)
        {
            File.Delete(file);
        }
        /// <summary>
        /// Creates a Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
        /// <summary>
        /// Deletes a Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        public void DeleteDirectory(string directoryPath)
        {
            Directory.Delete(directoryPath);
        }
        /// <summary>
        /// Determines, if a file exists.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>True if exists</returns>
        public bool FileExists(string file)
        {
            return File.Exists(file);
        }
        /// <summary>
        /// Determines, if a directory exists.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>True if exists</returns>
        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        /// <summary>
        /// Gets all files in the given directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>String[] FileCollection</returns>
        public string[] GetFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly);
        }
        /// <summary>
        /// Gets all Directories in the given directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>String[] DirectoryCollection</returns>
        public string[] GetDirectories(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);
        }
        /// <summary>
        /// Connects the given FilePaths.
        /// </summary>
        /// <param name="fileparts">The FilePaths.</param>
        /// <returns>PathString</returns>
        public string ConnectPath(params string[] fileparts)
        {
            var result = "";
            if (fileparts.Length == 1) return fileparts[0];
            for (var i = 0; i <= fileparts.Length - 1; i++)
            {
                result = Path.Combine(result, fileparts[i]);
            }
            return result;
        }
    }
}
