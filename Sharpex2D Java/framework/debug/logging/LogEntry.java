package sharpex2d.framework.debug.logging;

import java.util.Date;

public class LogEntry {

	/**
	 * Initializes a new LogEntry class.
	 * @param message The Message.
	 * @param level The Level.
	 * @param date The Date.
	 */
	public LogEntry(String message, LogLevel level, Date date)
	{
		_message = message;
		_level = level;
		_time = date;
	}
	
	private String _message;
	private LogLevel _level;
	private Date _time;
	
	/**
	 * Gets the Message.
	 * @return String.
	 */
	public String getMessage()
	{
		return _message;
	}
	/**
	 * Gets the LogLevel.
	 * @return LogLevel.
	 */
	public LogLevel getLevel()
	{
		return _level;
	}
	/**
	 * Gets the Date.
	 * @return Date.
	 */
	public Date getDate()
	{
		return _time;
	}
}
