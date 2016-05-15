using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;

public static class BinaryDataFormatter
{

	public static byte[] ToBytes (object obj)
	{
		BinaryFormatter bf = GetBinaryFormatter();
		byte[] bytes;
		using (MemoryStream ms = new MemoryStream()) {
			bf.Serialize (ms, obj);
			bytes = ms.ToArray ();
		}
		return bytes;
	}

	public static object FromBytes(byte[] bytes){
		object obj = null;
		BinaryFormatter bf = GetBinaryFormatter();
		using (MemoryStream ms = new MemoryStream()) {
			ms.Write (bytes, 0, bytes.Length);
			ms.Seek (0, SeekOrigin.Begin);
			obj = bf.Deserialize (ms);
		}
		return obj;
	}

	public static BinaryFormatter GetBinaryFormatter(){
		BinaryFormatter bf = new BinaryFormatter();
		SurrogateSelector ss = new SurrogateSelector();
		ss.AddSurrogate(typeof(Vector3),
		                new StreamingContext(StreamingContextStates.All),
		                new Vector3SerializationSurrogate());
		ss.AddSurrogate(typeof(Quaternion),
		                new StreamingContext(StreamingContextStates.All),
		                new QuaternionSerializationSurrogate());
		ss.AddSurrogate(typeof(Color),
		                new StreamingContext(StreamingContextStates.All),
		                new ColorSerializationSurrogate());
		bf.SurrogateSelector = ss;
		return bf;
	}
}

/// <summary>
/// Vector3 surrogate since unity doesn't flag his class as [Serializable]
/// </summary>

sealed class Vector3SerializationSurrogate : ISerializationSurrogate {

	public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
	{
		Vector3 v3 = (Vector3) obj;
		info.AddValue("x", v3.x);
		info.AddValue("y", v3.y);
		info.AddValue("z", v3.z);
	}

	public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
	{
		Vector3 v3 = (Vector3) obj;
		v3.x = info.GetSingle("x");
		v3.y = info.GetSingle("y");
		v3.z = info.GetSingle("z");
		obj = v3;
		return obj;
	}
}
/// <summary>
/// Color surrogate since unity doesn't flag his class as [Serializable]
/// </summary>
sealed class ColorSerializationSurrogate : ISerializationSurrogate {
	public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
	{
		Color c = (Color) obj;
		info.AddValue("a", c.a);
		info.AddValue("b", c.b);
		info.AddValue("g", c.g);
		info.AddValue("r", c.r);
	}

	public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
	{
		Color c = (Color) obj;
		c.a = (float)info.GetValue("a", typeof(float));
		c.r = (float)info.GetValue("r", typeof(float));
		c.b = (float)info.GetValue("b", typeof(float));
		c.g = (float)info.GetValue("g", typeof(float));
		obj = c;
		return obj;
	}
}
/// <summary>
/// Quaternion surrogate since unity doesn't flag his class as [Serializable]
/// </summary>
sealed class QuaternionSerializationSurrogate : ISerializationSurrogate {
	public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
	{
		Quaternion q = (Quaternion) obj;
		info.AddValue("w", q.w);
		info.AddValue("x", q.x);
		info.AddValue("y", q.y);
		info.AddValue("z", q.z);
	}

	public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
	{
		Quaternion q = (Quaternion) obj;
		q.w = info.GetSingle("w");
		q.x = info.GetSingle("x");
		q.y = info.GetSingle("y");
		q.z = info.GetSingle("z");
		obj = q;
		return obj;
	}
}
