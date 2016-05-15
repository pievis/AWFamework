using System.Collections;
using System.Reflection;
using AWFramework;

/**
 * Hologram structure and behoviour exposed
 **/
public class Hologram : IHologram
{

	public IView view;
	public IModel model;
	public INetworkSync networkSync;
	
	public void SetView (IView view)
	{
		this.view = view;
	}
	
	public void SetNetworkSync (INetworkSync netSync)
	{
		this.networkSync = netSync;
	}
	
	public void SetModel (IModel model)
	{
		this.model = model;
	}

	public void Invoke(string name, object[] args){
		MethodInfo vMethod = view.GetType().GetMethod(name);
		if(vMethod != null)
			vMethod.Invoke(view, args);
		MethodInfo mMethod = view.GetType().GetMethod(name);
		if(mMethod != null)
			mMethod.Invoke(model, args);
		MethodInfo nsMethod = networkSync.GetType().GetMethod(name);
		if(nsMethod != null)
			nsMethod.Invoke(networkSync, args);
	}
}
