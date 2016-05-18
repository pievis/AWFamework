using System;

namespace AWFramework
{
	public class BaseEvent : IEvent
	{
		IEventSender sender;

		public void SetSender(IEventSender sender){
			this.sender = sender;
		}

		public IEventSender GetSender ()
		{
			return sender;
		}
	}
}

