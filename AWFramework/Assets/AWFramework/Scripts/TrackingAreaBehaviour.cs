using UnityEngine;
using System.Collections;
using AWFramework;

/// <summary>
/// A behaviour that listens to collision events and propagetes it
/// to its event context.
/// </summary>
public class TrackingAreaBehaviour : MonoEventContext, IEventSender {

	Collider trackingArea; 

	void Start () {
		CheckCollider();
	}

	void CheckCollider(){
		trackingArea = GetComponent<Collider>();
		if(trackingArea == null){
			Debug.Log(gameObject.name + " has no collider");
		}
	}

	void OnTriggerEnter(Collider collider){
		HologramComponent hc = collider.GetComponentInParent<HologramComponent>();
		if(hc == null)
			return;
		OnTrackingAreaEnterEvent e = new OnTrackingAreaEnterEvent(collider.gameObject, this);
		Send(e);
	}

	void OnTriggerExit(Collider collider){
		HologramComponent hc = collider.GetComponentInParent<HologramComponent>();
		if(hc == null)
			return;
		OnTrackingAreaExitEvent e = new OnTrackingAreaExitEvent(collider.gameObject, this);
		Send(e);
	}

	void Log(string msg){
		Debug.Log (gameObject.name + "] " + msg);
	}

}
