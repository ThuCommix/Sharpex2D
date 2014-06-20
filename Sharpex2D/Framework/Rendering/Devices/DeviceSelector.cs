using System;
using System.Linq;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Rendering.GDI;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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
                    Log.Next("Selected Device: {0}", LogLevel.Engine, device.FriendlyName);
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
                    Log.Next("Selected Device: {0}", LogLevel.Engine, device.FriendlyName);
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
                        Log.Next("Selected Device: {0}", LogLevel.Engine, device.FriendlyName);
                    }

                    return renderer;
                }
            }

            if (useSoftwarefallback)
            {
                DeviceAttribute device;
                if (AttributeHelper.TryGetAttribute(typeof (GDIRenderDevice), out device))
                {
                    Log.Next("Selected Device: {0}", LogLevel.Engine, device.FriendlyName);
                }
                return new GDIRenderDevice(InterpolationMode.Linear, SmoothingMode.AntiAlias);
            }

            throw new NotSupportedException("None of the specified renderers are supported by the current platform.");
        }
    }
}