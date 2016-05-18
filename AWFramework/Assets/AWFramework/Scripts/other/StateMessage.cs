using UnityEngine;
using System.Collections;
using AWFramework;
using UnityEngine.Networking;
using System.Collections.Generic;

public class StateMessage : MessageBase
{
	public Dictionary<string, object> map;

	public StateMessage ()
	{
		map = new Dictionary<string, object> ();
	}
	
	public void SetValue (string key, object value)
	{
		map.Add (key, value);
	}

	public object GetValue (string key)
	{
		object obj;
		if (map.TryGetValue (key, out obj))
			return obj;
		else 
			return null;
	}

	public override void Deserialize (NetworkReader reader)
	{
		ushort length = reader.ReadUInt16 ();
		for (int i = 0; i < length; i++) {
			string key = reader.ReadString();
			byte[] bytes = reader.ReadBytesAndSize ();
			object value = BinaryDataFormatter.FromBytes (bytes);
			map.Add(key,value);
		}
	}
	
	public override void Serialize (NetworkWriter writer)
	{
		ushort count = (ushort) map.Keys.Count;
		writer.Write (count);	
		foreach(KeyValuePair<string, object> p in map){
			writer.Write(p.Key);
			byte[] bytes;
			try {
				bytes = BinaryDataFormatter.ToBytes (p.Value);
				writer.WriteBytesFull (bytes);
			} catch (System.Exception se) {
				Debug.LogException (se);
			}
		}
	}
	
	override public string ToString ()
	{
		string s = "";
		foreach(KeyValuePair<string, object> p in map){
			s+= p.Key+","+p.Value.ToString()+" ";
		}
		return "StateMessage["+s+"]";
	}
}
