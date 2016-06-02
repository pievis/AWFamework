using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;

public class CubeNetSync : HLAPINetworkSync, INetworkSync {

	CubeView view;
	CubeModel model;

	void Start(){
		view = GetComponent<CubeView>();
		model = GetComponent<CubeModel>();
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer();
		AskCurrentState(); //Ask the server for the current state message
	}

	[ClientRpc]
	public void RpcSetColor(Color color){
		view.SetColor(color);
	}
	[ClientRpc]
	public void RpcRotate(Vector3 axis, float degree){
		view.Rotate(axis, degree);
	}
	[ClientRpc]
	public void RpcJump(){
		view.Jump();
	}

	//Shared hologram methods

	public void SetColor(Color color){
		if(isServer){
			//send view update information to all clients
			ScreenLogger.getLogger().ShowMsg("called from server " + color.ToString());
			RpcSetColor(color);
		}
		if(isClient){
			ScreenLogger.getLogger().ShowMsg("called from client" + color.ToString());
			//send the command to the server
			SendCmd("SetColor", color);
		}
	}

	public void Jump(){
		if(isServer){
			//send view update information to all clients
			ScreenLogger.getLogger().ShowMsg("called from server: JUMP");
			RpcJump();
		}
	}

	public void Rotate(Vector3 axis, float degree){
		if(isServer){
			ScreenLogger.getLogger().ShowMsg("called from server: ROTATE " + degree);
			RpcRotate(axis, degree);
		}
		if(isClient){
			ScreenLogger.getLogger().ShowMsg("called from client: ROTATE " + degree);
			SendCmd("Rotate", axis, degree);
		}
	}

	/// <summary>
	/// Handler that processes the current state message received from the server
	/// after AskCurrentState is called.
	/// </summary>
	/// <param name="msg">Message.</param>
	public override void OnCurrentStateReceived (NetworkMessage msg)
	{
		StateMessage sm = msg.ReadMessage<StateMessage>();
		Debug.Log ("state message received " + sm.ToString() + " " + gameObject.name);
		Color c = (Color) sm.GetValue("color");
		Debug.Log ("color " + sm.ToString() + " " + gameObject.name);
		view.SetColor(c);
	}

	/// <summary>
	/// Gets the current state of the object from the model.
	/// </summary>
	/// <returns>The current state message.</returns>
	public override StateMessage GetCurrentState ()
	{
		StateMessage sm = new StateMessage();
		sm.SetValue("color", model.color);
		return sm;
	}

}
