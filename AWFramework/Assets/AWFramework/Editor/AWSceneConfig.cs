using UnityEngine;
using System.Collections;
using AWFramework;
using Vuforia;

public static class AWSceneConfig
{

	public static ARSystem arSystem = ARSystem.Vuforia;
	public static NetSystem netSystem = NetSystem.HLAPI;

	/*
	 *	Setups the scene for the framework
	 */
	public static void SetupScene ()
	{
		if(arSystem == ARSystem.NONE){
			DisableVuforia();
		}
		if(arSystem == ARSystem.Vuforia){
			EnableVuforia();
		}
		SetupAWControlCenter ();
	}

	/// <summary>
	/// Setups the scene from the editor and popups warning if some
	/// configuration problem is found.
	/// </summary>
	/// <param name="controlCenter">Control center.</param>
	static void SetupAWConfig (GameObject controlCenter)
	{
		AWConfig config = controlCenter.GetComponent<AWConfig>();
		config.netSystem = netSystem;
		config.arSystem = arSystem;
		if(netSystem == NetSystem.HLAPI){
			AWNetworkManager netManager = controlCenter.GetComponent<AWNetworkManager>();
			if(netManager == null){
				Debug.LogWarning("Networking system setted to HLAPI, " +
					"but no AWNetworkManager found on AWControlCenter");
			}
		}
		if(arSystem == ARSystem.Vuforia){
			GameObject arCameraGO = GameObject.Find("ARCamera");
			if(arCameraGO == null || !arCameraGO.activeInHierarchy){
				Debug.LogWarning("ARCamera for Vuforia not found or inactive, " +
				                 " remember to configure it properly.");
			} else {
				VuforiaBehaviour vb = arCameraGO.GetComponent<VuforiaBehaviour>();
				if(vb.AppLicenseKey.Length < 2){
					Debug.LogWarning("Is the VuforiaBehaviour License Key setted correctly?");
				}
			}
		}
	}

	static void EnableVuforia(){
		DefaultTrackableEventHandler dteh = Object.FindObjectOfType<DefaultTrackableEventHandler>();
		if(dteh != null)
			dteh.enabled = true;
		GameObject arCameraGO = GameObject.Find("ARCamera");
		if(arCameraGO != null)
			arCameraGO.SetActive(true);
		CheckDebugCamera(false);
	}
	static void DisableVuforia(){
		DefaultTrackableEventHandler dteh = Object.FindObjectOfType<DefaultTrackableEventHandler>();
		if(dteh != null)
			dteh.enabled = false;
		GameObject arCameraGO = GameObject.Find("ARCamera");
		if(arCameraGO != null)
			arCameraGO.SetActive(false);
		CheckDebugCamera(true);
	}

	static void CheckDebugCamera(bool active){
		GameObject debugCamera = GameObject.Find("DebugCamera");
		if(debugCamera != null)
			debugCamera.SetActive(active);
	}

	static void SetupAWControlCenter ()
	{
		GameObject cc = GameObject.Find (AWConfig.AW_CONFIG_GO_NAME);
		if (cc == null) {
			Debug.Log ("Todo: Object check and creation");
		}
		SetupAWConfig(cc);
	}
}
