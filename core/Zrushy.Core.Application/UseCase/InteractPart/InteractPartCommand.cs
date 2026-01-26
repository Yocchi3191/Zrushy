using Zrushy.Core.Domain.ValueObject;

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
