using UnityEngine;
using System.Collections;
using AWFramework;

public class CylinderView : MonoBehaviour, IView, IEventSender {

	HologramComponent hc;
	EventContextCollector ecc;
	Vector3 force = Vector3.zero;
	
	void Start () {
		hc = GetComponent<HologramComponent>();
		ecc = GetComponent<EventContextCollector>();
	}

	void Update () {
		Moving ();
		//pressing button
		if(Input.GetKeyDown(KeyCode.Space))
			hc.Invoke("SpacePressed", 1);
		if(Input.GetKeyUp(KeyCode.Space))
			hc.Invoke("SpacePressed", 0);
	}

	void Moving(){
		//handle input
		float x = Input.GetAxisRaw("Horizontal");
		if(x != 0){
			Vector3 dir = new Vector3(x,0,0);
			hc.Invoke("Move", dir);
		}
		//actually move
		transform.position += force * Time.deltaTime * 2.5f;
		force = Vector3.zero;
	}

	public void SendBtnEvent(int state){
		if(ecc != null){
			ButtonPressedEvent bpe = new ButtonPressedEvent(this, state);
			ecc.Send(bpe);
		}
	}

	/// <summary>
	/// Gives the input to move for one frame.
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void MoveLocal(Vector3 dir){
		force = dir;
	}
}
