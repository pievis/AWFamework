using UnityEngine;
using UnityEditor;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;

[CustomEditor(typeof(HologramComponent))]
public class HologramComponentEditor : Editor
{
	HologramComponent hc;
	GameObject go;
//	GUILayoutOption[] guiOptions = {};

	void OnEnable ()
	{
		hc = (HologramComponent)target;
		go = hc.gameObject;
	}

	public override void OnInspectorGUI ()
	{
		DisplayConfigInformation();
		ComponentSetup();
		//
		DrawDefaultInspector();
	}

	void ComponentSetup(){
		//Unity dropped support to class injection by interface type
		//		hc.view = (IView) EditorGUILayout.ObjectField("View", hc.view as Object, typeof(IView), false);
		//		hc.model =(IModel) EditorGUILayout.ObjectField("Model", hc.model as Object, typeof(IModel), false);
		//		hc.networkSync =(INetworkSync) EditorGUILayout.ObjectField("Network Sync", hc.networkSync as Object, typeof(INetworkSync));
		IView view = go.GetComponent<IView>();
		IModel model = go.GetComponent<IModel>();
		INetworkSync netSync = go.GetComponent<INetworkSync>();
		if(view == null){
			EditorGUILayout.HelpBox ("Can't find proper View Component assigned to this gameobject",
			                         MessageType.Warning, true);
		}
		if(model == null){
			EditorGUILayout.HelpBox ("Can't find proper Model Component assigned to this gameobject",
			                         MessageType.Warning, true);
		}
		if(netSync == null){
			EditorGUILayout.HelpBox ("Can't find proper NetworkSync Component assigned to this gameobject",
			                         MessageType.Warning, true);
		}
	}

	void DisplayConfigInformation(){
		bool check = true;
		
		//Vuforia config check
		if (AWSceneConfig.arSystem == ARSystem.Vuforia) {
			if (!CheckParent ()) {
				check = false;
				EditorGUILayout.HelpBox ("This gameobject needs to be child of "
				                         + AWConfig.AW_WORLD_GO_NAME + " to ensure space cooupling", MessageType.Warning, true);
			}
		}
		
		//HLAPI Config check
		if (AWSceneConfig.netSystem == NetSystem.HLAPI) {
			if (!CheckPrefab ()) {
				check = false;
				EditorGUILayout.HelpBox ("This gameobject needs to be a prefab", MessageType.Warning, true);
			}
			if (!CheckHLAPISynch ()) {
				check = false;
				EditorGUILayout.HelpBox ("There is a problem with the networking state " +
				                         "synchronization mechanism. Probably missing NetworkIdentity or NetworkBehaviour components.",
				                         MessageType.Warning, true);
			}
		}
		
		//
		if (check) {
			EditorGUILayout.LabelField ("This component is configured correctly.");
		}
	}

	bool CheckParent ()
	{
		Transform parent = hc.transform.parent;
		if (parent == null)
			return false;
		return parent.name.Equals (AWConfig.AW_WORLD_GO_NAME);
	}

	bool CheckPrefab ()
	{
		return PrefabUtility.GetPrefabParent (hc.gameObject) != null;
	}

	bool CheckHLAPISynch ()
	{
		bool check = true;
		check = hc.GetComponent<NetworkIdentity>() != null;
		check = check && hc.GetComponent<NetworkBehaviour>() != null;
		return check;
	}

}
