using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	public class InteractPartCommand
	{
		public PartID PartID { get; }

		public InteractPartCommand(PartID partID)
		{
			PartID = partID;
		}
	}
}
