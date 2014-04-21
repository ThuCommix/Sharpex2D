package sharpex2d.framework.common.java;

public class GenericHelper {

	/**
	 * Initializes a new GenericHelper class.
	 */
	private GenericHelper()
	{
		
	}
	
	/**
	 * Gets the Type of a generic.
	 * @param param The Generic.
	 * @return Type.
	 */
	@SuppressWarnings("rawtypes")
	public static <T> Class getType(T... param)
	{
		return param.getClass().getComponentType();
	}
}
