namespace AWFramework
{
	public interface IHologram : IEventSender
	{
		void SetView(IView view);
		void SetNetworkSync(INetworkSync netSync);
		void SetModel(IModel model);

		/// <summary>
		/// Invoke on model, view and networkSync the specified method by name.
		/// </summary>
		/// <param name="name">Name of the method to invoke.</param>
		/// <param name="args">Arguments of the method.</param>
		void Invoke(string name, object[] args);
	}
}

