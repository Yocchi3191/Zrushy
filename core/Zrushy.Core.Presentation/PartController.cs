using Zrushy.Core.Application.UseCase.InteractPart;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位のコントローラー
	/// ユーザー入力をUseCaseに変換し、結果をViewModelに反映する
	/// </summary>
	public class PartController
	{
		private readonly InteractPart interactPartUseCase;
		private readonly ScenarioInputGate scenarioInputGate;

		public PartController(InteractPart interactPartUseCase, ScenarioInputGate scenarioInputGate)
		{
			this.interactPartUseCase = interactPartUseCase;
			this.scenarioInputGate = scenarioInputGate;
		}

		/// <summary>
		/// さわり入力を受け取り、Domainにパラメータ変化を反映する
		/// </summary>
		/// <param name="input">ユーザー入力</param>
		public void SendInput(PartInput input)
		{
			InteractPartCommand command = new InteractPartCommand(input.PartID, input.Type);
			interactPartUseCase.Execute(command);
			scenarioInputGate.NotifyTouch(input.PartID.Value);
		}
	}
}
