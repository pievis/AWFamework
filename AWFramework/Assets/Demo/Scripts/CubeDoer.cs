using UnityEngine;
using System.Collections;
using AWFramework;

public class CubeDoer : HoloDoerComponent {

	public HologramComponent cubeHologram;
	public int counterLimit = 5;
	int counter = 0;
	public MonoEventContext[] subscribeTo;

	// Use this for initialization
	void Start () {
		Init();
		SubscribeToMainEventContext();
		foreach(MonoEventContext mec in subscribeTo){
			mec.Subscribe(this);
		}
	}

	// Update is called once per frame
	void Update () {
		ExecEventsInQueue();
	}

	public override void Exec (IEvent e)
	{
		Debug.Log("Executing event " + e.ToString());

		CountForJump();

		System.Type type = e.GetType();
		if(type.Equals(typeof(OnTrakingAreaEnterEvent))){
			Color c = Color.red;
			cubeHologram.Invoke("SetColor", c);
		}
		if(type.Equals(typeof(OnTrakingAreaExitEvent))){
			Color c = Color.blue;
			cubeHologram.Invoke("SetColor", c);
		}
		if(type.Equals(typeof(ButtonPressedEvent))){
			ButtonPressedEvent bpe = (ButtonPressedEvent) e;
			if(bpe.State == 1)
				cubeHologram.Invoke("SetColor", Color.yellow);
			else
				cubeHologram.Invoke("SetColor", Color.grey);
		}
	}

	void CountForJump(){
		counter++;
		if(counter >= counterLimit){
			counter = 0;
			cubeHologram.Invoke("Jump");
		}
		Debug.Log("Coutner: " + counter);
	}
}
