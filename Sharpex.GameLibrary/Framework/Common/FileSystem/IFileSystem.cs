using System.IO;

namespace SharpexGL.Framework.Common.FileSystem
{
    public interface IFileSystem
    {
        /// <summary>
        /// Opens an existing file.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>FileStream</returns>
        FileStream Open(string file);
        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="file">The File.</param>
        /// <returns>FileStream</returns>
        FileStream Create(string file);
        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        void Delete(string file);
        /// <summary>
        /// Creates a Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        void CreateDirectory(string directoryPath);
        /// <summary>
        /// Deletes a Directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        void DeleteDirectory(string directoryPath);
        /// <summary>
        /// Determines, if a file exists.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>True if exists</returns>
        bool FileExists(string file);
        /// <summary>
        /// Determines, if a directory exists.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>True if exists</returns>
        bool DirectoryExists(string directoryPath);
        /// <summary>
        /// Gets all files in the given directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>String[] FileCollection</returns>
        string[] GetFiles(string directoryPath);
        /// <summary>
        /// Gets all Directories in the given directory.
        /// </summary>
        /// <param name="directoryPath">The DirectoryPath.</param>
        /// <returns>String[] DirectoryCollection</returns>
        string[] GetDirectories(string directoryPath);
        /// <summary>
        /// Connects the given FilePaths.
        /// </summary>
        /// <param name="fileparts">The FilePaths.</param>
        /// <returns>PathString</returns>
        string ConnectPath(params string[] fileparts);
    }
}
