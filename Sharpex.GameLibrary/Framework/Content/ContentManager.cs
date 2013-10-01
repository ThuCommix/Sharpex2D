using System;
using System.IO;
using SharpexGL.Framework.Common.FileSystem;
using SharpexGL.Framework.Components;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Media.Sound;
using SharpexGL.Framework.Rendering;
using SharpexGL.Framework.Rendering.Font;
using SharpexGL.Framework.Rendering.Sprites;

namespace SharpexGL.Framework.Content
{
    public class ContentManager : IConstructable
    {
        private string _internalContentPath = "";
        /// <summary>
        /// Sets or gets the base ContentPath.
        /// </summary>
        public string ContentPath
        {
            get
            {
                return _internalContentPath;
            }
            set
            {
                _internalContentPath = value;
                Construct();
            }
        }
        /// <summary>
        /// Sets or gets the FileSystem.
        /// </summary>
        public IFileSystem FileSystem { set; get; }
        /// <summary>
        /// Initializes a new ContentManager.
        /// </summary>
        public ContentManager()
        {
            FileSystem = new Win32FileSystem();
            ContentPath = FileSystem.ConnectPath(Environment.CurrentDirectory, "Content");
        }
        /// <summary>
        /// Destructs the ContentManager.
        /// </summary>
        ~ContentManager()
        {
            FileSystem = null;
        }

        #region IConstructable Implementation

        /// <summary>
        /// Initializes the ContentManager.
        /// </summary>
        public void Construct()
        {
            if (ContentPath == "")
            {
                ContentPath = FileSystem.ConnectPath(Environment.CurrentDirectory, "Content");
            }
            if (!FileSystem.DirectoryExists(ContentPath))
            {
                FileSystem.CreateDirectory(ContentPath);
            }
        }

        #endregion

        #region Member
        /// <summary>
        /// Loads an asset into SGL.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="asset">The Asset.</param>
        /// <returns></returns>
        public T Load<T>(string asset)
        {
            //texture
            if (typeof (T) == typeof (Texture))
            {
                using (var fileStream = FileSystem.Open(FileSystem.ConnectPath(ContentPath, asset)))
                {
                    return (T)(Object)SGL.Implementations.Get<TextureSerializer>().Read(new BinaryReader(fileStream));
                }
            }
            //spritefont
            if (typeof(T) == typeof(SpriteFont))
            {
                using (var fileStream = FileSystem.Open(FileSystem.ConnectPath(ContentPath, asset)))
                {
                    return (T)(Object)SGL.Implementations.Get<SpriteFontSerializer>().Read(new BinaryReader(fileStream));
                }
            }
            //spritesheet
            if (typeof (T) == typeof (SpriteSheet))
            {
                using (var fileStream = FileSystem.Open(FileSystem.ConnectPath(ContentPath, asset)))
                {
                    var spriteSheet = SGL.Implementations.Get<SpriteSheetSerializer>().Read(new BinaryReader(fileStream));
                    return (T)(Object)spriteSheet;
                }
            }
            //animation
            if (typeof (T) == typeof (Animation))
            {
                using (var fileStream = FileSystem.Open(FileSystem.ConnectPath(ContentPath, asset)))
                {
                    var animation = SGL.Implementations.Get<AnimationSerializer>().Read(new BinaryReader(fileStream));
                    return (T)(Object)animation;
                }               
            }
            //sound
            if (typeof(T) == typeof(Sound))
            {
                return (T) (Object) Sound.Factory.Create(FileSystem.ConnectPath(ContentPath, asset));
            }
            throw new InvalidOperationException(typeof (T).FullName + " could not be loaded.");
        }

        #endregion
    }
}
