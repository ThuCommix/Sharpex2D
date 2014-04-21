package sharpex2d.framework.common.java;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

public class Action {

	private Method _method;
	private Object _target;
	
	/**
	 * Initializes a new Action class.
	 * @param obj The Object.
	 * @param methodName The Methodname.
	 * @throws SecurityException 
	 * @throws NoSuchMethodException 
	 */
	public Action(Object obj, String methodName) throws NoSuchMethodException, SecurityException
	{
		_method = obj.getClass().getDeclaredMethod(methodName, new Class[] {});
		_target = obj;
	}
	
	/**
	 * Invokes the Action.
	 * @throws InvocationTargetException 
	 * @throws IllegalArgumentException 
	 * @throws IllegalAccessException 
	 */
	public void invoke() throws IllegalAccessException, IllegalArgumentException, InvocationTargetException
	{
		_method.invoke(_target, new Object[] {});
	}
}
