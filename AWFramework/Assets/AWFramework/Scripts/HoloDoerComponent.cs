using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AWFramework;

/// <summary>
/// HoloDoer implementation as a component.
/// </summary>
public abstract class HoloDoerComponent : MonoBehaviour, IHoloDoer {


	public bool disableOnClient = true;

	protected Queue<IEvent> eventQueue;

	public abstract void Exec (IEvent e);

	public void AddToQueue (IEvent e)
	{
		if(eventQueue == null)
			eventQueue = new Queue<IEvent>();
		Debug.Log("ADDED");
		eventQueue.Enqueue(e);
	}

	/// <summary>
	/// Execute events that are pending in the queue using inner Exec function.
	/// Most commonly to be used during Update.
	/// </summary>
	protected void ExecEventsInQueue(){
		if(eventQueue == null || eventQueue.Count == 0)
			return;
		int count = eventQueue.Count;
		for(int i = 0; i < count; i++){
			IEvent e = eventQueue.Dequeue();
			Exec(e);
		}
	}

	/// <summary>
	/// Disables this component for client instance of the application.
	/// Useful when using networking system where the server is based 
	/// on Unity Engine like HLAPI.
	/// </summary>
	/// <param name="disableGameObject">If set to <c>true</c> disable game object.</param>
	protected void DisableOnClient(bool disableGameObject){
		bool isServer = AWConfig.IsServer();
		if(!isServer && disableOnClient){
			if(disableGameObject)
				gameObject.SetActive(false);
			enabled = false;
		}
	}

	protected void Init(){
		DisableOnClient(false);
		eventQueue = new Queue<IEvent>();
	}

	/// <summary>
	/// Register the EventListener to the main application EventContext
	/// </summary>
	protected void SubscribeToMainEventContext(){
		IEventContext mec =  AWConfig.GetInstance().MainEventContext;
		mec.Subscribe(this);
	}
}
