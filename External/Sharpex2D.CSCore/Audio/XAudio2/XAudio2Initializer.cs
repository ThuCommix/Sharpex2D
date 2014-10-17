using System;
using CSCore.XAudio2;

namespace Sharpex2D.Audio.XAudio2
{
    public class XAudio2Initializer : ISoundInitializer
    {
        private CSCore.XAudio2.XAudio2 _xaudio2;

        /// <summary>
        /// Initializes a new XAudio2Initializer class.
        /// </summary>
        public XAudio2Initializer()
        {
            CreateXAudio2();
        }

        /// <summary>
        /// A value indicating whether the SoundProvider is supported.
        /// </summary>
        public bool IsSupported
        {
            get { return _xaudio2 != null; }
        }

        /// <summary>
        /// Creates the Provider.
        /// </summary>
        /// <returns>ISoundProvider.</returns>
        public ISoundProvider Create()
        {
            if (_xaudio2 == null)
            {
                throw new NotSupportedException("No supported XAudio2 version is installed.");
            }

            return new XAudio2SoundProvider(_xaudio2, this);
        }

        /// <summary>
        /// Creates the XAudio2.
        /// </summary>
        private void CreateXAudio2()
        {
            try
            {
                _xaudio2 = new XAudio2_8(XAudio2Processor.Xaudio2DefaultProcessor);
            }
            catch (Exception)
            {
                try
                {
                    _xaudio2 = new XAudio2_7(false, XAudio2Processor.Xaudio2DefaultProcessor);
                }
                catch
                {
                }
            }
        }
    }
}