using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class InteractionMessage : MessageBase
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