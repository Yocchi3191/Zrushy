using Zrushy.Core.Domain.Scenarios.Entity;

namespace Zrushy.Core.Presentation
{
	public class HeroinViewModel
	{
		public Action? CurrentAction { get; private set; }
		public event System.Action<HeroinViewModel>? OnUpdated;

		internal void Act(Action action)
		{
			CurrentAction = action;
			OnUpdated?.Invoke(this);
		}
	}
}