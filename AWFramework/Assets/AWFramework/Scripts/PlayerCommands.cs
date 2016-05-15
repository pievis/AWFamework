using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using AWFramework;

/// <summary>
/// Base class that enables player commands across the HLAPI networking system.
/// All commands sent trough this component are act as a functor for the player/user.
/// </summary>
public class PlayerCommands : NetworkBehaviour
{
	ScreenLogger logger;
	AWNetworkManager netManager;

	//IMPORTANT - Inject the reference of the local player game object
	//Can only be done here
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		AWConfig.getInstance ().SetLocalPlayer (this.gameObject);
		Debug.Log ("Saved instance of the local player");
	}

	// Use this for initialization
	void Start ()
	{
		logger = ScreenLogger.getLogger ();
		netManager =(AWNetworkManager) AWNetworkManager.singleton;
	}

	/////
	// Commands
	//See@: http://docs.unity3d.com/Manual/UNetActions.html
	////

	[Command]
	void CmdSpawnObj (GameObject placingObject)
	{
		if (placingObject == null) {
			Log ("Can't spawn something that I don't have");
			return;
		}
		RpcLog ("Spawning: " + placingObject.name);
		NetworkServer.Spawn (placingObject);
	}

	[Command]
	void CmdSpawnPrefab (string name, Vector3 position, Quaternion rotation)
	{
		//get the prefab from the resource folder
		Debug.Log ("searching for: " + "prefabs/" + name);
		GameObject prefab = Resources.Load ("prefabs/" + name, typeof(GameObject)) as GameObject;
		GameObject placingObject = null;
		if (prefab != null) {
			placingObject = Instantiate (prefab, position, rotation) as GameObject;
			placingObject.transform.parent = AWConfig.getWorldTransform ();
		}
		if (placingObject == null) {
			Log ("Can't spawn something that I don't have - " + name);
			return;
		}
		RpcLog ("Spawning: " + placingObject.name);
		NetworkServer.Spawn (placingObject);
	}

	[Command]
	void CmdInteract (GameObject go, string method)
	{
		HologramComponent hc = go.GetComponent<HologramComponent> ();
		if(hc != null){
			hc.Invoke(method);
		}else {
				Debug.LogWarning ("Can't find HologramComponent for " + go.name
				                  + " using SendMessage insetead");
				go.SendMessage(method);
		}
	}

	[Command]
	void CmdDestroy (GameObject go)
	{
		if (go != null)
			NetworkServer.Destroy (go);
		else
			Log ("Asked to destroy something, but nothing was found");
	}

	public static void OnGameObjectInteraction (GameObject go , string method, object[] args)
	{
		HologramComponent hc = go.GetComponent<HologramComponent>();
		if(hc != null){
			hc.Invoke(method, args);
		}else {
			if(args != null && args.Length > 0){
				go.SendMessage(method, args[0]);
			} else{
				Debug.LogWarning ("Can't find HologramComponent for " + go.name
				                  + " using SendMessage insetead");
				go.SendMessage(method);
			}
		}
	}

	//Exposed methods called by the client

	/// <summary>
	/// Asks the server to destroy an object trough the network.
	/// Important: Object should have a netid - NetworkIdentity
	/// </summary>
	/// <param name="go">Go.</param>
	public void AskDestroy (GameObject go)
	{
		CmdDestroy (go);
	}

	/// <summary>
	/// Asks the spawn of an object.
	/// </summary>
	/// <param name="obj">GameObject with netid.</param>
	public void AskSpawnObj (GameObject obj)
	{
		CmdSpawnObj (obj);
	}

	/// <summary>
	/// Asks the remote server to spawn an object (form a prefab)
	/// </summary>
	/// <param name="name">Prefab name.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	public void AskSpawnObj (string name, Vector3 position, Quaternion rotation)
	{
		CmdSpawnPrefab (name, position, rotation);
	}

	/// <summary>
	/// Interact with the passed object calling the specified method name.
	/// </summary>
	/// <param name="go">GameObject reference with NetworkIdentity.</param>
	/// <param name="method">Method name.</param>
	public void Interact (GameObject go, string method)
	{
		CmdInteract (go, method);
	}

	/// <summary>
	/// Interact with the passed object calling the specified method name.
	/// </summary>
	/// <param name="go">GameObject reference with NetworkIdentity.</param>
	/// <param name="method">Method name.</param>
	/// <param name="args">Serializable parameters</param>
	public void Interact (GameObject go, string method, params object[] args)
	{
		if (isClient) {
			netManager.SendInteractionMessage (go, method, args);
		} else if(isServer) {
			OnGameObjectInteraction(go, method, args);
		}
	}

	//********************************
	//Other
	void Log (string text)
	{
		logger.ShowMsg ("Player]" + text);
		Debug.Log ("Player] " + text);
	}

	[ClientRpc]
	void RpcLog (string text)
	{
		logger.ShowMsg ("Server]" + text);
		Debug.Log ("Server] " + text);
	}
}
