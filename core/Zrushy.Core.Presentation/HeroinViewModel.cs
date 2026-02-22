using Zrushy.Core.Domain.Scenarios.Entity;

namespace Zrushy.Core.Presentation
{
	public class HeroinViewModel
	{
		public Beat? CurrentBeat { get; private set; }
		public event System.Action<HeroinViewModel>? OnUpdated;

		public void Act(Beat beat)
		{
			CurrentBeat = beat;
			OnUpdated?.Invoke(this);
		}
	}
}