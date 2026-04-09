using System.Collections.Generic;

namespace Zrushy.Core.Presentation.Unity
{
	public class ClickableRegistry
	{
		private readonly Dictionary<string, Clickable> _clickables = new();

		public void Register(Clickable clickable)
		{
			_clickables[clickable.PartId] = clickable;
		}

		public void Unregister(Clickable clickable)
		{
			_clickables.Remove(clickable.PartId);
		}

		public bool TryGet(string partId, out Clickable clickable)
		{
			return _clickables.TryGetValue(partId, out clickable);
		}
	}
}
