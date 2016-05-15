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

	//************
	//Message Handling

	const short INTERACTION_MSG_ID = 888;

	public override void OnStartServer ()
	{
		base.OnStartServer ();
		//Handlers registration
		NetworkServer.RegisterHandler (INTERACTION_MSG_ID, OnGameObjectInteraction);
	}

	
	class InteractionMessage : MessageBase
	{
		public uint netId;
		public string method;
		public object[] args;
		
		public InteractionMessage (uint netId, string method,
		                           object[] args)
		{
			this.netId = netId;
			this.method = method;
			this.args = args;
		}
		
		public InteractionMessage ()
		{
		}
		
		public override void Deserialize (NetworkReader reader)
		{
			netId = reader.ReadPackedUInt32 ();
			method = reader.ReadString ();
			ushort length = reader.ReadUInt16 ();
			args = new object[length];
			for (int i = 0; i < length; i++) {
				byte[] bytes = reader.ReadBytesAndSize ();
				args [i] = BinaryDataFormatter.FromBytes (bytes);
			}
		}
		
		public override void Serialize (NetworkWriter writer)
		{
			writer.WritePackedUInt32 (netId);
			writer.Write (method);
			writer.Write ((ushort)args.Length);

			for (int i = 0; i < args.Length; i++) {
				byte[] bytes;
				try {
					bytes = BinaryDataFormatter.ToBytes (args [i]);
					writer.WriteBytesFull (bytes);
				} catch (System.Exception se) {
					Debug.LogException (se);
				}

			}
		}

		override public string ToString ()
		{
			return "netID: " + netId + 
				" method_name: " + method + 
				" args_lenght: " + args.Length;
		}
	};

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
