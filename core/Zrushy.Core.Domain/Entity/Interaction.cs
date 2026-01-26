using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	public class Interaction
	{
		public Interaction(PartID partID)
		{
			PartID = partID;
		}

		public PartID PartID { get; internal set; }
	}
}