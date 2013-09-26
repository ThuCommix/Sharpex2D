using System;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Media.Sound
{
    public interface ISoundProvider : IComponent, IDisposable, ICloneable
    {
        void Play(Sound soundFile, PlayMode playMode);
        void Resume();
        void Pause();
        void Seek(long position);
        float Balance { set; get; }
        float Volume { set; get; }
        long Position { set; get; }
        bool IsPlaying { set; get; }
        long Length { get; }
    }
}
