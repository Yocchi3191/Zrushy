using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart
	{
		private readonly Heroin heroin;
		private readonly IEventEvaluator eventEvaluator;

		public InteractPart(Heroin heroin, IEventEvaluator eventEvaluator)
		{
			this.heroin = heroin;
			this.eventEvaluator = eventEvaluator;
		}

		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		/// <param name="command">操作コマンド</param>
		public void Execute(InteractPartCommand command)
		{
			Interaction interaction = new Interaction(command.PartID, command.Type);
			heroin.Interact(interaction);
			eventEvaluator.Evaluate(interaction);
			if (heroin.IsClimax) heroin.ApplyCooldown();
		}
	}
}
