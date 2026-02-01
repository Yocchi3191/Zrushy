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
		private readonly ScenarioPlayer scenarioPlayer;

		public PartController(InteractPart interactPartUseCase, ScenarioPlayer scenarioPlayer)
		{
			this.interactPartUseCase = interactPartUseCase;
			this.scenarioPlayer = scenarioPlayer;
		}

		/// <summary>
		/// ユーザー入力を受け取り、さわり反応の処理を実行してViewModelを更新する
		/// ViewModelが更新されると、自動的にViewに通知される
		/// </summary>
		/// <param name="input">ユーザー入力</param>
		/// <param name="viewModel">更新対象のViewModel</param>
		public void SendInput(PartInput input)
		{
			InteractPartCommand command = new InteractPartCommand(input.PartID);
			InteractPartResult result = interactPartUseCase.Execute(command);

			scenarioPlayer.Play(result.ScenarioToStart);
		}
	}
}
