using System;
using UnityEngine;
using AWFramework;

public class ButtonPressedEvent : BaseEvent
{

	int state = 0;

	public ButtonPressedEvent (IEventSender sender, int state)
	{
		SetSender(sender);
		this.state = state;
	}

	public int State {
		get {
			return this.state;
		}
	}

}