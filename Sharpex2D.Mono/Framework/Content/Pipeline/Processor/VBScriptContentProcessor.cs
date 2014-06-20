using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Scripting.VB;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class VBScriptContentProcessor : ContentProcessor<VBScript>
    {
        /// <summary>
        ///     Initializes a new CSharpScriptContentProcessor class.
        /// </summary>
        public VBScriptContentProcessor()
            : base(new Guid("81352726-E45A-4DF1-A8A9-8798BAA88D09"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>CSharpScript.</returns>
        public override VBScript ReadData(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                try
                {
                    var vbScript = new VBScript
                    {
                        Content = binaryreader.ReadString()
                    };

                    return vbScript;
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
        public override void WriteData(VBScript data, string destinationpath)
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