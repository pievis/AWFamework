using UnityEngine;
using UnityEditor;
using System.Collections;
using AWFramework;
/*
 *	Unity Editor class used to configure general framework properties like:
 *	- Selected Newtworking System
 *	- Selected AR System
 *
 *	Also providing ad-hoc object creation.
 */
public class AWConfigWindow : EditorWindow
{

	[MenuItem("Window/AW Framework Config")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(AWConfigWindow));
	}

	[MenuItem("GameObject/AW Framework/Hologram")]
	public static void CreateAWObject(){
		AWFactory.CreateAWObject(Vector3.zero, Quaternion.identity);
	}

	ARSystem arSystem;
	NetSystem netSystem;

	void OnGUI ()
	{
		//Base Settings
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		arSystem = (ARSystem)EditorGUILayout.EnumPopup ("AR System", arSystem);
		netSystem = (NetSystem)EditorGUILayout.EnumPopup ("Networking System", netSystem);
		if (GUILayout.Button ("Change Settings")) {
			SetupARSystem (arSystem);
			SetupNetSystem (netSystem);
		}
		if (GUILayout.Button ("Setup Scene")) {
			AWSceneConfig.SetupScene();
		}
	}

	void SetupARSystem (ARSystem arSystem)
	{
		AWSceneConfig.arSystem = arSystem;
		Debug.Log ("AR System changed to " + AWSceneConfig.arSystem);
	}

	void SetupNetSystem (NetSystem netSystem)
	{
		AWSceneConfig.netSystem = netSystem;
		Debug.Log ("Networking System changed to " + AWSceneConfig.netSystem);
	}

}
