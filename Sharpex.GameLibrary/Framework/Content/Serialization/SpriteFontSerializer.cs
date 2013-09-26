using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Rendering.Font;
using Color = SharpexGL.Framework.Rendering.Color;

namespace SharpexGL.Framework.Content.Serialization
{
    public class SpriteFontSerializer : ContentSerializer<SpriteFont>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override SpriteFont Read(BinaryReader reader)
        {
            var color = new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            var kerning = reader.ReadInt32();
            var spacing = reader.ReadInt32();
            var value = reader.ReadString();
            var fontname = reader.ReadString();
            var fontSize = reader.ReadSingle();
            var styleAtribute = reader.ReadInt32();
            Font font = null;
            if (styleAtribute == 3)
            {
                font = new Font(fontname, fontSize, FontStyle.Italic);
            }
            if (styleAtribute == 2)
            {
                font = new Font(fontname, fontSize, FontStyle.Regular);
            }
            if (styleAtribute == 1)
            {
                font = new Font(fontname, fontSize, FontStyle.Bold);
            }
            reader.Close();
            return new SpriteFont(kerning, spacing, value, font, color);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, SpriteFont value)
        {
            //Strcuture
            //Color: Int, Int, Int, Int (RGBA)
            //Kerning: Int
            //Spacing: Int
            //Value: String
            //FontType: String, Int, Style[Int] StyleInt = 1 : Bold StyleInt = 2 : Regular StyleInt = 3 : Italic

            writer.Write(value.FontColor.R);
            writer.Write(value.FontColor.G);
            writer.Write(value.FontColor.B);
            writer.Write(value.FontColor.A);
            writer.Write(value.Kerning);
            writer.Write(value.Spacing);
            writer.Write(value.Value);
            writer.Write(value.FontType.FontFamily.Name);
            writer.Write(value.FontType.Size);
            int style;
            if (value.FontType.Italic)
            {
                style = 3;
            }
            else if (value.FontType.Bold)
            {
                style = 1;
            }
            else
            {
                style = 2;
            }
            writer.Write(style);
            writer.Close();
        }
    }
}
