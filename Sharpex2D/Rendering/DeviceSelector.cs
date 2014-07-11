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
using System.Linq;
using Sharpex2D.Debug.Logging;
using Sharpex2D.Rendering.GDI;

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class DeviceSelector
    {
        public enum SelectorMode
        {
            /// <summary>
            ///     Selects the device with the lowest possible requirements.
            /// </summary>
            Lowest,

            /// <summary>
            ///     Selects the device with the highest possible requirements.
            /// </summary>
            Highest,

            /// <summary>
            ///     Selects the first capable device.
            /// </summary>
            Generic
        }

        private static readonly Logger Logger;

        /// <summary>
        ///     Initializes a new DeviceSelector class.
        /// </summary>
        static DeviceSelector()
        {
            Logger = LogManager.GetClassLogger();
        }

        /// <summary>
        ///     Gets the renderer based on the SelectorMode.
        /// </summary>
        /// <param name="devices">The RenderDevice Collection.</param>
        /// <param name="mode">The SelectorMode.</param>
        /// <param name="useSoftwarefallback">If no RenderDevice is available use a software renderer.</param>
        /// <returns>IRenderer.</returns>
        public static RenderDevice Select(RenderDevice[] devices, SelectorMode mode, bool useSoftwarefallback = false)
        {
            if (mode == SelectorMode.Lowest)
            {
                RenderDevice result = null;

                foreach (RenderDevice renderer in devices.Where(renderer => renderer.IsPlatformSupported))
                {
                    if (result == null)
                    {
                        result = renderer;
                    }
                    else
                    {
                        if (renderer.PlatformVersion < result.PlatformVersion)
                        {
                            result = renderer;
                        }
                    }
                }

                if (result == null)
                {
                    throw new NotSupportedException(
                        "None of the specified renderers are supported by the current platform.");
                }

                DeviceAttribute device;
                if (AttributeHelper.TryGetAttribute(result, out device))
                {
                    Logger.Engine("Selected Device: {0}", device.FriendlyName);
                }

                return result;
            }

            if (mode == SelectorMode.Highest)
            {
                RenderDevice result = null;

                foreach (RenderDevice renderer in devices.Where(renderer => renderer.IsPlatformSupported))
                {
                    if (result == null)
                    {
                        result = renderer;
                    }
                    else
                    {
                        if (renderer.IsPlatformSupported)
                        {
                            result = renderer;
                        }
                    }
                }

                if (result == null)
                {
                    throw new NotSupportedException(
                        "None of the specified renderers are supported by the current platform.");
                }

                DeviceAttribute device;
                if (AttributeHelper.TryGetAttribute(result, out device))
                {
                    Logger.Engine("Selected Device: {0}", device.FriendlyName);
                }

                return result;
            }

            if (mode == SelectorMode.Generic)
            {
                foreach (RenderDevice renderer in devices.Where(renderer => renderer.IsPlatformSupported))
                {
                    DeviceAttribute device;
                    if (AttributeHelper.TryGetAttribute(renderer, out device))
                    {
                        Logger.Engine("Selected Device: {0}", device.FriendlyName);
                    }

                    return renderer;
                }
            }

#if Windows

            if (useSoftwarefallback)
            {
                DeviceAttribute device;
                if (AttributeHelper.TryGetAttribute(typeof (GDIRenderDevice), out device))
                {
                    Logger.Engine("Selected Device: {0}", device.FriendlyName);
                }
                return new GDIRenderDevice(InterpolationMode.Linear, SmoothingMode.AntiAlias);
            }

#elif Mono
			Logger.Engine("Software fallback is not available on Mono.");
#endif

            throw new NotSupportedException("None of the specified renderers are supported by the current platform.");
        }
    }
}