using System;
using System.IO;
using System.Runtime.InteropServices;
using Sharpex2D.Framework.Game.Services;

namespace Sharpex2D.Framework.Content.Pipeline.Processor
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public class GamerContentProcessor : ContentProcessor<Gamer>
    {
        /// <summary>
        ///     Initializes a new GamerContentProcessor class.
        /// </summary>
        public GamerContentProcessor()
            : base(new Guid("2A6EB8A1-981E-42AC-92E5-87C1EBB5BDE5"))
        {
        }

        /// <summary>
        ///     Reads the data.
        /// </summary>
        /// <param name="filepath">The FilePath.</param>
        /// <returns>Gamer.</returns>
        public override Gamer ReadData(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var binaryreader = new BinaryReader(fileStream);

                try
                {
                    var gamer = new Gamer
                    {
                        DisplayName = binaryreader.ReadString(),
                        Guid = Guid.Parse(binaryreader.ReadString())
                    };

                    return gamer;
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
        public override void WriteData(Gamer data, string destinationpath)
        {
            using (var fileStream = new FileStream(destinationpath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var binaryWriter = new BinaryWriter(fileStream);

                    binaryWriter.Write(data.DisplayName);
                    binaryWriter.Write(data.Guid.ToString());
                }
                catch (Exception ex)
                {
                    throw new ContentProcessorException(GetType().Name + " caused an error.", ex);
                }
            }
        }
    }
}