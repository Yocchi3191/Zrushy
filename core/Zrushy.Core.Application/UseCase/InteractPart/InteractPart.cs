using Zrushy.Core.Domain.Entity;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	public class InteractPart
	{
		Body body;
		public void Execute(InteractPartCommand command)
		{
			body.Interact();
		}
	}
}
