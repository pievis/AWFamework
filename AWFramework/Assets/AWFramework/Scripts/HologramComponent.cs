using UnityEngine;
using System.Collections;
using System.Reflection;
using AWFramework;

/**
 * Specification of the Hologram concept based on Unity Monobehaviour Component.
 * This object is meant to be static.
 **/
public class HologramComponent : MonoBehaviour, IHologram
{

	IView view;
	IModel model;
	INetworkSync networkSync;
	
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

	public void Invoke (string name)
	{
		Invoke (name, null);
	}
	
	public void Invoke (string name, params object[] args)
	{
		System.Type[] types = {};
		if (args != null) {
			types = new System.Type[args.Length];
			for (int i = 0; i < args.Length; i++) {
				types [i] = args [i].GetType ();
			}
		}

		MethodInvoke (view, name, types, args);
		MethodInvoke (model, name, types, args);
		MethodInvoke (networkSync, name, types, args);

//		Log ("Invoke: " + name);
	}

	void MethodInvoke (object target, string method,
	                  System.Type[] types, params object[] args)
	{
		if (target == null || method.Length == 0)
			return;

		MethodInfo vMethod = target.GetType ().GetMethod (method, types);
		if (vMethod != null) {
			vMethod.Invoke (target, args);
		} else {
			Log ("method " + method + " not found for " + target.GetType ());
		}
	}

	void Bind ()
	{
		view = GetComponent<IView> ();
		model = GetComponent<IModel> ();
		networkSync = GetComponent<INetworkSync> ();
	}

	void Awake ()
	{
		Bind ();
	}

	void Log (string text)
	{
		Debug.Log (gameObject.name + "] " + text);
	}

}
