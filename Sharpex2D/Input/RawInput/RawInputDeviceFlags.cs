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

namespace Sharpex2D.Input.RawInput
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Flags]
    internal enum RawInputDeviceFlags
    {
        /// <summary>No flags.</summary>
        NONE = 0,

        /// <summary>
        ///     If set, this removes the top level collection from the inclusion list. This tells the operating system to stop
        ///     reading from a device which matches the top level collection.
        /// </summary>
        REMOVE = 0x00000001,

        /// <summary>
        ///     If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only
        ///     affects a TLC whose usage page is already specified with PageOnly.
        /// </summary>
        EXCLUDE = 0x00000010,

        /// <summary>
        ///     If set, this specifies all devices whose top level collection is from the specified UsagePage. Note that Usage
        ///     must be zero. To exclude a particular top level collection, use Exclude.
        /// </summary>
        PAGEONLY = 0x00000020,

        /// <summary>
        ///     If set, this prevents any devices specified by UsagePage or Usage from generating legacy messages. This is
        ///     only for the mouse and keyboard.
        /// </summary>
        NOLEGACY = 0x00000030,

        /// <summary>
        ///     If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that
        ///     WindowHandle must be specified.
        /// </summary>
        INPUTSINK = 0x00000100,

        /// <summary>If set, the mouse button click does not activate the other window.</summary>
        CAPTUREMOUSE = 0x00000200,

        /// <summary>
        ///     If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for
        ///     example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. NoHotKeys can
        ///     be specified even if NoLegacy is not specified and WindowHandle is NULL.
        /// </summary>
        NOHOTKEYS = 0x00000200,

        /// <summary>If set, application keys are handled.  NoLegacy must be specified.  Keyboard only.</summary>
        APPKEYS = 0x00000400,

        /// If set, this enables the caller to receive input in the background only if the foreground application
        /// does not process it. In other words, if the foreground application is not registered for raw input,
        /// then the background application that is registered will receive the input.
        /// </summary>
        EXINPUTSINK = 0x00001000,
        DEVNOTIFY = 0x00002000
    }
}