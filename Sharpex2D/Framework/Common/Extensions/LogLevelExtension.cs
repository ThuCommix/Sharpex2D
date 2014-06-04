using System;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Common.Extensions
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
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
                case LogLevel.Engine:
                    return "Engine";
            }

            throw new InvalidOperationException("Enum definition not found.");
        }
    }
}