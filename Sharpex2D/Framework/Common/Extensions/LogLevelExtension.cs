using System;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Common.Extensions
{
    public static class LogLevelExtension
    {
        public static string ToFriendlyString(this LogLevel mode)
        {
            switch (mode)
            {
                case LogLevel.Info:
                    return "Info";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Critical:
                    return "Critical";
            }

            throw new InvalidOperationException("Enum definition not found.");
        }
    }
}
