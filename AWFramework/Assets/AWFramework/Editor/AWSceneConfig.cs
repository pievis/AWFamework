using UnityEngine;
using System.Collections;
using AWFramework;

public static class AWSceneConfig
{

	public static ARSystem arSystem = ARSystem.Vuforia;
	public static NetSystem netSystem = NetSystem.HLAPI;

	/*
	 *	Setups the scene for the framework
	 */
	public static void SetupScene ()
	{
		SetupAWControlCenter ();
	}

	static void SetupAWConfig (GameObject controlCenter)
	{
		AWConfig config = controlCenter.GetComponent<AWConfig>();
		config.netSystem = netSystem;
		config.arSystem = arSystem;
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
