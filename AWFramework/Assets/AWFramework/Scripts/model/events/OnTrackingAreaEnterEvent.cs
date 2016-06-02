using System;
using UnityEngine;
namespace AWFramework
{
	public class OnTrackingAreaEnterEvent : BaseEvent
	{
		TrackingAreaBehaviour trackingAreaBehaviour;
		GameObject gotIn;

		public OnTrackingAreaEnterEvent (
			GameObject gotIn,
			TrackingAreaBehaviour tab)
		{
			this.trackingAreaBehaviour = tab;
			SetSender(trackingAreaBehaviour);
			this.gotIn = gotIn;
		}

		public TrackingAreaBehaviour TrackingAreaBehaviour {
			get {
				return this.trackingAreaBehaviour;
			}
			set {
				SetSender(trackingAreaBehaviour);
				trackingAreaBehaviour = value;
			}
		}

		public GameObject GotIn {
			get {
				return this.gotIn;
			}
			set {
				gotIn = value;
			}
		}
	}
}

