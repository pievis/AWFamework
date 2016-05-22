using UnityEngine;
using System.Collections;
using AWFramework;

/// <summary>
/// A behaviour that listens to collision events and propagetes it
/// to its event context.
/// </summary>
public class TrakingAreaBehaviour : MonoEventContext, IEventSender {

	Collider trakingArea; 

	void Start () {
		CheckCollider();
	}

	void CheckCollider(){
		trakingArea = GetComponent<Collider>();
		if(trakingArea == null){
			Debug.Log(gameObject.name + " has no collider");
		}
	}

	void OnTriggerEnter(Collider collider){
		HologramComponent hc = collider.GetComponentInParent<HologramComponent>();
		if(hc == null)
			return;
		OnTrakingAreaEnterEvent e = new OnTrakingAreaEnterEvent(collider.gameObject, this);
		Send(e);
	}

	void OnTriggerExit(Collider collider){
		HologramComponent hc = collider.GetComponentInParent<HologramComponent>();
		if(hc == null)
			return;
		OnTrakingAreaExitEvent e = new OnTrakingAreaExitEvent(collider.gameObject, this);
		Send(e);
	}

	void Log(string msg){
		Debug.Log (gameObject.name + "] " + msg);
	}

}
