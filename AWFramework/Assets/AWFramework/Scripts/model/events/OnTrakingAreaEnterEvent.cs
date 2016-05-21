using System;
using UnityEngine;
namespace AWFramework
{
	public class OnTrakingAreaEnterEvent : BaseEvent
	{
		TrakingAreaBehaviour trakingAreaBehaviour;
		GameObject gotIn;

		public OnTrakingAreaEnterEvent (
			GameObject gotIn,
			TrakingAreaBehaviour tab)
		{
			this.trakingAreaBehaviour = tab;
			SetSender(trakingAreaBehaviour);
			this.gotIn = gotIn;
		}

		public TrakingAreaBehaviour TrakingAreaBehaviour {
			get {
				return this.trakingAreaBehaviour;
			}
			set {
				SetSender(trakingAreaBehaviour);
				trakingAreaBehaviour = value;
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

