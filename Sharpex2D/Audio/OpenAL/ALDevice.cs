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
using System.Linq;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal class ALDevice : IDisposable
    {
        private static ALDevice[] _devices;

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the openal context
        /// </summary>
        public ALContext Context { get; private set; }

        private IntPtr _deviceHandle;
        private readonly List<ALSource> _sources; 

        /// <summary>
        /// Initializes a new ALDevice class
        /// </summary>
        public ALDevice(string deviceName)
        {
            Name = deviceName;
            _sources = new List<ALSource>();
        }

        /// <summary>
        /// Initializes the openal device
        /// </summary>
        public void Initialize()
        {
            _deviceHandle = ALInterops.alcOpenDevice(Name);
            Context = ALContext.CreateContext(_deviceHandle);
        }

        /// <summary>
        /// Generates a new openal source
        /// </summary>
        /// <returns></returns>
        public ALSource GenerateALSource()
        {
            Context.MakeCurrent();

            var sources = new uint[1];
            ALInterops.alGenSources(1, sources);

            return new ALSource(this, sources[0]);
        }

        /// <summary>
        /// Deletes the specified openal source
        /// </summary>
        /// <param name="source">The source</param>
        public void DeleteALSource(ALSource source)
        {
            Context.MakeCurrent();

            var sources = new uint[1];
            sources[0] = source.Id;

            ALInterops.alDeleteSources(1, sources);
        }

        /// <summary>
        /// Enumerates the openal devices
        /// </summary>
        /// <returns></returns>
        public static ALDevice[] EnumerateALDevices()
        {
            if (_devices == null)
            {
                var deviceNames = ALInterops.GetALDeviceNames();
                var devices = new ALDevice[deviceNames.Length];

                for (int i = 0; i < devices.Length; i++)
                {
                    devices[i] = new ALDevice(deviceNames[i]);
                }

                _devices = devices;
            }

            return _devices;
        }

        /// <summary>
        /// Gets the default playback device
        /// </summary>
        public static ALDevice DefaultDevice => EnumerateALDevices().FirstOrDefault();

        /// <summary>
        /// Disposes the openal device
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the openal device
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            if (_deviceHandle != IntPtr.Zero)
            {
                ALInterops.alcCloseDevice(_deviceHandle);
                _deviceHandle = IntPtr.Zero;
            }
        }
    }
}
