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
using System.Runtime.InteropServices;

namespace Sharpex2D.Framework.Audio.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class MMInterops
    {
        /// <summary>
        /// Callbackfunction number.
        /// </summary>
        internal const int CALLBACK_FUNCTION = 0x00030000;

        /// <summary>
        /// Gets the number of devices.
        /// </summary>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern int waveOutGetNumDevs();

        /// <summary>
        /// Gets the dev caps.
        /// </summary>
        /// <param name="deviceId">The DeviceId.</param>
        /// <param name="waveOutCaps">The WaveOutCaps.</param>
        /// <param name="cbwaveOutCaps">The Size.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutGetDevCaps(uint deviceId, out WaveOutCaps waveOutCaps, uint cbwaveOutCaps);

        /// <summary>
        /// Prepares the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutPrepareHeader(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        /// Unprepares the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutUnprepareHeader(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="lpWaveOutHdr">The WaveHeader.</param>
        /// <param name="uSize">The Size.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutWrite(IntPtr hWaveOut, WaveHdr lpWaveOutHdr, int uSize);

        /// <summary>
        /// Opens a WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="uDeviceID">The DeviceHandle.</param>
        /// <param name="lpFormat">The WaveFormat.</param>
        /// <param name="dwCallback">The Callback.</param>
        /// <param name="dwInstance">The Instance.</param>
        /// <param name="dwFlags">The Flags.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutOpen(out IntPtr hWaveOut, IntPtr uDeviceID, WaveFormat lpFormat,
            WaveCallback dwCallback, IntPtr dwInstance, uint dwFlags);

        /// <summary>
        /// Resets the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutReset(IntPtr hWaveOut);

        /// <summary>
        /// Closes the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutClose(IntPtr hWaveOut);

        /// <summary>
        /// Pauses the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutPause(IntPtr hWaveOut);

        /// <summary>
        /// Restarts the WaveOut.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutRestart(IntPtr hWaveOut);

        /// <summary>
        /// Sets the volume.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwVolume">The Volume.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutSetVolume(IntPtr hWaveOut, uint dwVolume);

        /// <summary>
        /// Gets the Volume.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwVolume">The Volume.</param>
        /// <returns>Int.</returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutGetVolume(IntPtr hWaveOut, out uint dwVolume);

        /// <summary>
        /// Gets the Pitch.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="pdwPitch">The Pitch.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutGetPitch(IntPtr hWaveOut, IntPtr pdwPitch);

        /// <summary>
        /// Sets the Pitch.
        /// </summary>
        /// <param name="hWaveOut">The Handle.</param>
        /// <param name="dwPitch">The Pitch.</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        internal static extern MMResult waveOutSetPitch(IntPtr hWaveOut, int dwPitch);

        internal delegate void WaveCallback(
            IntPtr handle, WaveMessage msg, UIntPtr user, WaveHdr header, UIntPtr reserved);

        /// <summary>
        /// WaveDelegate.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        /// <param name="message">The Message.</param>
        /// <param name="user">The User.</param>
        /// <param name="waveHeader">The WaveHeader.</param>
        /// <param name="reserved">Reserved for driver.</param>
        internal delegate void WaveDelegate(IntPtr handle, int message, IntPtr user, WaveHdr waveHeader, IntPtr reserved
            );
    }
}