// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using Sharpex2D.Framework.Audio;

namespace Sharpex2D.Audio.DirectSound
{
    internal class DirectSoundDevice : CSCore.SoundOut.DirectSound.DirectSoundDevice, IPlaybackDevice
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name => Description;

        /// <summary>
        /// Initializes a new DirectSoundDevice class
        /// </summary>
        /// <param name="description">The description</param>
        /// <param name="module"></param>
        /// <param name="guid"></param>
        public DirectSoundDevice(string description, string module, Guid guid) : base(description, module, guid)
        {

        }

        /// <summary>
        /// Creates a new directsound device from the cscore device
        /// </summary>
        /// <param name="device">The device</param>
        /// <returns>Directsound device</returns>
        public static DirectSoundDevice FromCsCoreDirectSoundDevice(CSCore.SoundOut.DirectSound.DirectSoundDevice device)
        {
            return new DirectSoundDevice(device.Description, device.Module, device.Guid);
        }
    }
}
