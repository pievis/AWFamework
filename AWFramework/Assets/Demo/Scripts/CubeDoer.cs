using UnityEngine;
using System.Collections;
using AWFramework;

public class CubeDoer : HoloDoerComponent {

	public HologramComponent cubeHologram;
	public int counterLimit = 5;
	int counter = 0;

	// Use this for initialization
	void Start () {
		Init();
		SubscribeToMainEventContext();
	}

	// Update is called once per frame
	void Update () {
		ExecEventsInQueue();
	}

	public override void Exec (IEvent e)
	{
		Debug.Log("Executing event " + e.ToString());
		counter++;
		if(counter >= counterLimit){
			counter = 0;
			cubeHologram.Invoke("Jump");
		}
		Debug.Log("Coutner: " + counter);
	}
}
