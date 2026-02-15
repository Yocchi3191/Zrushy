using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Application.UseCase.ApplyBonus
{
	/// <summary>
	/// 部位の開発度ボーナスを適用するユースケース
	/// シナリオコマンド apply_bonus から呼ばれる
	/// </summary>
	public class ApplyBonus
	{
		private readonly Heroin body;

		public ApplyBonus(Heroin body)
		{
			this.body = body;
		}

		/// <summary>
		/// 指定部位に開発度ボーナスを加算する
		/// </summary>
		/// <param name="command">対象部位IDと加算量</param>
		public void Execute(ApplyBonusCommand command)
		{
			body.ApplyDevelopmentBonus(command.PartID, command.Amount);
		}
	}
}
