using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart
	{
		private readonly Heroin body;

		public InteractPart(Heroin body)
		{
			this.body = body;
		}

		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		/// <param name="command">操作コマンド</param>
		public void Execute(InteractPartCommand command)
		{
			body.Interact(new Interaction(command.PartID, command.Type));
		}
	}
}
