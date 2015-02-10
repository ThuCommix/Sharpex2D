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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sharpex2D.Audio.OpenAL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class OpenAL
    {
        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alGetString(int name);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcGetString([In] IntPtr device, int name);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern sbyte alcIsExtensionPresent([In] IntPtr device, string extensionName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern sbyte alIsExtensionPresent(string extensionName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcCaptureStart(IntPtr device);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcCaptureStop(IntPtr device);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcCaptureSamples(IntPtr device, IntPtr buffer, int numSamples);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcCaptureOpenDevice(string deviceName, uint frequency, OpenALAudioFormat format,
            int bufferSize);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcCaptureCloseDevice(IntPtr device);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcOpenDevice(string deviceName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcCloseDevice(IntPtr handle);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcCreateContext(IntPtr device, IntPtr attrlist);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcMakeContextCurrent(IntPtr context);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcGetContextsDevice(IntPtr context);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcGetCurrentContext();

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcDestroyContext(IntPtr context);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetSourcei(uint sourceId, SourceProperty property, out int value);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourcePlay(uint sourceId);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourcePause(uint sourceId);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourceStop(uint sourceId);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourceQueueBuffers(uint sourceId, int number, uint[] bufferIDs);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourceUnqueueBuffers(uint sourceId, int buffers, uint[] buffersDequeued);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGenSources(int count, uint[] sources);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alDeleteSources(int count, uint[] sources);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetSourcef(uint sourceId, SourceProperty property, out float value);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetSource3f(uint sourceId, SourceProperty property, out float val1,
            out float val2, out float val3);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourcef(uint sourceId, SourceProperty property, float value);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourcefv(uint sourceId, SourceProperty property, float[] value);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSource3f(uint sourceId, SourceProperty property, float val1, float val2,
            float val3);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alSourcei(uint sourceId, SourceProperty property, float val1);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGenBuffers(int count, uint[] bufferIDs);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alBufferData(uint bufferId, OpenALAudioFormat format, byte[] data, int byteSize,
            uint frequency);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alDeleteBuffers(int numBuffers, uint[] bufferIDs);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alListenerf(SourceProperty param, float val);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alListenerfv(SourceProperty param, float[] val);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alListener3f(SourceProperty param, float val1, float val2, float val3);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetListener3f(SourceProperty param, out float val1, out float val2,
            out float val3);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetListenerf(SourceProperty param, out float val);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetListenerfv(SourceProperty param, float[] val);

        internal static OpenALDevice[] GetOpenALDevices()
        {
            var strings = new string[0];
            if (IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
            {
                strings =
                    ReadStringsFromMemory(alcGetString(IntPtr.Zero,
                        (int) DeviceSpecifications.AllDevicesSpecifier));
            }
            else if (IsExtensionPresent("ALC_ENUMERATION_EXT"))
            {
                strings =
                    ReadStringsFromMemory(alcGetString(IntPtr.Zero, (int) DeviceSpecifications.DeviceSpecifier));
            }

            var devices = new OpenALDevice[strings.Length];

            for (var i = 0; i < devices.Length; i++)
            {
                devices[i] = new OpenALDevice(strings[i], i);
            }

            return devices;
        }

        internal static string[] ReadStringsFromMemory(IntPtr location)
        {
            var strings = new List<string>();

            bool lastNull = false;
            int i = -1;
            byte c;
            while (!((c = Marshal.ReadByte(location, ++i)) == '\0' && lastNull))
            {
                if (c == '\0')
                {
                    lastNull = true;

                    strings.Add(Marshal.PtrToStringAnsi(location, i));
                    location = new IntPtr((long) location + i + 1);
                    i = -1;
                }
                else
                    lastNull = false;
            }

            return strings.ToArray();
        }

        internal static bool IsExtensionPresent(string extension)
        {
            var result = extension.StartsWith("ALC")
                ? alcIsExtensionPresent(IntPtr.Zero, extension)
                : alIsExtensionPresent(extension);

            return (result == 1);
        }

        internal static bool IsSupported()
        {
            try
            {
                alIsExtensionPresent("TEST_ONLY");
                return true;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
        }
    }
}