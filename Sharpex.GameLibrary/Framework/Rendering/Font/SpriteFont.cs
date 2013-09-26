using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using SharpexGL.Framework.Content.Factory;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Font
{
    [Serializable]
    public class SpriteFont
    {
        private string _internalValue = "";
        private System.Drawing.Color _internalColor = System.Drawing.Color.Black;

        /// <summary>
        /// Static ctor.
        /// </summary>
        static SpriteFont()
        {
            Factory = new SpriteFontFactory();
        }
        /// <summary>
        /// Sets or gets the SpriteFontFactory.
        /// </summary>
        public static SpriteFontFactory Factory {private set; get; }
        /// <summary>
        /// Gets or sets the space between chars.
        /// </summary>
        public int Kerning
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the whitespace width.
        /// </summary>
        public int Spacing
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the current font.
        /// </summary>
        public System.Drawing.Font FontType
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value
        {
            get
            {
                return _internalValue;
            }
            set
            {
                if (_internalValue != value)
                {
                    _internalValue = value;
                    CacheIsObsolete = true;
                }
            }
        }
        /// <summary>
        /// Gets or sets the font color.
        /// </summary>
        public System.Drawing.Color FontColor
        {
            get
            {
                return _internalColor;
            }
            set
            {
                if (_internalColor != value)
                {
                    _internalColor = value;
                    CacheIsObsolete = true;
                }
            }
        }
        /// <summary>
        /// Gets or sets the cached texture.
        /// </summary>
        private Texture CachedTexture
        {
            get;
            set;
        }
        /// <summary>
        /// Determines if the cache is obsolete.
        /// </summary>
        private bool CacheIsObsolete
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes a new SpriteFont.
        /// </summary>
        public SpriteFont()
        {
        }
        /// <summary>
        /// Initializes a new SpriteFont.
        /// </summary>
        /// <param name="kerning">The Kerning.</param>
        /// <param name="spacing">The Spacing.</param>
        public SpriteFont(int kerning, int spacing)
        {
            Kerning = kerning;
            Spacing = spacing;
        }
        /// <summary>
        /// Initializes a new SpriteFont.
        /// </summary>
        /// <param name="kerning">The Kerning.</param>
        /// <param name="spacing">The Spacing.</param>
        /// <param name="value"></param>
        public SpriteFont(int kerning, int spacing, string value)
        {
            Kerning = kerning;
            Spacing = spacing;
            Value = value;
        }
        /// <summary>
        /// Initializes a new SpriteFont.
        /// </summary>
        /// <param name="kerning">The Kerning.</param>
        /// <param name="spacing">The Spacing.</param>
        /// <param name="value">The Value.</param>
        /// <param name="font">The Font.</param>
        public SpriteFont(int kerning, int spacing, string value, System.Drawing.Font font)
        {
            Kerning = kerning;
            Spacing = spacing;
            Value = value;
            FontType = font;
        }
        /// <summary>
        /// Initializes a new SpriteFont.
        /// </summary>
        /// <param name="kerning">The Kerning.</param>
        /// <param name="spacing">The Spacing.</param>
        /// <param name="value">The Value.</param>
        /// <param name="font">The Font.</param>
        /// <param name="color">The Color.</param>
        public SpriteFont(int kerning, int spacing, string value, System.Drawing.Font font, Color color)
        {
            Kerning = kerning;
            Spacing = spacing;
            Value = value;
            FontType = font;
            FontColor = color.ToWin32Color();
        }
        /// <summary>
        /// Renders the spritefont.
        /// </summary>
        /// <returns>Texture</returns>
        public Texture Render()
        {
            Texture result;
            if (!CacheIsObsolete)
            {
                result = CachedTexture;
            }
            else
            {
                var vector = MeassureString(Value);
                var bitmap = new Bitmap((int)vector.X, (int)vector.Y);
                var graphics = Graphics.FromImage(bitmap);
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var num = 0;
                var num2 = 0;
                var array = Value.Split(new[]
				{
					Environment.NewLine
				}, StringSplitOptions.None);
                var array2 = array;
                foreach (var text in array2)
                {
                    var text2 = text;
                    foreach (char c in text2)
                    {
                        if (c.ToString() != " ")
                        {
                            int num3 = (int)graphics.MeasureString(c.ToString(), FontType).Width + Kerning;
                            graphics.DrawString(c.ToString(), FontType, new SolidBrush(FontColor), new Point(num, num2));
                            num += num3;
                        }
                        else
                        {
                            num += Spacing;
                        }
                    }
                    num = 0;
                    if (Kerning >= 0)
                    {
                        num2 += (int)graphics.MeasureString(text, FontType).Height + Kerning;
                    }
                    else
                    {
                        num2 += (int)graphics.MeasureString(text, FontType).Height + 1;
                    }
                }
                graphics.Dispose();
                CachedTexture = new Texture
                {
                    Texture2D = bitmap
                };
                CacheIsObsolete = false;
                result = new Texture
                {
                    Texture2D = bitmap
                };
            }
            return result;
        }
        /// <summary>
        /// Returns a Vector based on the value layout.
        /// </summary>
        /// <param name="value">The Value.</param>
        /// <returns>Vector2</returns>
        public Vector2 MeassureString(string value)
        {
            string[] array = value.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.None);
            var num = 0;
            var num2 = Kerning;
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                int num3 = 0;
                string text2 = text;
                for (int j = 0; j < text2.Length; j++)
                {
                    char c = text2[j];
                    if (c.ToString() == " ")
                    {
                        num3 += Spacing;
                    }
                    else
                    {
                        num3 += (int)graphics.MeasureString(c.ToString(), FontType).Width + Kerning;
                    }
                }
                if (num < num3 - Kerning)
                {
                    num = num3 - Kerning;
                }
                if (Kerning >= 0)
                {
                    num2 += (int)graphics.MeasureString(text, FontType).Height + Kerning;
                }
                else
                {
                    num2 += (int)graphics.MeasureString(text, FontType).Height + 1;
                }
            }
            return new Vector2(num, num2);
        }
    }
}
