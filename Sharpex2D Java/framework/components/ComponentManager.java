package sharpex2d.framework.components;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import sharpex2d.framework.common.java.GenericHelper;

public class ComponentManager implements IComponent {
	
	/**
	 * Gets the Guid.
	 */
	public UUID getGuid()
	{
		return UUID.fromString("6A3D114D-6DF4-429E-82ED-F7CD0AE29CF8");
	}
	
	/**
	 * Constructs the component.
	 */
	public void construct()
	{
		for(int i = 0; i<= _components.size() - 1; i++)
		{
			IComponent component = _components.get(i);
			if(component instanceof IConstructable)
			{
				((IConstructable)component).construct();
			}
		}
		
		_isAlreadyConstructed = true;
	}

	/**
	 * Initializes a new ComponentManager class.
	 */
	public ComponentManager()
	{
		_components = new ArrayList<IComponent>();
	}
	
	private boolean _isAlreadyConstructed;
	
	private List<IComponent> _components;
	
	/**
	 * Adds a new Component.
	 * @param component The Component.
	 */
	public void addComponent(IComponent component)
	{
		_components.add(component);
		
		if(!_isAlreadyConstructed)
		{
			if(component instanceof IConstructable)
			{
				((IConstructable)component).construct();
			}
		}
	}
	/**
	 * Removes the Component.
	 * @param component The Component.
	 */
	public void removeComponent(IComponent component)
	{
		if(_components.contains(component))
		{
			_components.remove(component);
		}
	}
	
	@SuppressWarnings("unchecked")
	/**
	 * Gets the Component by guid.
	 * @param guid The Guid.
	 * @return Component.
	 */
	public <T> T getByGuid(UUID guid)
	{
		for(int i = 0; i<= _components.size() -1; i++)
		{
			IComponent component = _components.get(i);
			if(component.getGuid() == guid)
			{
				return (T)component;
			}
		}
		
		throw new IllegalArgumentException("The type if the guid " + guid + " could not be found.");
	}
	/**
	 * Gets the Component.
	 * @return Component.
	 */
	@SuppressWarnings("unchecked")
	public <T> T Get()
	{
		for(int i = 0; i<= _components.size() -1; i++)
		{
			if(GenericHelper.<T>getType() == _components.get(i).getClass())
			{
				return (T)_components.get(i);
			}
		}
		
		//try to query interfaces
		
		for(int i = 0; i<= _components.size() -1; i++)
		{
			if(QueryInterface(_components.get(i), GenericHelper.<T>getType()))
			{
				return (T)_components.get(i);
			}
		}
		
		throw new IllegalArgumentException("The type " + GenericHelper.<T>getType().toString() + " could not be found.");
	}
	/**
	 * Queries a Type.
	 * @param obj The Object.
	 * @param target The Target.
	 * @return Returns true if the object contains the specified class.
	 */
	@SuppressWarnings("rawtypes")
	private boolean QueryInterface(Object obj, Class target)
	{
		return target.isInstance(obj);
	}
}
