using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AWFramework;

public class MonoEventContext : MonoBehaviour, IEventContext {

	IList<IEventListener> listeners;
	IList<IEvent> events;


	void Start(){
		Init ();
	}

	void Init(){
		listeners = new List<IEventListener>();
		events = new List<IEvent>();
	}

	public void Subscribe (IEventListener listener)
	{
		listeners.Add(listener);
	}

	public void Unsubscribe (IEventListener listener)
	{
		listeners.Remove(listener);
	}

	public void Send (IEvent e)
	{
		Debug.Log("Send}");
		events.Add(e);
	}

	void SendToListeners(IEvent e){
		Debug.Log("Send--} " + e.ToString());
		foreach(IEventListener el in listeners){
			Debug.Log("Send------} " + e.ToString());
			el.AddToQueue(e);
		}
	}

	void FixedUpdate(){
		for (int i = events.Count - 1; i >= 0; i--)
		{
			SendToListeners(events[i]);
			events.RemoveAt(i);
		}
	}
}
