using Zrushy.Core.Application.UseCase.InteractPart;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位のコントローラー
	/// ユーザー入力をUseCaseに変換し、結果をViewModelに反映する
	/// </summary>
	public class PartController
	{
		private readonly IInteractPart interactPartUseCase;

		public PartController(IInteractPart interactPartUseCase)
		{
			this.interactPartUseCase = interactPartUseCase;
		}

		/// <summary>
		/// ユーザー入力を受け取り、さわり反応の処理を実行する
		/// InteractPart が EventBus にイベントを発火し、ScenarioDriver が購読して ScenarioPlayer を制御する
		/// </summary>
		/// <param name="input">ユーザー入力</param>
		public void SendInput(PartInput input)
		{
			InteractPartCommand command = new InteractPartCommand(input.PartID);
			interactPartUseCase.Execute(command);
			// EventBus 経由でイベントが発火される
		}
	}
}
