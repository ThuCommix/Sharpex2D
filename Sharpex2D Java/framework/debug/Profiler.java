package sharpex2d.framework.debug;

import java.lang.reflect.InvocationTargetException;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import sharpex2d.framework.common.java.Action;
import sharpex2d.framework.debug.logging.Log;
import sharpex2d.framework.debug.logging.LogLevel;
import sharpex2d.framework.debug.logging.LogMode;

public class Profiler {

	/**
	 * Initializes a new Profiler class.
	 */
	private Profiler()
	{
		
	}
	
	/**
	 * Profiles an action.
	 * @param guid The Guid.
	 * @param action The Action.
	 */
	public static void Profile(UUID guid, Action action)
	{
		long time = System.nanoTime();
		
		Log.Next("Profiling: " + guid, LogLevel.Info, LogMode.StandardOut);
		
		try {
			action.invoke();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalArgumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		long elapsed = TimeUnit.NANOSECONDS.toMillis(System.nanoTime() - time);
		
		Log.Next("End profiling: " + guid + " Time: " + elapsed + "ms", LogLevel.Info, LogMode.StandardOut);
		
	}
}
