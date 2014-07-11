using System;
using CSCore;
using CSCore.Codecs;
using CSCore.Streams;
using CSCore.XAudio2;

namespace Sharpex2D.Audio.XAudio2
{
    public class XAudio2SoundProvider : ISoundProvider
    {
        private readonly int _operationSet;
        private readonly CSCore.XAudio2.XAudio2 _xaudio2;
        private IWaveSource _currentWaveSource;
        private XAudio2MasteringVoice _masteringVoice;
        private PanSource _panSource;
        private StreamingSourceVoice _sourceVoice;

        /// <summary>
        ///     Initializes a new XAudio2SoundProvider class
        ///     <param name="xaudio2">The XAudio2 Instance.</param>
        ///     <param name="soundInitializer">The ISoundInitializer.</param>
        /// </summary>
        internal XAudio2SoundProvider(CSCore.XAudio2.XAudio2 xaudio2, ISoundInitializer soundInitializer)
        {
            SoundInitializer = soundInitializer;
            _xaudio2 = xaudio2;
            _masteringVoice = _xaudio2.CreateMasteringVoice();
            _operationSet = Environment.TickCount;
        }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("BDF50AB9-4AFF-42C9-ADBF-0C84530A71CE"); }
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (_sourceVoice != null)
            {
                StreamingSourceVoiceListener.Default.Remove(_sourceVoice);
                _sourceVoice.Stop(SourceVoiceStopFlags.None, _operationSet);
            }
        }

        /// <summary>
        ///     Gets or sets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _panSource.Pan; }
            set { _panSource.Pan = value; }
        }

        /// <summary>
        ///     Gets or sets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _sourceVoice.Volume; }
            set { _sourceVoice.Volume = value; }
        }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public long Position
        {
            get { return _currentWaveSource.GetPosition().Milliseconds; }
            set { _currentWaveSource.SetPosition(new TimeSpan(0, 0, 0, 0, (int) value)); }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        ///     Gets the Length.
        /// </summary>
        public long Length
        {
            get { return _currentWaveSource.GetLength().Milliseconds; }
        }

        /// <summary>
        ///     Gets the ISoundInitializer.
        /// </summary>
        public ISoundInitializer SoundInitializer { get; private set; }

        /// <summary>
        ///     Plays a sound.
        /// </summary>
        /// <param name="soundFile">The Sound.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound soundFile, PlayMode playMode)
        {
            Play(CodecFactory.Instance.GetCodec(soundFile.ResourcePath), playMode);
        }

        /// <summary>
        ///     Resumes the sound.
        /// </summary>
        public void Resume()
        {
            _sourceVoice.Start();
        }

        /// <summary>
        ///     Pause the sound.
        /// </summary>
        public void Pause()
        {
            _sourceVoice.Stop(SourceVoiceStopFlags.None, CSCore.XAudio2.XAudio2.CommitAll);
        }

        /// <summary>
        ///     Seeks the sound.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            Position = position;
        }

        /// <summary>
        ///     Plays the wave source.
        /// </summary>
        /// <param name="waveSource">The WaveSource.</param>
        /// <param name="playMode">The PlayMode.</param>
        internal void Play(IWaveSource waveSource, PlayMode playMode)
        {
            if (playMode == PlayMode.Loop)
            {
                waveSource = new LoopStream(waveSource);
            }

            var panSource = new PanSource(waveSource);
            _panSource = panSource;
            _currentWaveSource = panSource.ToWaveSource();
            _sourceVoice = StreamingSourceVoice.Create(_xaudio2, _currentWaveSource);
            StreamingSourceVoiceListener.Default.Add(_sourceVoice);

            IsPlaying = true;
        }
    }
}