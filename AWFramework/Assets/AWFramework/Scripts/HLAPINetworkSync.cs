using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;

/// <summary>
/// Network Synchronization base class used specifically for the HLAPI unity networking system.
/// </summary>
public abstract class HLAPINetworkSync : NetworkBehaviour, INetworkSync {

	private PlayerCommands cmd = null;

	protected PlayerCommands GetCmd(){
		if(cmd == null)
			cmd = AWConfig.getInstance().GetLocalPlayer().GetComponent<PlayerCommands>();
		return cmd;
	}

	/// <summary>
	/// Try interacting with this object trough player commands
	/// </summary>
	/// <param name="method">Method.</param>
	protected void SendCmd(string method){
		GetCmd().Interact(this.gameObject, method);
	}

	/// <summary>
	/// Try interacting with this object trough player commands
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="param">Serializable parameter</param>
	protected void SendCmd(string method, params object[] param){
		GetCmd().Interact(this.gameObject, method, param);
	}

}
