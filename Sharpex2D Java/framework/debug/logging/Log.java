package sharpex2d.framework.debug.logging;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import sharpex2d.framework.common.extensions.LogLevelExtensions;

public class Log {

	/**
	 * Initializes a new Log class.
	 */
	static
	{
		Entries = new ArrayList<LogEntry>();
	}
	
	private static List<LogEntry> Entries;
	
	/**
	 * Writes a new log entry.
	 * @param message The Message.
	 * @param level The Level.
	 */
	public static void Next(String message, LogLevel level)
	{
		Next(message, level, LogMode.None);
	}
	/**
	 * Writes a new log entry.
	 * @param message The Message.
	 * @param level The Level.
	 * @param mode The LogMode.
	 */
	public static void Next(String message, LogLevel level, LogMode mode)
	{
		Date date = new Date();
		
		LogEntry log = new LogEntry(message, level, date);
		
		Entries.add(log);
		
		if(mode == LogMode.StandardOut)
		{
			System.out.println(log.getDate().toString() + " [" + LogLevelExtensions.toString(level) + "]: " + log.getMessage());
		}
		
		if(mode == LogMode.StandardError)
		{
			System.err.println(log.getDate().toString() + " [" + LogLevelExtensions.toString(level) + "]: " + log.getMessage());
		}
		
	}
	/**
	 * Gets all entries.
	 * @return Array of LogEntry.
	 */
	public static LogEntry[] GetEntries()
	{
		return (LogEntry[]) Entries.toArray();
	}
}
