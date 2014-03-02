using System;
using SharpexGL.Framework.Common.Debug;

namespace SharpexGL.Framework.Common.Extensions
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
