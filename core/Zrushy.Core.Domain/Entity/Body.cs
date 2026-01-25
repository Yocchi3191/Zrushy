using System.Collections.Generic;

namespace Zrushy.Core.Domain.Entity
{
	public class Body
	{
		private List<Part> Parts { get; set; }
		public void Interact(Interaction interaction)
		{
			Parts.Find(p => p.ID == interaction.PartID)?.Interact();
		}
	}
}
