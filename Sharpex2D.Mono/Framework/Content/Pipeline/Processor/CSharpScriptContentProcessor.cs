using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Scripting.CSharp;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class CSharpScriptContentProcessor : ContentProcessor<CSharpScript>
    {
        /// <summary>
        ///     Initializes a new CSharpScriptContentProcessor class.
        /// </summary>
        public CSharpScriptContentProcessor()
            : base(new Guid("FA126C12-8DB1-424F-94CE-571C7A641BBD"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>CSharpScript.</returns>
        public override CSharpScript ReadData(string filepath)
        {
            if (!filepath.EndsWith(".s2d"))
            {
                throw new FormatException("Specified file is not in *.s2d format.");
            }

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                try
                {
                    var csharpScript = new CSharpScript
                    {
                        Content = binaryreader.ReadString()
                    };

                    return csharpScript;
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }

        /// <summary>
        ///     Writes the data.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="destinationpath">The DestinationPath.</param>
        public override void WriteData(CSharpScript data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var binaryWriter = new BinaryWriter(fileStream);
                    binaryWriter.Write(data.Content);
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}