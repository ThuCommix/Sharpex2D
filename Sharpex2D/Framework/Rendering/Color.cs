// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Serializable]
    public struct Color
    {
        /// <summary>
        ///     Initializes a new Color.
        /// </summary>
        /// <param name="r">The RedValue.</param>
        /// <param name="g">The GreenValue.</param>
        /// <param name="b">The BlueValue.</param>
        /// <param name="a">The AlphaValue.</param>
        public Color(int r, int g, int b, int a) : this()
        {
            R = (byte) r;
            G = (byte) g;
            B = (byte) b;
            A = (byte) a;
        }

        /// <summary>
        ///     Gets or sets the RedValue.
        /// </summary>
        public byte R { set; get; }

        /// <summary>
        ///     Gets or sets the GreenValue.
        /// </summary>
        public byte G { set; get; }

        /// <summary>
        ///     Gets or sets the BlueValue.
        /// </summary>
        public byte B { set; get; }

        /// <summary>
        ///     Gets or sets the AlphaValue.
        /// </summary>
        public byte A { set; get; }

        /// <summary>
        ///     Generates a Color from the given Values.
        /// </summary>
        /// <param name="a">The AlphaValue.</param>
        /// <param name="r">The RedValue.</param>
        /// <param name="g">The GreenValue.</param>
        /// <param name="b">The BlueValue.</param>
        /// <returns>Color</returns>
        public static Color FromArgb(int a, int r, int g, int b)
        {
            return new Color(r, g, b, a);
        }

        #region Colors

        public static Color AliceBlue
        {
            get { return new Color(240, 248, 255, 255); }
        }

        public static Color AntiqueWhite
        {
            get { return new Color(250, 235, 215, 255); }
        }

        public static Color Aqua
        {
            get { return new Color(0, 255, 255, 255); }
        }

        public static Color Aquamarine
        {
            get { return new Color(127, 255, 212, 255); }
        }

        public static Color Azure
        {
            get { return new Color(240, 255, 255, 255); }
        }

        public static Color Beige
        {
            get { return new Color(245, 245, 220, 255); }
        }

        public static Color Bisque
        {
            get { return new Color(255, 228, 196, 255); }
        }

        public static Color Black
        {
            get { return new Color(0, 0, 0, 255); }
        }

        public static Color BlanchedAlmond
        {
            get { return new Color(255, 235, 205, 255); }
        }

        public static Color Blue
        {
            get { return new Color(0, 0, 255, 255); }
        }

        public static Color BlueViolet
        {
            get { return new Color(138, 43, 226, 255); }
        }

        public static Color Brown
        {
            get { return new Color(165, 42, 42, 255); }
        }

        public static Color BurlyWood
        {
            get { return new Color(222, 184, 135, 255); }
        }

        public static Color CadetBlue
        {
            get { return new Color(95, 158, 160, 255); }
        }

        public static Color Chartreuse
        {
            get { return new Color(127, 255, 0, 255); }
        }

        public static Color Chocolate
        {
            get { return new Color(210, 105, 30, 255); }
        }

        public static Color Coral
        {
            get { return new Color(255, 127, 80, 255); }
        }

        public static Color CornflowerBlue
        {
            get { return new Color(100, 149, 237, 255); }
        }

        public static Color Cornsilk
        {
            get { return new Color(255, 248, 220, 255); }
        }

        public static Color Crimson
        {
            get { return new Color(220, 20, 60, 255); }
        }

        public static Color Cyan
        {
            get { return new Color(0, 255, 255, 255); }
        }

        public static Color DarkBlue
        {
            get { return new Color(0, 0, 139, 255); }
        }

        public static Color DarkCyan
        {
            get { return new Color(0, 139, 139, 255); }
        }

        public static Color DarkGoldenrod
        {
            get { return new Color(184, 134, 11, 255); }
        }

        public static Color DarkGray
        {
            get { return new Color(169, 169, 169, 255); }
        }

        public static Color DarkGreen
        {
            get { return new Color(0, 100, 0, 255); }
        }

        public static Color DarkKhaki
        {
            get { return new Color(189, 183, 107, 255); }
        }

        public static Color DarkMagenta
        {
            get { return new Color(139, 0, 139, 255); }
        }

        public static Color DarkOliveGreen
        {
            get { return new Color(85, 107, 47, 255); }
        }

        public static Color DarkOrange
        {
            get { return new Color(255, 140, 0, 255); }
        }

        public static Color DarkOrchid
        {
            get { return new Color(153, 50, 204, 255); }
        }

        public static Color DarkRed
        {
            get { return new Color(139, 0, 0, 255); }
        }

        public static Color DarkSalmon
        {
            get { return new Color(233, 150, 122, 255); }
        }

        public static Color DarkSeaGreen
        {
            get { return new Color(143, 188, 139, 255); }
        }

        public static Color DarkSlateBlue
        {
            get { return new Color(72, 61, 139, 255); }
        }

        public static Color DarkSlateGray
        {
            get { return new Color(47, 79, 79, 255); }
        }

        public static Color DarkTurquoise
        {
            get { return new Color(0, 206, 209, 255); }
        }

        public static Color DarkViolet
        {
            get { return new Color(148, 0, 211, 255); }
        }

        public static Color DeepPink
        {
            get { return new Color(255, 20, 147, 255); }
        }

        public static Color DeepSkyBlue
        {
            get { return new Color(0, 191, 255, 255); }
        }

        public static Color DimGray
        {
            get { return new Color(105, 105, 105, 255); }
        }

        public static Color DodgerBlue
        {
            get { return new Color(30, 144, 255, 255); }
        }

        public static Color Firebrick
        {
            get { return new Color(178, 34, 34, 255); }
        }

        public static Color FloralWhite
        {
            get { return new Color(255, 250, 240, 255); }
        }

        public static Color ForestGreen
        {
            get { return new Color(34, 139, 34, 255); }
        }

        public static Color Fuchsia
        {
            get { return new Color(255, 0, 255, 255); }
        }

        public static Color Gainsboro
        {
            get { return new Color(220, 220, 220, 255); }
        }

        public static Color GhostWhite
        {
            get { return new Color(248, 248, 255, 255); }
        }

        public static Color Gold
        {
            get { return new Color(255, 215, 0, 255); }
        }

        public static Color Goldenrod
        {
            get { return new Color(218, 165, 32, 255); }
        }

        public static Color Gray
        {
            get { return new Color(128, 128, 128, 255); }
        }

        public static Color Green
        {
            get { return new Color(0, 128, 0, 255); }
        }

        public static Color GreenYellow
        {
            get { return new Color(173, 255, 47, 255); }
        }

        public static Color Honeydew
        {
            get { return new Color(240, 255, 240, 255); }
        }

        public static Color HotPink
        {
            get { return new Color(255, 105, 180, 255); }
        }

        public static Color IndianRed
        {
            get { return new Color(205, 92, 92, 255); }
        }

        public static Color Indigo
        {
            get { return new Color(75, 0, 130, 255); }
        }

        public static Color Ivory
        {
            get { return new Color(255, 255, 240, 255); }
        }

        public static Color Khaki
        {
            get { return new Color(240, 230, 140, 255); }
        }

        public static Color Lavender
        {
            get { return new Color(230, 230, 250, 255); }
        }

        public static Color LavenderBlush
        {
            get { return new Color(255, 240, 245, 255); }
        }

        public static Color LawnGreen
        {
            get { return new Color(124, 252, 0, 255); }
        }

        public static Color LemonChiffon
        {
            get { return new Color(255, 250, 205, 255); }
        }

        public static Color LightBlue
        {
            get { return new Color(173, 216, 230, 255); }
        }

        public static Color LightCoral
        {
            get { return new Color(240, 128, 128, 255); }
        }

        public static Color LightCyan
        {
            get { return new Color(224, 255, 255, 255); }
        }

        public static Color LightGoldenrodYellow
        {
            get { return new Color(250, 250, 210, 255); }
        }

        public static Color LightGray
        {
            get { return new Color(211, 211, 211, 255); }
        }

        public static Color LightGreen
        {
            get { return new Color(144, 238, 144, 255); }
        }

        public static Color LightPink
        {
            get { return new Color(255, 182, 193, 255); }
        }

        public static Color LightSalmon
        {
            get { return new Color(255, 160, 122, 255); }
        }

        public static Color LightSeaGreen
        {
            get { return new Color(32, 178, 170, 255); }
        }

        public static Color LightSkyBlue
        {
            get { return new Color(135, 206, 250, 255); }
        }

        public static Color LightSlateGray
        {
            get { return new Color(119, 136, 153, 255); }
        }

        public static Color LightSteelBlue
        {
            get { return new Color(176, 196, 222, 255); }
        }

        public static Color LightYellow
        {
            get { return new Color(255, 255, 224, 255); }
        }

        public static Color Lime
        {
            get { return new Color(0, 255, 0, 255); }
        }

        public static Color LimeGreen
        {
            get { return new Color(50, 205, 50, 255); }
        }

        public static Color Linen
        {
            get { return new Color(250, 240, 230, 255); }
        }

        public static Color Magenta
        {
            get { return new Color(255, 0, 255, 255); }
        }

        public static Color Maroon
        {
            get { return new Color(128, 0, 0, 255); }
        }

        public static Color MediumAquamarine
        {
            get { return new Color(102, 205, 170, 255); }
        }

        public static Color MediumBlue
        {
            get { return new Color(0, 0, 205, 255); }
        }

        public static Color MediumOrchid
        {
            get { return new Color(186, 85, 211, 255); }
        }

        public static Color MediumPurple
        {
            get { return new Color(147, 112, 219, 255); }
        }

        public static Color MediumSeaGreen
        {
            get { return new Color(60, 179, 113, 255); }
        }

        public static Color MediumSlateBlue
        {
            get { return new Color(123, 104, 238, 255); }
        }

        public static Color MediumSpringGreen
        {
            get { return new Color(0, 250, 154, 255); }
        }

        public static Color MediumTurquoise
        {
            get { return new Color(72, 209, 204, 255); }
        }

        public static Color MediumVioletRed
        {
            get { return new Color(199, 21, 133, 255); }
        }

        public static Color MidnightBlue
        {
            get { return new Color(25, 25, 112, 255); }
        }

        public static Color MintCream
        {
            get { return new Color(245, 255, 250, 255); }
        }

        public static Color MistyRose
        {
            get { return new Color(255, 228, 225, 255); }
        }

        public static Color Moccasin
        {
            get { return new Color(255, 228, 181, 255); }
        }

        public static Color NavajoWhite
        {
            get { return new Color(255, 222, 173, 255); }
        }

        public static Color Navy
        {
            get { return new Color(0, 0, 128, 255); }
        }

        public static Color OldLace
        {
            get { return new Color(253, 245, 230, 255); }
        }

        public static Color Olive
        {
            get { return new Color(128, 128, 0, 255); }
        }

        public static Color OliveDrab
        {
            get { return new Color(107, 142, 35, 255); }
        }

        public static Color Orange
        {
            get { return new Color(255, 165, 0, 255); }
        }

        public static Color OrangeRed
        {
            get { return new Color(255, 69, 0, 255); }
        }

        public static Color Orchid
        {
            get { return new Color(218, 112, 214, 255); }
        }

        public static Color PaleGoldenrod
        {
            get { return new Color(238, 232, 170, 255); }
        }

        public static Color PaleGreen
        {
            get { return new Color(152, 251, 152, 255); }
        }

        public static Color PaleTurquoise
        {
            get { return new Color(175, 238, 238, 255); }
        }

        public static Color PaleVioletRed
        {
            get { return new Color(219, 112, 147, 255); }
        }

        public static Color PapayaWhip
        {
            get { return new Color(255, 239, 213, 255); }
        }

        public static Color PeachPuff
        {
            get { return new Color(255, 218, 185, 255); }
        }

        public static Color Peru
        {
            get { return new Color(205, 133, 63, 255); }
        }

        public static Color Pink
        {
            get { return new Color(255, 192, 203, 255); }
        }

        public static Color Plum
        {
            get { return new Color(221, 160, 221, 255); }
        }

        public static Color PowderBlue
        {
            get { return new Color(176, 224, 230, 255); }
        }

        public static Color Purple
        {
            get { return new Color(128, 0, 128, 255); }
        }

        public static Color Red
        {
            get { return new Color(255, 0, 0, 255); }
        }

        public static Color RosyBrown
        {
            get { return new Color(188, 143, 143, 255); }
        }

        public static Color RoyalBlue
        {
            get { return new Color(65, 105, 225, 255); }
        }

        public static Color SaddleBrown
        {
            get { return new Color(139, 69, 19, 255); }
        }

        public static Color Salmon
        {
            get { return new Color(250, 128, 114, 255); }
        }

        public static Color SandyBrown
        {
            get { return new Color(244, 164, 96, 255); }
        }

        public static Color SeaGreen
        {
            get { return new Color(46, 139, 87, 255); }
        }

        public static Color SeaShell
        {
            get { return new Color(255, 245, 238, 255); }
        }

        public static Color Sienna
        {
            get { return new Color(160, 82, 45, 255); }
        }

        public static Color Silver
        {
            get { return new Color(192, 192, 192, 255); }
        }

        public static Color SkyBlue
        {
            get { return new Color(135, 206, 235, 255); }
        }

        public static Color SlateBlue
        {
            get { return new Color(106, 90, 205, 255); }
        }

        public static Color SlateGray
        {
            get { return new Color(112, 128, 144, 255); }
        }

        public static Color Snow
        {
            get { return new Color(255, 250, 250, 255); }
        }

        public static Color SpringGreen
        {
            get { return new Color(0, 255, 127, 255); }
        }

        public static Color SteelBlue
        {
            get { return new Color(70, 130, 180, 255); }
        }

        public static Color Tan
        {
            get { return new Color(210, 180, 140, 255); }
        }

        public static Color Teal
        {
            get { return new Color(0, 128, 128, 255); }
        }

        public static Color Thistle
        {
            get { return new Color(216, 191, 216, 255); }
        }

        public static Color Tomato
        {
            get { return new Color(255, 99, 71, 255); }
        }

        public static Color Transparent
        {
            get { return new Color(0, 0, 0, 0); }
        }

        public static Color Turquoise
        {
            get { return new Color(64, 224, 208, 255); }
        }

        public static Color Violet
        {
            get { return new Color(238, 130, 238, 255); }
        }

        public static Color Wheat
        {
            get { return new Color(245, 222, 179, 255); }
        }

        public static Color White
        {
            get { return new Color(255, 255, 255, 255); }
        }

        public static Color WhiteSmoke
        {
            get { return new Color(245, 245, 245, 255); }
        }

        public static Color Yellow
        {
            get { return new Color(255, 255, 0, 255); }
        }

        public static Color YellowGreen
        {
            get { return new Color(154, 205, 50, 255); }
        }

        #endregion

        #region Operator

        /// <summary>
        ///     The != Operator.
        /// </summary>
        /// <param name="a">The FirstColor.</param>
        /// <param name="b">The SecondColor.</param>
        /// <returns>Bool.</returns>
        public static bool operator !=(Color a, Color b)
        {
            return (a.A != b.A) | (a.B != b.B) | (a.G != b.G) | (a.A != b.A);
        }

        /// <summary>
        ///     The == Operator.
        /// </summary>
        /// <param name="a">The FirstColor.</param>
        /// <param name="b">The SecondColor.</param>
        /// <returns>Bool.</returns>
        public static bool operator ==(Color a, Color b)
        {
            return (a.A == b.A) & (a.B == b.B) & (a.G == b.G) & (a.A == b.A);
        }

        /// <summary>
        ///     The * Operator.
        /// </summary>
        /// <param name="a">The FirstColor.</param>
        /// <param name="scale">The Scalevalue.</param>
        /// <returns>Bool.</returns>
        public static Color operator *(Color a, float scale)
        {
            return Multiply(a, scale);
        }

        /// <summary>
        ///     Determines, if this Object is equal with another.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>Bool.</returns>
        public bool Equals(Color color)
        {
            return (A == color.A) & (B == color.B) & (G == color.G) & (A == color.A);
        }

        /// <summary>
        ///     Determines, if this Object is equal with another.
        /// </summary>
        /// <param name="obj">The Object.</param>
        /// <returns>Bool.</returns>
        public override bool Equals(object obj)
        {
            return obj is Color & Equals((Color) obj);
        }

        /// <summary>
        ///     Gets the HashCode for the current Color.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (R + B + G + A).GetHashCode();
        }

        /// <summary>
        ///     Color Converted to String.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return "{R:" + R + " G:" + G + " B:" + B + " A:" + A + "}";
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Multiplies the specified color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="scale">The scale.</param>
        public static Color Multiply(Color value, float scale)
        {
            int r = value.R;
            int g = value.G;
            int b = value.B;
            int a = value.A;

            var intScale = (int) MathHelper.Clamp(scale*65536f, 0, 0xffffff);

            r = (r*intScale) >> 16;
            g = (g*intScale) >> 16;
            b = (b*intScale) >> 16;
            a = (a*intScale) >> 16;

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;
            a = a > 255 ? 255 : a;

            return FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Counterpart for ToString.
        /// </summary>
        /// <param name="stringColor">The ColorString.</param>
        /// <returns>Color</returns>
        public static Color ToColor(string stringColor)
        {
            string[] raw = stringColor.Replace("{", "").Replace("}", "").Trim().Split(':');
            return new Color(Convert.ToInt32(raw[1]), Convert.ToInt32(raw[3]), Convert.ToInt32(raw[5]),
                Convert.ToInt32(raw[7]));
        }

        #endregion
    }
}