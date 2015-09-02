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
using System.Runtime.InteropServices;
using System.Security;

namespace Sharpex2D.Framework.Audio.OpenAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class ALInterops
    {
        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcGetString([In] IntPtr device, int name);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern sbyte alcIsExtensionPresent([In] IntPtr device, string extensionName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern sbyte alIsExtensionPresent(string extensionName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcOpenDevice(string deviceName);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcCloseDevice(IntPtr handle);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr alcCreateContext(IntPtr device, IntPtr attrlist);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcMakeContextCurrent(IntPtr context);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alcDestroyContext(IntPtr context);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alGetSourcei(uint sourceId, ALSourceParameters param, out int value);

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
        internal static extern void alGenBuffers(int count, uint[] bufferIDs);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alBufferData(uint bufferId, ALFormat format, byte[] data, int byteSize,
            uint frequency);

        [DllImport("OpenAL32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void alDeleteBuffers(int numBuffers, uint[] bufferIDs);

        public const int DeviceSpecifier = 0x1005;

        public const int AllDevicesSpecifier = 0x1013;

        internal static string[] GetALDeviceNames()
        {
            var strings = new string[0];
            if (IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
            {
                strings =
                    ReadStringsFromMemory(alcGetString(IntPtr.Zero,
                        AllDevicesSpecifier));
            }
            else if (IsExtensionPresent("ALC_ENUMERATION_EXT"))
            {
                strings =
                    ReadStringsFromMemory(alcGetString(IntPtr.Zero, DeviceSpecifier));
            }

            return strings;
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
            sbyte result = extension.StartsWith("ALC")
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
