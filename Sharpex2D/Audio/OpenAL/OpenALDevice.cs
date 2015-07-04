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
using System.Collections.Generic;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class OpenALDevice : IDisposable
    {
        private readonly List<OpenALAudio> _audioBuffers;
        private IntPtr _deviceHandle;
        private bool _disposing;

        /// <summary>
        /// Initializes a new OpenALDevice class.
        /// </summary>
        /// <param name="deviceName">The DeviceName.</param>
        /// <param name="id">The ID.</param>
        internal OpenALDevice(string deviceName, int id)
        {
            Name = deviceName;
            Id = id;
            _audioBuffers = new List<OpenALAudio>();

            InitializeDevice();
        }

        /// <summary>
        /// Gets the Title of the audio device.
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        public int Id { private set; get; }

        /// <summary>
        /// Gets the OpenALContext.
        /// </summary>
        public OpenALContext Context { get; private set; }

        /// <summary>
        /// Gets the SourcePool for this device.
        /// </summary>
        public OpenALSourcePool SourcePool { get; private set; }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the OpenALDevice class.
        /// </summary>
        ~OpenALDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// Creates a new AudioBuffer.
        /// </summary>
        /// <param name="format">The AudioFormat.</param>
        /// <returns>OpenALAudioBuffer.</returns>
        internal OpenALAudio CreateAudioBuffer(OpenALAudioFormat format)
        {
            var audioBuffer = new OpenALAudio(format, this);
            lock (_audioBuffers)
            {
                _audioBuffers.Add(audioBuffer);
            }

            return audioBuffer;
        }

        /// <summary>
        /// Destroys the AudioBuffer.
        /// </summary>
        /// <param name="audioBuffer">The OpenALAudioBuffer.</param>
        internal void DestroyAudioBuffer(OpenALAudio audioBuffer)
        {
            if (_disposing) return; //avoid deadlock

            lock (_audioBuffers)
            {
                _audioBuffers.Remove(audioBuffer);
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SourcePool.Dispose();
                Context.Dispose();
            }

            if (_deviceHandle == IntPtr.Zero) return;

            _disposing = true;

            OpenALInterops.alcCloseDevice(_deviceHandle);
            _deviceHandle = IntPtr.Zero;
            Context = null;
        }

        /// <summary>
        /// Initializes the device.
        /// </summary>
        private void InitializeDevice()
        {
            if (_deviceHandle != IntPtr.Zero) return;

            _disposing = false;
            _deviceHandle = OpenALInterops.alcOpenDevice(Name);
            Context = OpenALContext.CreateContext(_deviceHandle);
            SourcePool = new OpenALSourcePool(Context);
        }

        #region Static Implementation

        private static OpenALDevice[] _openALDevices;

        /// <summary>
        /// Gets all available openal devices.
        /// </summary>
        public static OpenALDevice[] AvailableDevices
        {
            get { return _openALDevices ?? (_openALDevices = OpenALInterops.GetOpenALDevices()); }
        }

        /// <summary>
        /// Gets the OpenALDevice assigned for this application.
        /// </summary>
        public static OpenALDevice DefaultDevice { private set; get; }

        /// <summary>
        /// Sets the OpenALDevice for this application.
        /// </summary>
        /// <param name="device">The OpenALDevice.</param>
        public static void SetOpenALDeviceIfNeeded(OpenALDevice device)
        {
            if (DefaultDevice != null)
            {
                if (DefaultDevice.Id != device.Id)
                {
                    DefaultDevice.Dispose();
                    DefaultDevice = device;
                }
            }
            else
            {
                DefaultDevice = device;
            }
        }

        /// <summary>
        /// Sets the default openal device if no one is set.
        /// </summary>
        /// <param name="device">The OpenALDevice.</param>
        public static void SetDefaultOpenALDeviceOnStartup(OpenALDevice device)
        {
            if (DefaultDevice == null) DefaultDevice = device;
        }

        #endregion
    }
}