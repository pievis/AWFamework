using UnityEngine;
using System.Collections;

namespace AWFramework
{
	//Enums
	public enum ARSystem
	{
		Vuforia,
		NONE }
	;
	
	public enum NetSystem
	{
		HLAPI,
		NONE }
	;

	public class AWConfig : MonoBehaviour
	{
		//Scene / Framework relative configuration
		public const string AW_CONFIG_GO_NAME = "AWControlCenter";
		public const string AW_WORLD_GO_NAME = "_world";

		//Properties
		public ARSystem arSystem = ARSystem.Vuforia;
		public NetSystem netSystem = NetSystem.HLAPI;

		//static
		static AWConfig instance;
		GameObject worldGO;
		GameObject localPlayer;
		IEventContext mainEventContext;

		//Singleton - Only the monobehaviour instance attached to this GO should be used
		public static AWConfig GetInstance ()
		{
			if (instance == null) {
				instance = GameObject.Find (AW_CONFIG_GO_NAME).GetComponent<AWConfig> ();
			}
			return instance;
		}

		//
		void Awake ()
		{
			Bind ();
		}

		/**
		 * Binds essentials objects at runtime
		 */
		void Bind ()
		{
			worldGO = GameObject.Find (AW_WORLD_GO_NAME);
			IEventContext mec = GetComponent<MonoEventContext> ();
			if (mec == null) {
				mec = gameObject.AddComponent<MonoEventContext> ();
			}
			mainEventContext = mec;
		}

		/// <summary>
		/// Gets the world transform (position, rotation, size).
		/// </summary>
		/// <returns>The world transform.</returns>
		public Transform GetWorldTransform ()
		{
			return worldGO.GetComponent<Transform> ();
		}

		/// <summary>
		/// Set the reference of the local player for the client application.
		/// </summary>
		/// <param name="player">Player GameObject</param>
		public void SetLocalPlayer (GameObject player)
		{
			localPlayer = player;
		}
		/// <summary>
		/// Gets the local player instance for this player of which he has the authority.
		/// </summary>
		/// <returns>The local player or null if not setted yet.</returns>
		public GameObject GetLocalPlayer ()
		{
			return localPlayer;
		}

		/// <summary>
		/// Gets the main event context shared by the whole application.
		/// </summary>
		/// <value>The main event context.</value>
		public IEventContext MainEventContext {
			get {
				return this.mainEventContext;
			}
		}

		public static bool IsServer ()
		{
			switch (AWConfig.GetInstance ().netSystem) {
			case NetSystem.HLAPI:
				return ((AWNetworkManager)AWNetworkManager.singleton).IsServer ();
			case NetSystem.NONE:
				return true;
			}
			return false;
		}
	}

}