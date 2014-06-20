using System;

namespace Sharpex2D
{
    public class Platform
    {
        /// <summary>
        ///     Gets the current CLR runtime.
        /// </summary>
        /// <returns>PlatformType.</returns>
        public static bool IsMonoRuntime()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}