namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GdiQuality
    {
        /// <summary>
        ///     Initializes a new GdiQuality class.
        /// </summary>
        public GdiQuality()
        {
            AntiAlias = true;
            Interpolation = true;
            Compositing = true;
            HighQualityPixelOffset = false;
        }

        /// <summary>
        ///     Initializes a new GdiQuality class.
        /// </summary>
        /// <param name="qualityMode"></param>
        public GdiQuality(GdiQualityMode qualityMode)
        {
            switch (qualityMode)
            {
                case GdiQualityMode.Low:
                    AntiAlias = false;
                    Interpolation = false;
                    Compositing = false;
                    HighQualityPixelOffset = false;
                    break;
                case GdiQualityMode.Middle:
                    AntiAlias = false;
                    Interpolation = false;
                    Compositing = true;
                    HighQualityPixelOffset = false;
                    break;
                case GdiQualityMode.High:
                    AntiAlias = true;
                    Interpolation = true;
                    Compositing = true;
                    HighQualityPixelOffset = false;
                    break;
                case GdiQualityMode.Ultra:
                    AntiAlias = true;
                    Interpolation = true;
                    Compositing = true;
                    HighQualityPixelOffset = true;
                    break;
            }
        }

        /// <summary>
        ///     A value indicating whether AntiAlias should be used.
        /// </summary>
        public bool AntiAlias { set; get; }

        /// <summary>
        ///     A value indicating whether Interpolation should be used.
        /// </summary>
        public bool Interpolation { set; get; }

        /// <summary>
        ///     A value indicating whether Compositing should be used.
        /// </summary>
        public bool Compositing { set; get; }

        /// <summary>
        ///     A value indicating whether high quality PixelOffset should be used.
        /// </summary>
        public bool HighQualityPixelOffset { set; get; }
    }
}