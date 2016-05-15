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

	public void Invoke(string name){
		Invoke(name, null);
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

		MethodInfo vMethod = view.GetType ().GetMethod (name, types);
		if (vMethod != null)
			vMethod.Invoke (view, args);
		MethodInfo mMethod = model.GetType ().GetMethod (name, types);
		if (mMethod != null)
			mMethod.Invoke (model, args);
		MethodInfo nsMethod = networkSync.GetType ().GetMethod (name, types);
		if (nsMethod != null)
			nsMethod.Invoke (networkSync, args);
		Log ("Invoke: " + name);
	}

	void Bind ()
	{
		view = GetComponent<IView> ();
		model = GetComponent<IModel> ();
		networkSync = GetComponent<INetworkSync> ();
	}

	void Start ()
	{
		Bind ();
	}

	void Log (string text)
	{
		Debug.Log (gameObject.name + "] " + text);
	}

}
