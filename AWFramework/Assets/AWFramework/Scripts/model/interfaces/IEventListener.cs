namespace AWFramework
{
	public interface IEventListener
	{
		void Exec(IEvent e);
		void AddToQueue(IEvent e);
	}
}