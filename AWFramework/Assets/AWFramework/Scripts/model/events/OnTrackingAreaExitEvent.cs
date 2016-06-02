using System;
using UnityEngine;
namespace AWFramework
{
	public class OnTrackingAreaExitEvent : BaseEvent
	{
		GameObject gotOut;
		TrackingAreaBehaviour ta;
		
		public OnTrackingAreaExitEvent (GameObject gotOut, TrackingAreaBehaviour tab)
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
		
		public TrackingAreaBehaviour TrakingAreaBehaviour {
			get {
				return this.ta;
			}
			set {
				ta = value;
			}
		}
	}
}

