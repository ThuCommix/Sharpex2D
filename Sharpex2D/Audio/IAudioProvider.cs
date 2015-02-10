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

namespace Sharpex2D.Audio
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IAudioProvider : IDisposable
    {
        /// <summary>
        /// Sets or gets the Pan.
        /// </summary>
        float Pan { set; get; }

        /// <summary>
        /// Sets or gets the Volume.
        /// </summary>
        float Volume { set; get; }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        long Position { set; get; }

        /// <summary>
        /// Gets the PlaybackState.
        /// </summary>
        PlaybackState PlaybackState { get; }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Triggered if the playback state changed.
        /// </summary>
        event PlaybackChangedEventHandler PlaybackChanged;

        /// <summary>
        /// Initializes the audio provider with the given source.
        /// </summary>
        /// <param name="audioSource">The IAudioSource.</param>
        void Initialize(IAudioSource audioSource);

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="playbackMode">The PlaybackMode.</param>
        void Play(PlaybackMode playbackMode);

        /// <summary>
        /// Resumes a sound.
        /// </summary>
        void Resume();

        /// <summary>
        /// Pause a sound.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops the sound.
        /// </summary>
        void Stop();

        /// <summary>
        /// Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        void Seek(long position);

        /// <summary>
        /// Creates an AudioSource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>AudioSource.</returns>
        IAudioSource CreateAudioSource(string path);
    }
}