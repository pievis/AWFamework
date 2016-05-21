using System;
using UnityEngine;
namespace AWFramework
{
	public class OnTrakingAreaExitEvent : BaseEvent
	{
		GameObject gotOut;
		TrakingAreaBehaviour ta;
		
		public OnTrakingAreaExitEvent (GameObject gotOut, TrakingAreaBehaviour tab)
		{
			this.gotOut = gotOut;
			this.ta = tab;
		}
		
		public GameObject GotOut {
			get {
				return this.gotOut;
			}
			set {
				gotOut = value;
			}
		}
		
		public TrakingAreaBehaviour TrakingAreaBehaviour {
			get {
				return this.ta;
			}
			set {
				ta = value;
			}
		}
	}
}

