namespace AWFramework
{
	public interface IEventContext
	{

		void Subscribe (IEventListener listener);

		void Unsubscribe (IEventListener listener);

		void Send (IEvent e);
	
	}
}
