using UnityEngine;
using System.Collections;
using AWFramework;

public class CylinderView : MonoBehaviour, IView {

	HologramComponent hc;
	Vector3 force = Vector3.zero;
	
	void Start () {
		hc = GetComponent<HologramComponent>();
	}

	void Update () {
		Moving ();
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

	void SendEvent(){

	}

	/// <summary>
	/// Gives the input to move for one frame.
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void MoveLocal(Vector3 dir){
		force = dir;
	}
}
