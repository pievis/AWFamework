using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Network Synchronization base class used specifically for the HLAPI unity networking system.
/// </summary>
public abstract class HLAPINetworkSync : NetworkBehaviour, INetworkSync
{

//	public short currentMsgIdOffset = 0;
	private PlayerCommands cmd = null;

	protected PlayerCommands GetCmd ()
	{
		if (cmd == null)
			cmd = AWConfig.GetInstance ()
				.GetLocalPlayer ()
				.GetComponent<PlayerCommands> ();
		return cmd;
	}

	/// <summary>
	/// Try interacting with this object trough player commands
	/// </summary>
	/// <param name="method">Method.</param>
	protected void SendCmd (string method)
	{
		GetCmd ().Interact (this.gameObject, method);
	}

	/// <summary>
	/// Try interacting with this object trough player commands
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="param">Serializable parameter</param>
	protected void SendCmd (string method, params object[] param)
	{
		GetCmd ().Interact (this.gameObject, method, param);
	}

	//AskCurrentState handling

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		NetworkManager.singleton
			.client.RegisterHandler (GetAskCSMsgId(),
			                        OnCurrentStateReceived);
	}

	public void AskCurrentState ()
	{
		GetCmd ().AskCurrentState (GetAskCSMsgId (),
		                            this.gameObject);
	}

	/// <summary>
	/// The message Id is guaranteed to be unique.
	/// </summary>
	/// <returns>AskForCurrentStateMessageId</returns>
	protected short GetAskCSMsgId ()
	{
		return (short)(GetInstanceID() + AWNetworkManager.ASKSTATE_MSG_ID);
	}

	/// <summary>
	/// Handler that processes the current state message received from the server
	/// after AskCurrentState is called.
	/// </summary>
	/// <param name="msg">Message.</param>
	public abstract void OnCurrentStateReceived (NetworkMessage msg);

	/// <summary>
	/// Get the current state message with information about server model instance.
	/// </summary>
	/// <returns>The current state.</returns>
	public abstract StateMessage GetCurrentState ();
}
