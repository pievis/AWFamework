using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;

public static class AWFactory
{
	
	public static GameObject CreateAWObject (Vector3 position, Quaternion rotation)
	{
		GameObject go = getBasicObject ("hologram", position, rotation);
		go.AddComponent<HologramComponent> ();
		if(AWSceneConfig.netSystem == NetSystem.HLAPI){
			go.AddComponent<NetworkIdentity>();
			go.AddComponent<NetworkTransform>();
		}
		return go;
	}

	static Transform getWorld ()
	{
		return GameObject.Find (AWConfig.AW_WORLD_GO_NAME).transform;
	}

	static GameObject getBasicObject (string name, 
	                          Vector3 position,
	                          Quaternion rotation)
	{
		GameObject go = new GameObject (name);
		go.transform.position = position;
		go.transform.rotation = rotation;
		go.transform.parent = getWorld ();
		return go;
	}
}
