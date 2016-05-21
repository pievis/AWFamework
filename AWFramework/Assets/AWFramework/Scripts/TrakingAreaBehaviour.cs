using UnityEngine;
using System.Collections;
using AWFramework;

/// <summary>
/// A behaviour that listens to collision events and propagetes it
/// to its event context.
/// </summary>
public class TrakingAreaBehaviour : MonoEventContext, IEventSender {

	public Collider trakingArea; 

	void Start () {
		CheckCollider();
	}

	void CheckCollider(){
		if(trakingArea == null){
			Debug.Log(gameObject.name + " has no collider");
		}
	}

	void OnTriggerEnter(Collider collider){
		OnTrakingAreaEnterEvent e = new OnTrakingAreaEnterEvent(collider.gameObject, this);
		Send(e);
	}

	void OnTriggerExit(Collider collider){
		OnTrakingAreaExitEvent e = new OnTrakingAreaExitEvent(collider.gameObject, this);
		Send(e);
	}
}
