using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AWNetworkManager : NetworkManager
{

	ScreenLogger logger;
	
	void Log (string str)
	{
		if (logger == null)
			logger = ScreenLogger.getLogger ();
		str = "Settings] " + str;
		logger.ShowMsg (str);
		Debug.Log (str);
	}
	
	void printError (int errorCode)
	{
		NetworkError code = (NetworkError)errorCode;
		switch (code) {
		case NetworkError.Timeout:
			Log ("*** Timeout");
			break;
		case NetworkError.WrongHost:
			Log ("*** Unable to reach the host");
			break;
		case NetworkError.NoResources:
			Log ("*** Missing resources");
			break;
		}
	}

	bool isServer = false;
	public bool IsServer ()
	{
		return isServer;
	}

	//SERVER SIDE
	
	//Called when a client connects
	public override void OnServerConnect (NetworkConnection conn)
	{
		base.OnServerConnect (conn);
		Log ("A new client has connected: " + conn.address);
	}
	
	public override void OnServerDisconnect (NetworkConnection conn)
	{
		base.OnServerDisconnect (conn);
		Log ("Client left the room: " + conn.address);
	}
	
	public override void OnServerError (NetworkConnection conn, int errorCode)
	{
		base.OnServerError (conn, errorCode);
		Log ("***NETWORK ERROR*** " + conn.address + " code: " + errorCode);
		printError (errorCode);
	}

	//CLIENT SIDE
	
	// called when connected to a server
	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);
		Log ("Connected to server");
	}
	
	// called when disconnected from a server
	public override void OnClientDisconnect (NetworkConnection conn)
	{
		base.OnClientDisconnect (conn);
		Log ("Disconnected form server");
	}
	
	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
		Log ("***NETWORK ERROR*** " + conn.address + " code: " + errorCode);
		printError (errorCode);
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();
		isServer = true;
		//Handlers registration
		NetworkServer.RegisterHandler (INTERACTION_MSG_ID, OnGameObjectInteraction);
		//Enable all HoloDoer
		HoloDoerComponent[] hdoers = Object.FindObjectsOfType<HoloDoerComponent>();
		foreach(HoloDoerComponent hd in hdoers){
			hd.enabled = true;
		}
	}

	//************
	//Message Handling

	public const short INTERACTION_MSG_ID = 888;
	public const short ASKSTATE_MSG_ID = 889;

	public void SendInteractionMessage (GameObject go, string method, object[] args)
	{
		NetworkIdentity ni = go.GetComponent<NetworkIdentity> ();
		if (ni == null) {
			Log ("No network Identity found on the object");
			return;
		}
		uint netId = ni.netId.Value;
		InteractionMessage msg = new InteractionMessage (netId, method, args);
		if (!client.Send (INTERACTION_MSG_ID, msg)) {
			Log ("Problem while sending msg:\n " + msg.ToString ());
		}
	}
	
	//Callback (Executed locally on the server)
	public void OnGameObjectInteraction (NetworkMessage netMsg)
	{
		InteractionMessage msg = netMsg.ReadMessage<InteractionMessage> ();
		Debug.Log ("Message received " + msg.ToString ());
		NetworkInstanceId netId = new NetworkInstanceId (msg.netId);
		GameObject go = NetworkServer.FindLocalObject (netId);
		PlayerCommands.OnGameObjectInteraction (go, msg.method, msg.args);
	}

//	void Update ()
//	{
//		if (Input.GetKeyDown (KeyCode.A)) {
//			foreach (NetworkMessageDelegate nmd in NetworkServer.handlers.Values) {
//				Debug.Log (nmd.Method.Name);
//			}
//		}
//	}
}
