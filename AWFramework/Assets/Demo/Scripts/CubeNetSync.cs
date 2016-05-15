using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;

public class CubeNetSync : HLAPINetworkSync, INetworkSync {

	CubeView view;
//	CubeModel model;

	void Start(){
		view = GetComponent<CubeView>();
//		model = GetComponent<CubeModel>();
	}

	[ClientRpc]
	public void RpcSetColor(Color color){
		view.SetColor(color);
	}
	[ClientRpc]
	public void RpcRotate(Vector3 axis, float degree){
		view.Rotate(axis, degree);
	}

	//Shared hologram methods
	
	public void SetColor(Color color){
		int colorInt = color.Equals(Color.red) ? 1 : 0;
		SetColor(colorInt);
	}

	public void SetColor(int colorInt){
		Color color = colorInt == 1 ? Color.red : Color.blue;
		if(isServer){
			//send view update information to all clients
			ScreenLogger.getLogger().ShowMsg("called from server " + color.ToString());
			RpcSetColor(color);
		}
		if(isClient){
			ScreenLogger.getLogger().ShowMsg("called from client" + color.ToString());
			//send the command to the server
			SendCmd("SetColor", colorInt);
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
}
