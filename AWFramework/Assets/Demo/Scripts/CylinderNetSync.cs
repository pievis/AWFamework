﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using AWFramework;

public class CylinderNetSync : HLAPINetworkSync, INetworkSync
{

	CylinderView view;

	public void Start ()
	{
		view = GetComponent<CylinderView> ();
	}

	public void Move(Vector3 dir)
	{
		if (isServer) {
			view.MoveLocal (dir);
		}
		if (isClient) {
			SendCmd ("Move", dir);
		}
	}

	//

	public override void OnCurrentStateReceived (NetworkMessage msg)
	{
		//nothing
	}

	public override StateMessage GetCurrentState ()
	{
		return null;
	}
}
