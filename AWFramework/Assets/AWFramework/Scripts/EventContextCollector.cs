using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AWFramework;

/// <summary>
/// This class is meant to be used to collect geometry based EventContext
/// using Unity based trigger colliders.
/// </summary>
public class EventContextCollector : MonoBehaviour, IEventSender {

	List<IEventContext> contexts;

	Collider col;

	void Start(){
		contexts = new List<IEventContext>();
		col = GetComponent<Collider>();
		if(col == null)
			Debug.LogWarning(gameObject.name + " has no collider");
	}

	//Automatic context catch

	void OnTriggerEnter(Collider other) {
		IEventContext ec = other.GetComponentInParent<IEventContext>();
		if(ec != null)
			RegisterContext(ec);
	}

	void OnTriggerExit(Collider other) {
		IEventContext ec = other.GetComponentInParent<IEventContext>();
		if(ec != null)
			UnregisterContext(ec);
	}

	//public methods

	public void RegisterContext(IEventContext ec){
		contexts.Add(ec);
	}

	public void UnregisterContext(IEventContext ec){
		contexts.Remove(ec);
	}

	/// <summary>
	/// Send the specified event to all context registered in this collection.
	/// </summary>
	/// <param name="e">AWFramework Event</param>
	public void Send(IEvent e){
		foreach(IEventContext ec in contexts){
			ec.Send(e);
		}
	}
}
