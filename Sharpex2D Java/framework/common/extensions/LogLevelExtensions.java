package sharpex2d.framework.common.extensions;

import sharpex2d.framework.debug.logging.LogLevel;

public class LogLevelExtensions {

	public static String toString(LogLevel level)
	{
		switch(level)
		{
		case Info:
			return "Info";
		case Warning:
			return "Warning";
		case Error:
			return "Error";
		case Critical:
			return "Critical";
		}
		
		return "";
	}
}
