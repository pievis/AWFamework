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
		static GameObject worldGO;
		static GameObject localPlayer;
		static IEventContext mainEventContext; //TODO setup

		//Singleton - Only the monobehaviour instance attached to this GO should be used
		public static AWConfig getInstance ()
		{
			if (instance == null) {
				instance = GameObject.Find (AW_CONFIG_GO_NAME).GetComponent<AWConfig> ();
			}
			return instance;
		}

		//
		void Start ()
		{
			Bind ();
		}

		/**
		 * Binds essentials objects at runtime
		 */
		void Bind ()
		{
			worldGO = GameObject.Find (AW_WORLD_GO_NAME);
		}

		/// <summary>
		/// Gets the world transform (position, rotation, size).
		/// </summary>
		/// <returns>The world transform.</returns>
		public static Transform getWorldTransform(){
			return worldGO.GetComponent<Transform>();
		}

		/// <summary>
		/// Set the reference of the local player for the client application.
		/// </summary>
		/// <param name="player">Player GameObject</param>
		public void SetLocalPlayer(GameObject player){
			localPlayer = player;
		}
		/// <summary>
		/// Gets the local player instance for this player of which he has the authority.
		/// </summary>
		/// <returns>The local player or null if not setted yet.</returns>
		public GameObject GetLocalPlayer(){
			return localPlayer;
		}
	}

}