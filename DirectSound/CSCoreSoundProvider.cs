using System;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;

namespace SharpexGL.Framework.Media.Sound.DiectSound
{
    public class CSCoreSoundProvider : ISoundProvider
    {
        private DirectSoundOutExtended _directSoundOut;

        public CSCoreSoundProvider()
        {
            _directSoundOut = new DirectSoundOutExtended();
        }

        public void Play(Sound soundFile, PlayMode playMode)
        {
            Play(CodecFactory.Instance.GetCodec(soundFile.ResourcePath), playMode);
        }

        public void Play(IWaveSource source, PlayMode playMode)
        {
            Stop();
            if (playMode == PlayMode.Loop)
                source = new LoopStream(source);
            _directSoundOut.Initialize(source);
            _directSoundOut.Play();
            IsPlaying = true;
        }

        public void Resume()
        {
            _directSoundOut.Resume();
        }

        public void Pause()
        {
            _directSoundOut.Pause();
            IsPlaying = false;
        }

        private void Stop()
        {
            if (PlaybackState != PlaybackState.Stopped)
            {
                _directSoundOut.Stop();
                IsPlaying = false;
            }
        }

        public void Seek(long position)
        {
            if (_directSoundOut.WaveSource != null)
                _directSoundOut.WaveSource.Position = position;
        }

        public long Position
        {
            get { return _directSoundOut.WaveSource != null ? _directSoundOut.WaveSource.Position : 0; }
            set { Seek(value); }
        }

        public bool IsPlaying { get; set; }

        public long Length
        {
            get { return _directSoundOut.WaveSource != null ? _directSoundOut.WaveSource.Length : 0; }
        }

        public float Balance
        {
            get { return _directSoundOut.Pan; }
            set { _directSoundOut.Pan = value; }
        }

        public float Volume
        {
            get { return _directSoundOut.Volume; }
            set { _directSoundOut.Volume = value; }
        }

        public PlaybackState PlaybackState
        {
            get { return _directSoundOut.PlaybackState; }
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_directSoundOut != null)
                {
                    Stop();
                    _directSoundOut.Dispose();
                    _directSoundOut = null;
                }
            }
            _disposed = true;
        }

        ~CSCoreSoundProvider()
        {
            Dispose(false);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
